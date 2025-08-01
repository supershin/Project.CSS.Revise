﻿using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Respositories;
using Project.CSS.Revise.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add http context accessor for BaseController
builder.Services.AddHttpContextAccessor();

// Add SQL Server database context
builder.Services.AddDbContext<CSSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CSSStrings")));

// Add Config appsetting.json
builder.Services.AddOptions();
builder.Services.Configure<UserProfile>(builder.Configuration.GetSection("UserProfile"));

// Add Services Scope
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepo, LoginRepo>();
builder.Services.AddScoped<IMasterRepo, MasterRepo>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IShopAndEventRepo, ShopAndEventRepo>();
builder.Services.AddScoped<IShopAndEventService, ShopAndEventService>();

// Add Rate Limiting Middleware
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Add Authentication Settings
builder.Services.Configure<AuthenticationSettings>(builder.Configuration.GetSection("AuthenticationSettings"));

builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // ต้องมีตัวนี้ก่อน UseAuthorization
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Upload")),
    RequestPath = "/Upload"
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
