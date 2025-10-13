using MySql.Data.MySqlClient;
using System.Data;
using Proyecto.Modelos.Repositorios.ReposDapper;
using Proyecto.Web.Api.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a MySQL
builder.Services.AddTransient<IDbConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar todos los repositorios Dapper
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<OrdenRepository>();
builder.Services.AddScoped<DetalleOrdenRepository>();
builder.Services.AddScoped<EntradaRepository>();
builder.Services.AddScoped<EventoRepository>();
builder.Services.AddScoped<FuncionRepository>();
builder.Services.AddScoped<LocalRepository>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<SectorRepository>();
builder.Services.AddScoped<TarifaRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioRolRepository>();

var app = builder.Build();

app.MapClienteEndpoints(app.Services.GetRequiredService<ClienteRepository>());
app.MapOrdenEndpoints(app.Services.GetRequiredService<OrdenRepository>());
app.MapDetalleOrdenEndpoints(app.Services.GetRequiredService<DetalleOrdenRepository>());
app.MapEntradaEndpoints(app.Services.GetRequiredService<EntradaRepository>());
app.MapEventosEndpoints(app.Services.GetRequiredService<EventoRepository>());
app.MapFuncionEndpoints(app.Services.GetRequiredService<FuncionRepository>());
app.MapLocalEndpoints(app.Services.GetRequiredService<LocalRepository>());
app.MapRolEndpoints(app.Services.GetRequiredService<RolRepository>());
app.MapSectorEndpoints(app.Services.GetRequiredService<SectorRepository>());
app.MapTarifaEndpoints(app.Services.GetRequiredService<TarifaRepository>());
app.MapUsuarioEndpoints(app.Services.GetRequiredService<UsuarioRepository>());
app.MapUsuarioRolEndpoints(app.Services.GetRequiredService<UsuarioRolRepository>());

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
