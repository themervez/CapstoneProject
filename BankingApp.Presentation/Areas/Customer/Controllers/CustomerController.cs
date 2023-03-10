using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Presentation.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Customer/[controller]/[action]")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
