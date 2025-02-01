using BL.Extensions;
using DAL.Context;
using DAL.Extensions;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
builder.Services.AddSession();
builder.Services.AddIdentity<AppUser, AppRole>(opt => { })
    .AddRoleManager<RoleManager<AppRole>>()
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    config.LogoutPath = new PathString("/Admin/Auth/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "YTBlogSite",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest,
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(1);
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions()
{
    PositionClass = ToastPositions.TopRight,
    TimeOut = 3000,
}).AddRazorRuntimeCompilation();


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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapDefaultControllerRoute();
});
app.Run();
