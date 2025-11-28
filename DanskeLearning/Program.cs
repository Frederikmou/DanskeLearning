using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DanskeLearning;
using DanskeLearning.Services.SubjectService;
using DanskeLearning.Services.DashboardService;
using DanskeLearning.Services.LoginService;
using DanskeLearning.Services.SubjectService;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddSingleton<IDashboardService, DashboardService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5231/") // <-- API port
});
builder.Services.AddScoped<IDashboardService, DashboardServiceHttp>();
builder.Services.AddScoped<ISubjectService, SubjectServiceHttp>();
builder.Services.AddScoped<ILoginService, LoginService>();



await builder.Build().RunAsync();