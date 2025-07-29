using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize(Roles = "Admin")] // Только для администраторов
    public class AdminController : Controller
    {
        // ...

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            // Access администраторам
            return View();
        }
    }
}
