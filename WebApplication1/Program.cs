using Gyumri.Application.Interfaces;
using Gyumri.Application.Services;
using Gyumri.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server = (localdb)\\mssqllocaldb;Database = userstoredb;Trusted_Connection=true";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ISubcategory, SubcategoryService>();


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "admin_default",
        pattern: "admin",
        defaults: new { area = "Admin", controller = "Dashboard", action = "Index" });

    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
