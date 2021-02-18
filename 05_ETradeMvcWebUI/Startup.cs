using AppCore.DataAccess.Configs;
using ETradeBusiness.Services;
using ETradeBusiness.Services.Bases;
using ETradeDataAccess.EntityFramework;
using ETradeDataAccess.EntityFramework.Repositories;
using ETradeDataAccess.EntityFramework.Repositories.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ETradeMvcWebUI
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config => //�erezler �zerinden �al��t�rd�k uygulamay� �ok daha g�venli 
                {
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                });

            services.AddSession();

            #region IoC Container
            
            // services.AddScoped() // istek (request) boyunca objenin referans�n� kulland���m�z yerde olu�turulan objeler hayatta kal�r. *** En mant�kl� ve az maliyetli olan bu olur ***
            // services.AddSingleton() // Web uygulamas� ba�lad���nda objenin referans�n� kulland���m�z yerde obje bir kere olu�ur ve uygulama �al��t��� s�rece bu obje hayatta kal�r.
            // services.AddTransient() // istek (request) ba��ms�z ihtiya� olan (objenin referans�n� kulland���m�z) her durumda obje'yi new'ler.

            ConnectionConfig.ConnectionString = Configuration.GetConnectionString("ETradeContext");
            services.AddScoped<DbContext, ETradeContext>(); // DbContext tipinde kullan�lan her referans� ETradeContext objesi olarak new'ler
            services.AddScoped<ProductRepositoryBase, ProductRepository>();
            services.AddScoped<CategoryRepositoryBase, CategoryRepostory>();
            services.AddScoped<UserRepositoryBase, UserRepository>();
            services.AddScoped<CountryRepositoryBase, CountryRepostory>();
            services.AddScoped<CityRepositoryBase, CityRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILocationService, LocationService>();

            #endregion

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

            // bu sen kimsin diye soruyor...
            app.UseAuthentication();

            // bu da i�lemler i�in yetkili misin diye soruyor
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
