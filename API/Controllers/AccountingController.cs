using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Accounting
{
    public class AccountingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
