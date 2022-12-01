using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	builder.Configuration.GetConnectionString("ConnectionDb")
));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(30);
	options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "products",
	pattern: "/products/{categorySlug?}",
	defaults: new {controller = "Products", action = "Index"});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
SeedData.SeedDatabase(context);

app.Run();
