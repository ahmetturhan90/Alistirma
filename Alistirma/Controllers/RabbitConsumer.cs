using Microsoft.AspNetCore.Mvc;

namespace Alistirma.Controllers
{

    public class RabbitConsumer : Controller
    {
        public IActionResult Index()
        {
         
            return View();
        }
    }
}
