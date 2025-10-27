using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    public class LoansController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
