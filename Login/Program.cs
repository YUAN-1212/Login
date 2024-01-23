using Domain.Model;
using Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 追加AddRazorRuntimeCompilation，前端才能即時更新
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// DI注入
builder.Services.AddServices();

// 配置SQLite資料庫連線
// 需要安裝 EntityFrameworkCore 的Nuget套件
builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DBConnection"))
);

//Added for session state
builder.Services.AddDistributedMemoryCache();

// 加入 Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 新增 cookie 驗證
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
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

app.UseAuthentication();
app.UseAuthorization();

//app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Member}/{action=Login}/{id?}");

//加入下方這一行使用session
app.UseSession();

app.Run();
