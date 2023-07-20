﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoeShop.Business.Abstract;
using ShoeShop.Business.Concrete;
using ShoeShop.Businness.Abstract;
using ShoeShop.Businness.Concrete;
using ShoeShop.Businness.MapperProfile;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.DataAccess.Concrete.Data;
using ShoeShop.DataAccess.Concrete.Data.Repository;
using ShoeShop.DataAccess.Concrete.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopShop
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
            services.AddControllersWithViews();
            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IBrandRepository, EfBrandRepository>();
            services.AddScoped<IColorRepository, EfColorRepository>();
            services.AddScoped<IColorService, ColorManager>();
            services.AddScoped<IGenderRepository, EfGenderRpository>();
            services.AddScoped<IGenderService, GenderManager>();
            services.AddScoped<IOrderRepository, EfOrderRepository>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IStockRepository, EfStockRepository>();
            services.AddScoped<IStockService, StockManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IFavoriteRepository, EfFavoriteRepository>();
            services.AddScoped<IFavoriteService, FavoriteManager>();
            services.AddScoped<IPaymentCardService, PaymentCardManager>();
            services.AddScoped<IPaymentCardRepository, EfPaymentCardRepository>();

            var connectionString = Configuration.GetConnectionString("db");
            services.AddDbContext<ShoeShopDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddAutoMapper(typeof(MapProfile));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/Users/Login";
                    opt.AccessDeniedPath = "/Users/AccessDenied";
                });
            services.AddSession();
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
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
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "",
                   pattern: "Favorilerim",
                   defaults: new { controller = "Favorite", action = "Show" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Stok/Ekle",
                    defaults: new { controller = "Stock", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Markalar/Sil/{id}",
                    defaults: new { controller = "Brand", action = "Delete", id = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Markalar/Ekle",
                    defaults: new { controller = "Brand", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Markalar/D�zenle/{id}",
                    defaults: new { controller = "Brand", action = "Edit", id = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Markalar/",
                    defaults: new { controller = "Brand", action = "Show" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Kategoriler/Sil/{id}",
                    defaults: new { controller = "Category", action = "Delete", id = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Kategoriler/Ekle",
                    defaults: new { controller = "Category", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Kategoriler/D�zenle/{id}",
                    defaults: new { controller = "Category", action = "Edit", id = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Kategoriler/",
                    defaults: new { controller = "Category", action = "Show" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "�r�nler/Ekle",
                    defaults: new { controller = "Products", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "�r�nler/D�zenle/{id}",
                    defaults: new { controller = "Products", action = "Edit", id = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "�r�nler/Sayfa/{page}",
                    defaults: new { controller = "Products", action = "Show", page = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Anasayfa/{catName}/Sayfa{page}",
                    defaults: new { controller = "Home", action = "Index", page = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Sepetim",
                    defaults: new { controller = "Cart", action = "Basket" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Anasayfa/Sayfa{page}",
                    defaults: new { controller = "Home", action = "Index", page = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Urunler/Sayfa{page}",
                    defaults: new { controller = "Products", action = "Show", page = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Urunler/Sayfa{page}",
                    defaults: new { controller = "Cart", action = "Basket" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{username}/Bilgiler",
                    defaults: new { controller = "Users", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{catName}/Sayfa{page}",
                    defaults: new { controller = "Home", action = "Index", page = 1 });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Detaylar/{productID}",
                    defaults: new { controller = "Products", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{catName}",
                    defaults: new { controller = "Home", action = "Index", page = 1 });



                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               


            });
        }
    }
}
