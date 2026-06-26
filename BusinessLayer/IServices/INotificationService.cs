using BusinessLayer.DTOs.Notification;
using DataAccessLayer.Entities;

namespace BusinessLayer.IServices
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetMyNotificationsAsync(
            int userId,
            bool unreadOnly,
            int take);

        Task<int> GetUnreadCountAsync(int userId);
        Task MarkAsReadAsync(int userId, int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task NotifyPaymentSucceededAsync(Payment payment);
        Task NotifyMaterialUploadedAsync(Material material);
        Task<int> CreateLessonReminderNotificationsAsync(DateTime utcNow);
    }
}
