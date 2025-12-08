using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DanskeLearning;
using DanskeLearning.Services.ArticleService;
using DanskeLearning.Services.ArticleReadStatusService;
using DanskeLearning.Services.SubjectService;
using DanskeLearning.Services.DashboardService;
using DanskeLearning.Services.LoginService;
using DanskeLearning.Services.SubjectService;
using DanskeLearning.Services.UserSessionService;
using DanskeLearning.Services.MyGrowthService;
using DanskeLearning.Services.TestService;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddSingleton<IDashboardService, DashboardService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7148/") // <-- API port
});

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<IDashboardService, DashboardServiceHttp>();
builder.Services.AddScoped<ISubjectService, SubjectServiceHttp>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();
builder.Services.AddScoped<IArticlesService,  ArticlesService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IMyGrowthService, MyGrowthHttp>();
builder.Services.AddScoped<IArticleReadStatusService, ArticleReadStatusService>();



await builder.Build().RunAsync();