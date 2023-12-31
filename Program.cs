using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiAutores.Data;
using WebApiAutores.Filtros;
using WebApiAutores.Interface;
using WebApiAutores.Middleware;
using WebApiAutores.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opciones => {
    opciones.Filters.Add(typeof(FiltroExeception)); // agregando filtro de manera global
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("AppConnection");
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(connectionString));
builder.Services.AddScoped<ITareaService, ServicioA>();
builder.Services.AddTransient<ServicioTransitorio>();
builder.Services.AddScoped<ServicioAddScoped>();
builder.Services.AddSingleton<ServicioSinglenton>();
builder.Services.AddHostedService<EscribirArchivo>();

var app = builder.Build();

var servicioLoguer = (ILogger<Program>)app.Services.GetService(typeof(ILogger<Program>));

app.UserLoguearRespuesta();


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
