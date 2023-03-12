using AutoMapper;
using BankingApp.BusinessLayer.Features.Abstract;
using BankingApp.DTOLayer.DTOs.AppUserDTOs;
using BankingApp.EntityLayer.Concrete;
using BankingApp.Presentation.Models;
using BankingApp.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Threading.Tasks;

namespace BankingApp.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public LoginController(SignInManager<AppUser> signInManager, IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult SignIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTO p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(p.UserName);
                if (user != null)
                {
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız Bir Süreliğine Kilitlenmiştir. Lütfen Daha Sonra Tekrar Deneyiniz!");
                        return View(p);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, p.Password, p.RememberMe, false);//Checkbox'tan alınan beni hatırla özelliğine göre Cookie'nin ömrünü belirledik

                    if (result.Succeeded && user.EmailConfirmed == true)//Email Confirmation
                    {
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());//Kullancının Login sayfasına gitmeden önceki sayfaya geri dönmesini sağladık
                        }
                        return RedirectToAction("Index", "Account", new { area = "Customer" });//Change it
                    }
                    else
                    {
                        return RedirectToAction("SignIn", "Login");
                    }
                }
            }
                return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel p)
        {
            var hasUser = await _userManager.FindByEmailAsync(p.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu E-mail adresine sahip kullanıcı bulunamamıştır!");//validation-summary kısmında gözükecek hatalardan
                return View();
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);//user bilgilerinden oluşan token

            var PasswordResetLink = Url.Action("ResetPassword", "Login", new
            {
                userId = hasUser.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);

            //Link Example: https://localhost:38512?userId=16453&token=dfghjkclm

            await _emailService.SendResetPasswordEmail(PasswordResetLink, hasUser.Email);

            TempData["SuccessMessage"] = "Şifre yenileme linki e-posta adresinize gönderilmiştir.";
            return RedirectToAction(nameof(ForgotPassword), "Login");

        }

        public IActionResult ResetPassword(string userId, string token)//Post işleminde kullanabilmek için userıd ve token ı temp data ile httpost yapılan metoda gönderiyoruz//Framework query string ile otomatik olarak bu parametreleri mapleyecek
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel p)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];

            if (userId == null || token == null)
            {
                throw new Exception("Bir hata meydana geldi!");
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanıcı bulunamamıştır.");
                return View();
            }
            //var code = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            //var result = await _userManager.ResetPasswordAsync(hasUser, code!.ToString(), p.Password);
            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, p.Password);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(hasUser);//SecurityStamp değeri kullanıcının eski bilgileri ile sistemde aktif olmaması için güncellendi
                @TempData["SuccessMessage"] = "Şifreniz başarıyla yenilenmiştir.";
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
    }
}
