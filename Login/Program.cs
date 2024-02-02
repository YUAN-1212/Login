using Application.Member.Dto;
using Domain.Model;
using Login;
using Login.Filter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// �l�[AddRazorRuntimeCompilation�A�e�ݤ~��Y�ɧ�s
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// DI�`�J
builder.Services.AddServices();

// �t�mSQLite��Ʈw�s�u
// �ݭn�w�� EntityFrameworkCore ��Nuget�M��
builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DBConnection"))
);

//Added for session state
builder.Services.AddDistributedMemoryCache();

// �[�J Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// �s�W cookie ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //���n�J�ɷ|�۰ʾɨ�o�Ӻ��}
        options.LoginPath = new PathString("/Member/Login/");

        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(AuthorizationFilter));
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

//�]�w�w�]���A�n�b UseStaticFiles ���e
//app.UseDefaultFiles();
//�ҥ� wwwroot �R�A�ɮץ\��
//app.UseFileServer();
app.UseStaticFiles();

app.UseRouting();

// �ҥΨ����{��
app.UseAuthentication();
app.UseAuthorization();

//app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Member}/{action=Login}/{id?}");

//�[�J�U��o�@��ϥ�session
app.UseSession();

app.Run();
