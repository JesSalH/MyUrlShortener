using Microsoft.EntityFrameworkCore;
using MyUrlShortener.DataAccess.Data;
using MyUrlShortener.DataAccess.Repositories;
using MyUrlShortener.DataAccess.Repositories.IRepositories;
using MyUrlShortener.Middleware;
using MyUrlShortener.Services.UrlShortener;

namespace MyUrlShortener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")
                   ));
            builder.Services.AddControllersWithViews();


            builder.Services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
            builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseExceptionHandlingMiddleware();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}