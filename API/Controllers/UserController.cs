using Microsoft.AspNetCore.Mvc;

namespace API.Modules.User
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
