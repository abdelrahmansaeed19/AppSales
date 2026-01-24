using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Expenses
{
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
