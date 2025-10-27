using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
