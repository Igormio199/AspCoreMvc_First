using Microsoft.EntityFrameworkCore;
using CMS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

//�������� ��������� ������
//������ ���������� ����� UseCookiePolicy() � ����� UseMvc()
//app.UseSession()(������)



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

// ��������� ������� �����������
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware ��������� ������ css, js, img �� ����� wwwroot
app.UseStaticFiles();
// Middleware ��� ��������������� � http �� https
app.UseHttpsRedirection();
// Middleware �������������
app.UseRouting();
app.MapStaticAssets();
// �����: UseAuthentication ����� UseAuthorization
//��������������
app.UseAuthentication();
//�����������
app.UseAuthorization();

// ������ ��� ������� 404, 401, 502 � ��.
app.UseStatusCodePagesWithReExecute("/Error/{0}"); // ������� ��� ��������� ������
//app.UseExceptionHandler("/Error/500"); // ��� ��������� ����������

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
