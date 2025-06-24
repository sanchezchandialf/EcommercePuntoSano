
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository;
using Ecommerce.DataAccess.Repository.Irepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de CORS (por si hacés peticiones desde React, JS, etc.)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocal",
        builder => builder
            .WithOrigins("https://localhost:7207")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Configuración de EF Core con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 34))
    ));

// Agregar servicios de Razor Pages
builder.Services.AddRazorPages();
//Agregar reposositorios al contenedor de Inyeccion de Dependencias
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();



// Configurar middleware del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseCors("AllowFrontendLocal");
// Habilitar redirección HTTPS
app.UseHttpsRedirection();

// Habilitar uso de archivos estáticos (CSS, JS, etc.)
app.UseStaticFiles();

// Habilitar routing
app.UseRouting();

// Aplicar política de CORS
app.UseCors("AllowLocalhost");

// Autorización (si es que usás login o roles)
app.UseAuthorization();

// Mapear Razor Pages
app.MapRazorPages();

app.Run();
