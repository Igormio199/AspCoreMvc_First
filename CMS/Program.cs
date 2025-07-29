using Microsoft.EntityFrameworkCore;
using CMS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

//Включает поддержку сессий
//Должен вызываться после UseCookiePolicy() и перед UseMvc()
//app.UseSession()(сессии)



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

// Настройка политик авторизации
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware статичных файлов css, js, img из папки wwwroot
app.UseStaticFiles();
// Middleware для перенаправления с http на https
app.UseHttpsRedirection();
// Middleware маршрутизация
app.UseRouting();
app.MapStaticAssets();
// Важно: UseAuthentication перед UseAuthorization
//аутентификация
app.UseAuthentication();
//авторизация
app.UseAuthorization();

// ошибки или статусы 404, 401, 502 и тд.
app.UseStatusCodePagesWithReExecute("/Error/{0}"); // Маршрут для обработки ошибок
//app.UseExceptionHandler("/Error/500"); // Для обработки исключений

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
