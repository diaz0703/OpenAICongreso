using ChatCommun;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenRagTest.Models;
using System.Diagnostics;

namespace OpenRagTest.Controllers
{
//    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public string construyeresp(string messageinput)
        {
            OpenAIClass _chat = new OpenAIClass();
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion() { Resultado = 0, MensajeError = "" };
            try
            {
                resultadoOperacion.Respuesta = _chat.RecibeRespuesta(messageinput);
            }
            catch (Exception ex)
            {
                resultadoOperacion.Resultado = 1;
                resultadoOperacion.MensajeError = ex.Message;
            }
            return System.Text.Json.JsonSerializer.Serialize(resultadoOperacion);
        }



        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
