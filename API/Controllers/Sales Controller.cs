using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Sales
{
    public class Sales_Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
