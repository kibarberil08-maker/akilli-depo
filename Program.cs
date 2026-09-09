using AkilliDepo.Data;
using AkilliDepo.Models;
using Microsoft.EntityFrameworkCore;
namespace AkilliDepo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<DepoContext>(options =>
    options.UseSqlite("Data Source=akillidepo.db"));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.WebHost.UseUrls("http://0.0.0.0:5000");



            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DepoContext>();

                if (!context.Kullanicilar.Any())
                {
                    context.Kullanicilar.Add(new Kullanici
                    {
                        KullaniciAdi = "admin",
                        Sifre = "1234"
                    });

                    context.SaveChanges();
                }
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
