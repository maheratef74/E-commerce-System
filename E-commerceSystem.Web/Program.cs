using E_commerceSystem.DataAccessLayer.DbContext;
using E_commerceSystem.DataAccessLayer.Entities;
using E_commerceSystem.DataAccessLayer.Repositories.ProductRepository;
using E_commerceSystem.DataAccessLayer.Repositories.UserRepository;
using E_commerceSystem.Services.CartService;
using E_commerceSystem.Services.CheckoutService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region DataBase Config
        
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information);
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICheckoutService , CheckoutService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddSession();

var app = builder.Build();

app.UseSession(); 

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();