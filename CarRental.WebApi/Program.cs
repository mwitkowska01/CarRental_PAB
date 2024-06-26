using CarRental.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog;
using CarRental.Application.Mapping;
using FluentValidation.AspNetCore;
using CarRental.Domain.Contracts;
using CarRental.Infrastructure.Repositories;
using CarRental.Application.Services;
using CarRental.Application.IServices;
using CarRental.WebApi.Middleware;

namespace CarRental.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddAutoMapper(typeof(RentalMappingProfile));
                builder.Services.AddFluentValidationAutoValidation();
                var sqliteConnectionString = @"Data Source=C:\Users\majwi\source\repos\CarRental2-master\Rental.WebAPI.Logger.db";
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

                builder.Services.AddScoped<ExceptionMiddleware>();

                builder.Services.AddCors(o => o.AddPolicy("SaleKiosk", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }));

                var app = builder.Build();

                app.UseStaticFiles();
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.UseCors("SaleKiosk");

                using (var scope = app.Services.CreateScope())
                {
                    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                    dataSeeder.Seed();
                }

                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }


        }
    }
}
