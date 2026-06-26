namespace BusinessLayer.DTOs.Notification
{
    public sealed class NotificationResponse
    {
        public int NotificationId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? BookingId { get; set; }
        public int? LessonId { get; set; }
        public int? AvailabilityId { get; set; }
        public int? MaterialId { get; set; }
        public int? PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
