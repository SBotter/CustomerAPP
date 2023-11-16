global using CustomerAPP.Client.Services.CustomerService;
global using CustomerAPP.Client.Index.CustomerIndex;
global using CustomerAPP.Shared;
global using System.Runtime.Serialization;
using CustomerAPP.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerIndex, CustomerIndex>();

await builder.Build().RunAsync();
