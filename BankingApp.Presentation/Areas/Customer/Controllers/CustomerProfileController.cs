﻿using BankingApp.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using BankingApp.Presentation.Areas.Customer.Models;

namespace BankingApp.Presentation.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Customer/[controller]/[action]")]
    public class CustomerProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomerProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileViewModel profileView = new ProfileViewModel();
            profileView.Name = values.Name;
            profileView.Surname = values.Surname;
            profileView.PhoneNumber = values.PhoneNumber;
            profileView.Email = values.Email;
            return View(profileView);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();//Dosyanın anlık bulunduğu konumu için
                var extension = Path.GetExtension(p.Image.FileName);//Resim dosyasının pathi
                var imageName = Guid.NewGuid() + extension;//Benzersiz dosya isimleri için
                var savelocation = resource + "/wwwroot/Images/" + imageName;//Resmin kaydedileceği konum
                var stream = new FileStream(savelocation, FileMode.Create);//Resmin kaydolacağı kısım ve dosyanın erişim türü(okuma,yazma,oluşturma,..)
                await p.Image.CopyToAsync(stream);
                values.ImageURL = imageName;
            }
            values.Name = p.Name;
            values.Surname = p.Surname;
            values.Email = p.Email;
            values.PhoneNumber = p.PhoneNumber;
            values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, p.Password);

            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn", "Login");
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
            return View();
        }
    }
}
