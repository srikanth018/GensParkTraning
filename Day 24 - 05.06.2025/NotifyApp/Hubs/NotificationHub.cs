using Microsoft.AspNetCore.SignalR;

namespace NotifyApp.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string fileName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", fileName, message);
        }
    }
}