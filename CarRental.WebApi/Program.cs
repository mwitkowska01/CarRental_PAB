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


namespace CarRental.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // rejestracja automappera w kontenerze IoC
                builder.Services.AddAutoMapper(typeof(RentalMappingProfile));

                // rejestracja automatycznej walidacji (FluentValidation waliduje i przekazuje wynik przez ModelState)
                builder.Services.AddFluentValidationAutoValidation();

                // rejestracja kontekstu bazy w kontenerze IoC
                var sqliteConnectionString = "Data Source=C:\\Users\\majwi\\source\\repos\\CarRental2-master\\Rental.WebAPI.Logger.db";
                builder.Services.AddDbContext<RentalDbContext>(options =>
                    options.UseSqlite(sqliteConnectionString));

                // rejestracja walidatora
               // builder.Services.AddScoped<IValidator<CreateCarDto>, RegisterCreateProductDtoValidator>();

                // bulider Car
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





                //builder.Services.AddScoped<ExceptionMiddleware>();


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseStaticFiles();
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

//                app.UseMiddleware<ExceptionMiddleware>();

                app.UseHttpsRedirection();

                app.UseAuthorization();
                app.MapControllers();

                // seeding data
                using (var scope = app.Services.CreateScope())
                {
                    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                    dataSeeder.Seed();
                }

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }
    }
}
