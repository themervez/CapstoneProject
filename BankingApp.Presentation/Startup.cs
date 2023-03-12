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
                opts.User.RequireUniqueEmail = true;//1 den fazla ayný email ile kaydý önlemek için

            }).AddErrorDescriber<CustomIdentityErrorValidator>().AddPasswordValidator<CustomPasswordValidator>().AddEntityFrameworkStores<BankingAppDbContext>().AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "BankingApp";
            cookieBuilder.HttpOnly = false;//Client tarafýnda Cookie okumayý engellemek için
            cookieBuilder.SameSite = SameSiteMode.Strict;//Sadece Name kýsmýnda belirtilensite üzerinden cookilere ulaþabilmek için//Lax:bu özelliði kapatýr,Strict: bu özelliði kýsar.CSRF'i önlemek için.
                                                         //Bankacýlýk uygulamalarýna yönelik olmasý için Strict kullandým.
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(2);//Oluþturulan Token ömrü:2 saat
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));//EmailSettings Konfigürasyonu

            services.ConfigureApplicationCookie(opts =>
            {
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(2);//Cooki'lerin kullanýcýnýn bilgisayarýnda kalma süresi
                //opts.LoginPath = new PathString("/Login/SignIn");//Authorization gerekli sayfalar için yönlendirme
                //opts.LogoutPath = new PathString("/Customer/Customer/Logout");
                //opts.Cookie = cookieBuilder;
                opts.SlidingExpiration = true;//Cookie ömrünün yarýsýnda kullanýcý eðer yeniden istek atarsa cookie ömrünü uzatacak
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
