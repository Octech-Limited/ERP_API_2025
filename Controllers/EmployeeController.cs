using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
