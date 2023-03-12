using BankingApp.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using BankingApp.Presentation.Models;
using BankingApp.DTOLayer.DTOs.AppUserDTOs;
using System.Net.Mail;
using BankingApp.BusinessLayer.Features.Abstract;

namespace BankingApp.Presentation.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public RegisterController(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO p)
        {
            if (ModelState.IsValid)//Eğer model geçerli ise işleme devam etmek için
            {
                AppUser appUser = new AppUser()
                {
                    UserName = p.UserName,
                    Name = p.Name,
                    Surname = p.Surname,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    City = p.City,
                    Gender = p.Gender,
                    EmailCode = new Random().Next(10000, 1000000).ToString()
                };

                if (p.Password == p.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(appUser, p.Password);
                    if (result.Succeeded)
                    {
                        _emailService.SendEmailAsync(appUser.Email, appUser.EmailCode);
                        return RedirectToAction("EmailConfirmed", "Register");
                        //return RedirectToAction("SignIn", "Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Şifreler Uyuşmuyor");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EmailConfirmed()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailConfirmed(AppUser appUser)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);
            if (user.EmailCode == appUser.EmailCode)
            {
                user.EmailConfirmed = true;

                await _userManager.UpdateAsync(user);
                return RedirectToAction("SignIn", "Login");
            }

            return View();
        }
 
    }
}
