using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                // Другие коды ошибок...
                default:
                    return View("Error");
            }
        }
    }
}
