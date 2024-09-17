using Microsoft.AspNetCore.SignalR;

namespace FileProcessing.Models
{
    public class FileProcessingHub : Hub
    {
        public async Task NotifyProgess(int progressPercentage)
        {
            await Clients.All.SendAsync("ReceiveProgress", progressPercentage);
        }


       
    }
}
