using OplataKUWebApp.Services;
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
            // Add services to the container.5012
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ClientApiService>();
            //http client
            builder.Services.AddHttpClient("ClientApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://localhost:5012");
            }
            );
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
