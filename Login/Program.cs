using Domain.Model;
using Login;
using Microsoft.EntityFrameworkCore;

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
