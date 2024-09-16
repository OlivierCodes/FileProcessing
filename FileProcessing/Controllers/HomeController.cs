using FileProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace FileProcessing.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IHubContext<FileProcessingHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<FileProcessingHub> hubContext)
        {
         
            _hubContext = hubContext;
        }

        public IActionResult UploadFile()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("ErrorNoFile");
            }

            // Simuler le traitement du fichier
            for (int i = 0; i <= 100; i += 10)
            {
                // Envoyer le progression vers SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveProgress", i);
                await Task.Delay(500);  // Simule un délai de traitement
            }

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult ErrorNoFile()
        {
            return View();
        }


    }
}
