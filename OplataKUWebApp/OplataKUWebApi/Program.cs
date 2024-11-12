
using AutoMapper;
using ClientInfoData;
using Microsoft.EntityFrameworkCore;

namespace OplataKUWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Add AutoMap
            var mapper = new MapperConfiguration(mc => mc.AddProfile<MapperProfile>())
                .CreateMapper();

            builder.Services.AddSingleton(mapper);

            // Add DataBase
            builder.Services.AddDbContext<ClientInfoContext>(options => options.UseSqlite("Data Source = clientinfo.db"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
