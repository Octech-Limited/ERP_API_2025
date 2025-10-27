using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    public class ComplianceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
