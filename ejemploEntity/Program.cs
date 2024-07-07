using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProducto, ProductoServices>();
builder.Services.AddScoped<ICatalogo, CatalogoServices>();
builder.Services.AddScoped<ICliente, ClienteServices>();
builder.Services.AddScoped<IVentas, VentaServices>();

builder.Services.AddDbContext<TestContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    }
));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
