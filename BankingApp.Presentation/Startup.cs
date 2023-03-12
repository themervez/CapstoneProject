using BankingApp.BusinessLayer.Configuration.DIContainer;
using BankingApp.BusinessLayer.Configuration.Mapper;
using BankingApp.BusinessLayer.Configuration.Validation.CustomValidations;
using BankingApp.BusinessLayer.Features.Abstract;
using BankingApp.BusinessLayer.Features.Concrete;
using BankingApp.BusinessLayer.Features.OptionsModels;
using BankingApp.DAL.DbContexts;
using BankingApp.EntityLayer.Concrete;
using BankingApp.Presentation.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BankingApp.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ContainerDependencies();//DI

            services.AddDbContext<BankingAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConStr"));
            });

            services.AddIdentity<AppUser, AppRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;//1 den fazla ayn� email ile kayd� �nlemek i�in

            }).AddErrorDescriber<CustomIdentityErrorValidator>().AddPasswordValidator<CustomPasswordValidator>().AddEntityFrameworkStores<BankingAppDbContext>().AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "BankingApp";
            cookieBuilder.HttpOnly = false;//Client taraf�nda Cookie okumay� engellemek i�in
            cookieBuilder.SameSite = SameSiteMode.Strict;//Sadece Name k�sm�nda belirtilensite �zerinden cookilere ula�abilmek i�in//Lax:bu �zelli�i kapat�r,Strict: bu �zelli�i k�sar.CSRF'i �nlemek i�in.
                                                         //Bankac�l�k uygulamalar�na y�nelik olmas� i�in Strict kulland�m.
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(2);//Olu�turulan Token �mr�:2 saat
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));//EmailSettings Konfig�rasyonu

            services.ConfigureApplicationCookie(opts =>
            {
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(2);//Cooki'lerin kullan�c�n�n bilgisayar�nda kalma s�resi
                //opts.LoginPath = new PathString("/Login/SignIn");//Authorization gerekli sayfalar i�in y�nlendirme
                //opts.LogoutPath = new PathString("/Customer/Customer/Logout");
                //opts.Cookie = cookieBuilder;
                opts.SlidingExpiration = true;//Cookie �mr�n�n yar�s�nda kullan�c� e�er yeniden istek atarsa cookie �mr�n� uzatacak
            });

            services.CustomizeValidator();

            services.AddControllersWithViews().AddFluentValidation();

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new MapperProfile());//AutoMapper configuration
            });
            services.AddMvc(config => /*Authorization*/
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Areas Configuration
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
