using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize] // Только для авторизованных пользователей
    public class ProfileController : Controller
    {
        // ...
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult ManageUsers()
        {
            // Доступно только менеджерам и администраторам
            return View();
        }
    }
}
