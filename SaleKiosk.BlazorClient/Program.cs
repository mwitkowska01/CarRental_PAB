using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using CarRental.BlazorClient.Services;
using SaleKiosk.BlazorClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// rejestracja ProductService w kontenerze zale¿noœci
builder.Services.AddScoped<IServiceService, ServiceService>();

// rejestracja CartService w kontenerze zale¿noœci
builder.Services.AddScoped<ICartService, CartService>();

// rejestracja OrderService w kontenerze zale¿noœci
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddRadzenComponents();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// modyfikacja klienta http aby pobiera³ dane z pliku konfiguracyjnego 
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("CarRentalAPIUrl"))
});


builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
