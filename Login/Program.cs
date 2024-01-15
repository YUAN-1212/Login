using Domain.Model;
using Login;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Member}/{action=Login}/{id?}");

app.Run();
