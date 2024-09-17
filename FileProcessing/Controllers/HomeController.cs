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
                // Retourne un message d'erreur JSON si aucun fichier n'a été fourni
                return Json(new { success = false, message = "Aucun fichier sélectionné" });
            }

            // Simuler le traitement du fichier
            for (int i = 0; i <= 100; i += 10)
            {
                await Task.Delay(500);  // Attendre un peu pour simuler un délai de traitement

                await _hubContext.Clients.All.SendAsync("ReceiveProgress", i);   // Envoyer le progression vers SignalR
            }

            return Json(new { success = true, message = "Fichier téléchargé avec succès" });
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
