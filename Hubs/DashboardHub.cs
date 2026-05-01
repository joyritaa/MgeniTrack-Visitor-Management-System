using Microsoft.AspNetCore.SignalR;

namespace MgeniTrack.Hubs;

public class DashboardHub : Hub
{
    // connection life cycle
    public override async Task OnConnectedAsync()
    {
        var role = Context.GetHttpContext()?.Request.Query["role"].ToString();
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

        if (!string.IsNullOrEmpty(role))
        {
            var groupName = role.ToLower();
            if (!groupName.EndsWith("s"))
                groupName += "s";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        if (!string.IsNullOrEmpty(userId))
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    // Client can request stats update
    public async Task RequestStatsUpdate()
    {
        // This can be called by clients to trigger a stats refresh
        await Clients.All.SendAsync("RefreshStatsRequested");
    }

    // Notify when a visitor checks in/out
    public async Task NotifyVisitUpdate(object visitUpdate)
    {
        await Clients.All.SendAsync("VisitUpdated", visitUpdate);
    }

    // Notify when a unit status changes
    public async Task NotifyUnitStatusChange(object unitUpdate)
    {
        await Clients.Group("admins").SendAsync("UnitStatusChanged", unitUpdate);
        await Clients.Group("guards").SendAsync("UnitStatusChanged", unitUpdate);
    }
}