using Server.Repositories.ArticleRepository;
using Server.Repositories.DashboardRepository;
using Server.Repositories.MyGrowthRepo;
using Server.Repositories.SubjectRepository;
using Server.Repositories.UserRepository;
using Server.Repositories.TestReposi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDashboardRepo, DashboardRepo>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITestRepo, TestRepo>();
builder.Services.AddScoped<IArticlesRepo,  ArticlesRepo>();
builder.Services.AddScoped<IMyGrowthRepo, MyGrowthRepo>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("policy");

app.MapControllers();

app.Run();