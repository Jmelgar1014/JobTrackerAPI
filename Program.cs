using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using JobTrackerApi.Models;
using JobTrackerApi.Profiles;
using JobTrackerApi.Repositories.Implementations;
using JobTrackerApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

// using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;
IdentityModelEventSource.LogCompleteSecurityArtifact = true;
// Add services to the container.

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8080); // Important for Fly.io
});
var connection = Environment.GetEnvironmentVariable("CONNECTIONSTRING_DEFAULTCONNECTION");
var jwtIssuer = Environment.GetEnvironmentVariable("SUPABASE_VALIDISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("SUPABASE_VALIDAUDIENCE");
var jwtSecret = Environment.GetEnvironmentVariable("SUPABASE_JWTSECRET");

builder.Services.AddAuthorization();

builder.Services.AddAuthentication().AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!)),
            ValidAudience = jwtAudience,
            ValidIssuer = jwtIssuer
        };
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection String is missing");



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connection));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactLocalHost", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IApplicationsRepository, ApplicationsRepository>();
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(ApplicationProfile));



var app = builder.Build();
// Add this right after var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Supabase URL: {builder.Configuration["Supabase:Url"]}");
Console.WriteLine($"JWT Secret exists: {!string.IsNullOrEmpty(builder.Configuration["Supabase:JwtSecret"])}");
Console.WriteLine($"Expected Issuer: {builder.Configuration["Supabase:Url"]}/auth/v1");
// Configure the HTTP request pipeline.

app.UseCors("AllowReactLocalHost");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     dbContext.Database.EnsureCreated(); // ✅ creates tables based on your models
// }


app.Run();
