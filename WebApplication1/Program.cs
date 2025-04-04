using Gyumri.Application.Interfaces;
using Gyumri.Application.Services;
using Gyumri.Data.Models;
using Gyumri.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    options.AddSupportedCultures(new string[] { "hy-Am", "hy-Am" })
//    .AddSupportedUICultures(new string[] { "ru-Ru", "ru-Ru" });
//});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var connectionString = builder.Configuration.GetConnectionString("ShopConnection");
if (connectionString.Contains("postgresql://"))
{
    // Parse Render's DATABASE_URL format:
    // postgresql://user:password@host:port/database
    var uri = new Uri(connectionString);
    var properConnectionString =
        $"Host={uri.Host};" +
        $"Database={uri.AbsolutePath.Trim('/')};" +
        $"Username={uri.UserInfo.Split(':')[0]};" +
        $"Password={uri.UserInfo.Split(':')[1]};" +
        $"Port={uri.Port};" +
        "SSL Mode=Require;Trust Server Certificate=true";

    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(properConnectionString));
}
else
{
    // Use regular connection string
    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(connectionString));
}
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ISubcategory, SubcategoryService>();
builder.Services.AddScoped<IPlace, PlaceService>(); 

builder.Services.AddLocalization();

var app = builder.Build();

//app.UseRequestLocalization();
app.UseMiddleware<LocalizationMiddleware>();

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

var supportedCultures = new[] { "en", "hy-AM", "ru-RU" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

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

app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
