using Capa_Datos.Interfaces;
using Capa_Datos.Repositorio;
using Capa_Negocio.Interfaces;
using Capa_Negocio.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyecci�n de dependencias SERVICIO
builder.Services.AddScoped<IServicio, ServicioRepositorio>();
builder.Services.AddScoped<IServicioService, ServicioServicio>();

// Inyecci�n de dependencias PROVEEDOR
builder.Services.AddScoped<IProveedor, ProveedorRepositorio>();
builder.Services.AddScoped<IProveedorService, ProveedorServicio>();

//Inyectar dependencias CITAS
builder.Services.AddScoped<ICitaService, CitaServicio>(); // Servicio (l�gica de negocio)
builder.Services.AddScoped<ICita, CitaRepositorio>(); // Repositorio (acceso a datos)

// Inyecci�n de dependencias VETERINARIO
builder.Services.AddScoped<IVeterinario, VeterinarioRepositorio>();
builder.Services.AddScoped<IVeterinarioService, VeterinarioServicio>();

// Inyecci�n de dependencias PRODUCTO
builder.Services.AddScoped<IProducto, ProductoRepositorio>();
builder.Services.AddScoped<IProductoService, ProductoServicio>();

// Inyecci�n de dependencias MASCOTAS
builder.Services.AddScoped<IMascota, MascotaRepositorio>();
builder.Services.AddScoped<IMascotaService, MascotaServicio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
