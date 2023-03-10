using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Presentation.Areas.Customer.Controllers
{
    [AllowAnonymous]
    [Area("Customer")]
    [Route("Customer/[controller]/[action]")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
