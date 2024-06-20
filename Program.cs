using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLyNhanSuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyNhanSu")));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/TaiKhoans/Login";
        options.LogoutPath = "/TaiKhoans/Logout";
        options.AccessDeniedPath = "/TaiKhoans/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TaiKhoans}/{action=Login}/{id?}");

app.Run();
