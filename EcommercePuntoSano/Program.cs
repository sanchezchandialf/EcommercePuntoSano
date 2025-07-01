
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository;
using Ecommerce.DataAccess.Repository.Irepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocal",
        policy => policy
            .WithOrigins("https://localhost:7207")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// EF Core con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34))
    ));

builder.Services.AddRazorPages();
builder.Services.AddControllers(); // 👈 ¡IMPORTANTE!
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowFrontendLocal"); 

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); 

app.Run();
