using Microsoft.AspNetCore.SignalR;
using MgeniTrack.Hubs;

namespace MgeniTrack.Services;

public class DashboardNotifier
{
    private readonly IHubContext<DashboardHub> _hub;

    public DashboardNotifier(IHubContext<DashboardHub> hub)
    {
        _hub = hub;
    }

    // Called when a visitor checks in or out
    public async Task NotifyVisitUpdate(object visitSummary, int? residentUserId = null)
    {
        // Notify guard group
        await _hub.Clients.Group("guards").SendAsync("VisitUpdated", visitSummary);

        // Notify admin group
        await _hub.Clients.Group("admins").SendAsync("VisitUpdated", visitSummary);

        // Notify specific resident if provided
        if (residentUserId.HasValue)
        {
            await _hub.Clients.Group($"residents:{residentUserId}")
                              .SendAsync("VisitorArrived", visitSummary);
        }
    }

    // Called when a new user / resident is created
    public async Task NotifyUserCreated(object userSummary)
    {
        await _hub.Clients.Group("admins").SendAsync("UserCreated", userSummary);
    }

    // Called when a unit status changes
    public async Task NotifyUnitStatusChanged(object unitSummary)
    {
        await _hub.Clients.Group("admins").SendAsync("UnitStatusChanged", unitSummary);
    }

    // Generic stats refresh
    public async Task BroadcastStats(object stats)
    {
        await _hub.Clients.All.SendAsync("StatsUpdated", stats);
    }
}