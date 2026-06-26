using System.Security.Claims;
using BusinessLayer.DTOs.Notification;
using BusinessLayer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduNest_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/notification")]
    public sealed class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<List<NotificationResponse>>> GetMyNotifications(
            [FromQuery] bool unreadOnly = false,
            [FromQuery] int take = 50)
        {
            return Ok(await _notificationService.GetMyNotificationsAsync(
                CurrentUserId(),
                unreadOnly,
                take));
        }

        [HttpGet("unread-count")]
        public async Task<ActionResult<object>> GetUnreadCount()
        {
            return Ok(new
            {
                unreadCount = await _notificationService.GetUnreadCountAsync(CurrentUserId())
            });
        }

        [HttpPost("{notificationId:int}/read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            await _notificationService.MarkAsReadAsync(CurrentUserId(), notificationId);
            return NoContent();
        }

        [HttpPost("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await _notificationService.MarkAllAsReadAsync(CurrentUserId());
            return NoContent();
        }

        private int CurrentUserId()
        {
            var raw = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub");

            return int.Parse(raw!);
        }
    }
}
