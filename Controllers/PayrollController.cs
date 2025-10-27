using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    public class PayrollController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
