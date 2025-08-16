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

//Inyectar dependencias 
builder.Services.AddScoped<ICitaService, CitaServicio>(); // Servicio (lógica de negocio)
builder.Services.AddScoped<ICita, CitaRepositorio>(); // Repositorio (acceso a datos)

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
