using BankingApp.EntityLayer.Concrete;
using BankingApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Threading.Tasks;

namespace BankingApp.Presentation.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false, true);

                //if (result.Succeeded && appUser.EmailConfirmed == true)//E-mail onayı şartı
                if (result.Succeeded)//E-mail onayı şartı
                {
                    return RedirectToAction("Index", "CustomerProfile",new {area="Customer"});
                }
                else
                {
                    return RedirectToAction("SignIn", "Login");
                }
            }
                return View();
        }
    }
}
