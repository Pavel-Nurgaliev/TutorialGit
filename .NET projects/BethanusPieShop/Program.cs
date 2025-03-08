using BethanysPieShop.Models;
using BethanysPieShop.Models.DbInitializer;
using BethanysPieShop.Models.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

DbInitializer.Seed(app);

app.Run();
