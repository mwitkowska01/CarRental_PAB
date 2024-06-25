using CarRental.Application.IServices;
using CarRental.Application.Mapping;
using CarRental.Application.Services;
using CarRental.Domain.Contracts;
using CarRental.Infrastructure;
using CarRental.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;
using Radzen;

namespace CarRental.BlazorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddAutoMapper(typeof(RentalMappingProfile));


            // rejestracja kontekstu bazy w kontenerze IoC
            var sqliteConnectionString = "Data Source=C:\\Users\\majwi\\source\\repos\\CarRental2-master\\Rental.WebAPI.Logger.db";
            builder.Services.AddDbContext<RentalDbContext>(options =>
                 options.UseSqlite(sqliteConnectionString));


            builder.Services.AddScoped<IRentalUnitOfWork, RentalUnitOfWork>();


            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IContractorRepository, ContractorRepository>();
            builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<DataSeeder>();

            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IPersonelService, PersonelService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IContractorService, ContractorService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddRadzenComponents();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // seeding data
            using (var scope = app.Services.CreateScope())
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                dataSeeder.Seed();
            }

            app.Run();
        }
    }
}