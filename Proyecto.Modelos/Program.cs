using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Modelos.RepositorioDappers;
using Proyecto.Modelos.Repositorios;
using Proyecto.Web.Api.Repositorios;
using Microsoft.OpenApi.Models;
using Proyecto.Modelos.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<IFuncionRepository, FuncionRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Servicios
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<QrService>();

// Cadena de conexión
string connStr = builder.Configuration.GetConnectionString("MySqlConnection")
                 ?? throw new InvalidOperationException("Cadena de conexión no encontrada");

var repo = new LocalRepository(connStr);

// Configuración de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestor de Eventos", Version = "v1" });
});

var app = builder.Build();

// URL base
app.Urls.Add("http://localhost:5001");

// Swagger habilitado SIEMPRE
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestor de Eventos v1");
    c.RoutePrefix = "swagger"; // disponible en /swagger
});

app.UseAuthentication();
app.UseAuthorization();

// Redirección desde raíz a swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Mapear controladores
app.MapControllers();

app.Run();
