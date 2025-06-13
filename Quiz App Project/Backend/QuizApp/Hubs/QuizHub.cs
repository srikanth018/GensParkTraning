using Microsoft.AspNetCore.SignalR;

namespace QuizApp.Hubs
{
    public class QuizHub : Hub
    {
        public async Task NotifyNewQuiz(string category, string title)
        {
            await Clients.All.SendAsync("ReceiveNewQuiz", category, title);
        }
    }
}
