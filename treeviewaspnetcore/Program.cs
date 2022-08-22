using Microsoft.EntityFrameworkCore;
using treeviewaspnetcore.Models;
var builder = WebApplication.CreateBuilder(args);

// Kapsay�c�ya hizmetler ekleme.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//HTTP istek ard���k d�zenini yap�land�r�lmas�.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
