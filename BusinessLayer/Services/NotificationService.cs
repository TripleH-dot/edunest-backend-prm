using BusinessLayer.DTOs.Notification;
using BusinessLayer.IServices;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public sealed class NotificationService : INotificationService
    {
        public const string TutorStudentJoined = "TutorStudentJoined";
        public const string StudentCoursePaid = "StudentCoursePaid";
        public const string LessonStartingSoon = "LessonStartingSoon";
        public const string StudentMaterialUploaded = "StudentMaterialUploaded";

        private readonly EduNestDbContext _db;

        public NotificationService(EduNestDbContext db)
        {
            _db = db;
        }

        public async Task<List<NotificationResponse>> GetMyNotificationsAsync(
            int userId,
            bool unreadOnly,
            int take)
        {
            take = Math.Clamp(take, 1, 100);

            var query = _db.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == userId);

            if (unreadOnly)
                query = query.Where(n => !n.IsRead);

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .Take(take)
                .Select(n => ToResponse(n))
                .ToListAsync();
        }

        public Task<int> GetUnreadCountAsync(int userId)
        {
            return _db.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task MarkAsReadAsync(int userId, int notificationId)
        {
            var notification = await _db.Notifications
                .FirstOrDefaultAsync(n =>
                    n.NotificationId == notificationId &&
                    n.UserId == userId)
                ?? throw new KeyNotFoundException("Notification not found.");

            if (notification.IsRead)
                return;

            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var now = DateTime.UtcNow;

            await _db.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(n => n.IsRead, true)
                    .SetProperty(n => n.ReadAt, now));
        }

        public async Task NotifyPaymentSucceededAsync(Payment payment)
        {
            var booking = await _db.Bookings
                .Include(b => b.User)
                .Include(b => b.Student)
                    .ThenInclude(s => s.User)
                .Include(b => b.Availability)
                    .ThenInclude(a => a.Tutor)
                        .ThenInclude(t => t.User)
                .Include(b => b.Availability)
                    .ThenInclude(a => a.Subject)
                .FirstAsync(b => b.BookingId == payment.BookingId);

            var learnerUserId = booking.UserId ?? booking.Student?.UserId;
            var learnerName = booking.User?.Name
                ?? booking.Student?.User?.Name
                ?? $"User #{learnerUserId}";
            var courseName = booking.Availability.Subject?.Name
                ?? $"Course #{booking.AvailabilityId}";
            var tutorUserId = booking.Availability.Tutor.UserId;

            if (learnerUserId.HasValue)
            {
                await AddIfMissingAsync(new Notification
                {
                    UserId = learnerUserId.Value,
                    Type = StudentCoursePaid,
                    Title = "Thanh toán khóa học thành công",
                    Message = $"Bạn đã thanh toán thành công khóa học {courseName}.",
                    ReferenceKey = $"{StudentCoursePaid}:{payment.PaymentId}",
                    BookingId = booking.BookingId,
                    AvailabilityId = booking.AvailabilityId,
                    PaymentId = payment.PaymentId
                });
            }

            await AddIfMissingAsync(new Notification
            {
                UserId = tutorUserId,
                Type = TutorStudentJoined,
                Title = "Có học viên mới tham gia khóa học",
                Message = $"{learnerName} đã thanh toán và tham gia khóa học {courseName}.",
                ReferenceKey = $"{TutorStudentJoined}:{payment.PaymentId}",
                BookingId = booking.BookingId,
                AvailabilityId = booking.AvailabilityId,
                PaymentId = payment.PaymentId
            });

            await _db.SaveChangesAsync();
        }

        public async Task NotifyMaterialUploadedAsync(Material material)
        {
            var availability = await _db.Availabilities
                .AsNoTracking()
                .Include(a => a.Subject)
                .FirstAsync(a => a.AvailabilityId == material.AvailabilityId);

            var courseName = availability.Subject?.Name
                ?? $"Course #{availability.AvailabilityId}";

            var learnerUserIds = await _db.Bookings
                .AsNoTracking()
                .Include(b => b.Student)
                .Where(b =>
                    b.AvailabilityId == material.AvailabilityId &&
                    !b.IsDeleted &&
                    (b.Status == "Confirmed" || b.Status == "Completed"))
                .Select(b => b.UserId ?? b.Student!.UserId)
                .Where(userId => userId > 0)
                .Distinct()
                .ToListAsync();

            foreach (var learnerUserId in learnerUserIds)
            {
                await AddIfMissingAsync(new Notification
                {
                    UserId = learnerUserId,
                    Type = StudentMaterialUploaded,
                    Title = "Tài liệu mới trong khóa học",
                    Message = $"Tutor vừa upload tài liệu {material.Title} trong khóa học {courseName}.",
                    ReferenceKey = $"{StudentMaterialUploaded}:{material.MaterialId}:{learnerUserId}",
                    AvailabilityId = material.AvailabilityId,
                    MaterialId = material.MaterialId
                });
            }

            await _db.SaveChangesAsync();
        }

        public async Task<int> CreateLessonReminderNotificationsAsync(DateTime utcNow)
        {
            var windowEnd = utcNow.AddMinutes(15);
            var windowStart = utcNow.AddMinutes(-2);
            var created = 0;

            var lessons = await _db.Lessons
                .Include(l => l.Booking)
                    .ThenInclude(b => b.User)
                .Include(l => l.Booking)
                    .ThenInclude(b => b.Student)
                .Include(l => l.Booking)
                    .ThenInclude(b => b.Availability)
                        .ThenInclude(a => a.Tutor)
                .Include(l => l.Booking)
                    .ThenInclude(b => b.Availability)
                        .ThenInclude(a => a.Subject)
                .Where(l =>
                    l.Status == "Scheduled" &&
                    l.Booking.Status == "Confirmed" &&
                    l.ScheduleTime >= windowStart &&
                    l.ScheduleTime <= windowEnd)
                .ToListAsync();

            foreach (var lesson in lessons)
            {
                var availability = lesson.Booking.Availability;
                var courseName = availability.Subject?.Name
                    ?? $"Course #{availability.AvailabilityId}";
                var scheduleText = lesson.ScheduleTime.ToString("HH:mm 'UTC'");
                var learnerUserId = lesson.Booking.UserId ?? lesson.Booking.Student?.UserId;

                if (learnerUserId.HasValue)
                {
                    created += await AddIfMissingAsync(new Notification
                    {
                        UserId = learnerUserId.Value,
                        Type = LessonStartingSoon,
                        Title = "Sắp đến giờ học",
                        Message = $"Buổi học {courseName} sẽ bắt đầu lúc {scheduleText}.",
                        ReferenceKey = $"{LessonStartingSoon}:student:{lesson.LessonId}",
                        BookingId = lesson.BookingId,
                        LessonId = lesson.LessonId,
                        AvailabilityId = availability.AvailabilityId
                    });
                }

                created += await AddIfMissingAsync(new Notification
                {
                    UserId = availability.Tutor.UserId,
                    Type = LessonStartingSoon,
                    Title = "Sắp đến giờ dạy",
                    Message = $"Buổi học {courseName} sẽ bắt đầu lúc {scheduleText}.",
                    ReferenceKey = $"{LessonStartingSoon}:tutor:{availability.AvailabilityId}:{lesson.ScheduleTime.Ticks}",
                    BookingId = lesson.BookingId,
                    LessonId = lesson.LessonId,
                    AvailabilityId = availability.AvailabilityId
                });
            }

            if (created > 0)
                await _db.SaveChangesAsync();

            return created;
        }

        private async Task<int> AddIfMissingAsync(Notification notification)
        {
            var pending = _db.Notifications.Local.Any(n =>
                n.UserId == notification.UserId &&
                n.Type == notification.Type &&
                n.ReferenceKey == notification.ReferenceKey);

            if (pending)
                return 0;

            var exists = await _db.Notifications.AnyAsync(n =>
                n.UserId == notification.UserId &&
                n.Type == notification.Type &&
                n.ReferenceKey == notification.ReferenceKey);

            if (exists)
                return 0;

            notification.CreatedAt = DateTime.UtcNow;
            _db.Notifications.Add(notification);
            return 1;
        }

        private static NotificationResponse ToResponse(Notification notification)
        {
            return new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                Type = notification.Type,
                Title = notification.Title,
                Message = notification.Message,
                BookingId = notification.BookingId,
                LessonId = notification.LessonId,
                AvailabilityId = notification.AvailabilityId,
                MaterialId = notification.MaterialId,
                PaymentId = notification.PaymentId,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead,
                ReadAt = notification.ReadAt
            };
        }
    }
}
