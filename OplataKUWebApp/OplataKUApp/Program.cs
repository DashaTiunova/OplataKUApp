
using ClientInfoData;
using Microsoft.EntityFrameworkCore;
namespace OplataKUWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            // Add DataBase
            //builder.Services.AddDbContext<ClientInfoContext>(options => options.UseSqlite("Data Source = clientinfo.db"));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Client}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
