using MgeniTrack.Models;
using MgeniTrack.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MgenitrackContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(5); //session expires after 5 mins
        options.SlidingExpiration = true; // resets timer on activity

        options.Cookie.HttpOnly = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ActivityLogService>(); 
builder.Services.AddScoped<DashboardNotifier>();
builder.Services.AddScoped<VisitAnalyticsService>();
builder.Services.AddSignalR();

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



app.MapHub<MgeniTrack.Hubs.DashboardHub>("/dashboardHub");

//smart route direct
app.MapGet("/", async (MgenitrackContext db, HttpContext ctx) =>
{
    bool hasUsers = await db.Users.AnyAsync();
    ctx.Response.Redirect(hasUsers ? "/Account/Login" : "/Users/Create");
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
