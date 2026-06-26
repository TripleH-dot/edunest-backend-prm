using BusinessLayer.IServices;

namespace EduNest_Backend.BackgroundServices
{
    public sealed class LessonNotificationBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<LessonNotificationBackgroundService> _logger;

        public LessonNotificationBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<LessonNotificationBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var notificationService = scope.ServiceProvider
                        .GetRequiredService<INotificationService>();

                    await notificationService.CreateLessonReminderNotificationsAsync(DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create lesson reminder notifications.");
                }

                try
                {
                    await timer.WaitForNextTickAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
