using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DanskeLearning;
using DanskeLearning.Services.DashboardService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddSingleton<IDashboardService, DashboardService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5231/") // <-- API port
});
builder.Services.AddScoped<IDashboardService, DashboardServiceHttp>();


await builder.Build().RunAsync();