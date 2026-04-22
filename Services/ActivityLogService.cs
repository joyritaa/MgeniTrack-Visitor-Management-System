using MgeniTrack.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MgeniTrack.Services
{
    public class ActivityLogService
    {
        private readonly MgenitrackContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActivityLogService(MgenitrackContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(
            string actionType,
            string? actionDetails = null,
            string? relatedEntityType = null,
            int? relatedEntityId = null,
            int? userId = null)
        {
            var http = _httpContextAccessor.HttpContext;

            // Try to get userId from claims if not passed in
            if (userId == null && http?.User?.Identity?.IsAuthenticated == true)
            {
                var email = http.User.FindFirstValue(ClaimTypes.Name);
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                userId = user?.UserId;
            }

            var log = new ActivityLog
            {
                UserId = userId,
                ActionType = actionType,
                ActionDetails = actionDetails,
                RelatedEntityType = relatedEntityType,
                RelatedEntityId = relatedEntityId,
                IpAddress = http?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = http?.Request?.Headers["User-Agent"].ToString(),
                TimeStamp = DateTime.Now
            };

            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}