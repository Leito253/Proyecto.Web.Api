using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Core.Entidades;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Servicios;
using Proyecto.Modelos.Repositorios.ReposDapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Carga la configuración (esto ya lo hace por defecto)
var configuration = builder.Configuration;


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Eventos API",
        Version = "v1",
        Description = "API para sistema de gestión de entradas QR",
        Contact = new OpenApiContact
        {
            Name = "sisas Team",
            Email = "soporte@appqr.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<IFuncionRepository, FuncionRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITarifaRepository, TarifaRepository>();

// Servicios
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<QrService>();

// Cadena de conexión
string connStr = builder.Configuration.GetConnectionString("MySqlConnection")
                 ?? throw new InvalidOperationException("Cadena de conexión no encontrada");

// Autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateLifetime = true
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Eventos API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));

/* ===========================
 *      ENDPOINTS API
 * ===========================*/

#region CLIENTES
app.MapGet("/api/clientes", ([FromServices] IClienteRepository repo) => Results.Ok(repo.GetAll()));

app.MapGet("/api/clientes/{id}", (int idCliente, [FromServices] IClienteRepository repo) =>
{
    var item = repo.GetById(idCliente);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

app.MapPost("/api/clientes", (Cliente c, [FromServices] IClienteRepository repo) =>
{
    repo.Add(c);
    return Results.Created($"/api/clientes/{c.DNI}", c);
});

app.MapPut("/api/clientes/{id}", (int idCliente, Cliente c, [FromServices] IClienteRepository repo) =>
{
    if (idCliente != c.DNI) return Results.BadRequest();
    repo.Update(c);
    return Results.NoContent();
});

app.MapDelete("/api/clientes/{id}", (int idCliente, [FromServices] IClienteRepository repo) =>
{
    repo.Delete(idCliente);
    return Results.NoContent();
});
#endregion

#region ORDENES
app.MapGet("/api/ordenes", ([FromServices] IOrdenRepository repo) => Results.Ok(repo.GetAll()));

app.MapGet("/api/ordenes/{id}", (int idOrden, [FromServices] IOrdenRepository repo) =>
{
    var item = repo.GetById(idOrden);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

app.MapPost("/api/ordenes", (Orden o, [FromServices] IOrdenRepository repo) =>
{
    repo.Add(o);
    return Results.Created($"/api/ordenes/{o.idOrden}", o);
});

app.MapPut("/api/ordenes/{id}", (int idOrden, Orden o, [FromServices] IOrdenRepository repo) =>
{
    if (idOrden != o.idOrden) return Results.BadRequest();
    repo.Update(o);
    return Results.NoContent();
});
#endregion

#region ENTRADAS
app.MapGet("/api/entradas", ([FromServices] IEntradaRepository repo) => Results.Ok(repo.GetAll()));

app.MapPost("/api/entradas", (Entrada e, [FromServices] IEntradaRepository repo) =>
{
    repo.Add(e);
    return Results.Created($"/api/entradas/{e.idEntrada}", e);
});
#endregion

#region EVENTOS
app.MapGet("/api/eventos", ([FromServices] IEventoRepository repo) => Results.Ok(repo.GetAll()));

app.MapPost("/api/eventos", (Evento e, [FromServices] IEventoRepository repo) =>
{
    repo.Add(e);
    return Results.Created($"/api/eventos/{e.idEvento}", e);
});
#endregion

#region FUNCIONES
app.MapGet("/api/funciones", ([FromServices] IFuncionRepository repo) => Results.Ok(repo.GetAll()));

app.MapPost("/api/funciones", (Funcion f, [FromServices] IFuncionRepository repo) =>
{
    repo.Add(f);
    return Results.Created($"/api/funciones/{f.IdFuncion}", f);
});
#endregion

#region LOCAL
app.MapGet("/api/locales", ([FromServices] ILocalRepository repo) => Results.Ok(repo.GetAll()));

app.MapPost("/api/locales", (Local l, [FromServices] ILocalRepository repo) =>
{
    repo.Add(l);
    return Results.Created($"/api/locales/{l.idLocal}", l);
});
#endregion

#region ROLES
app.MapGet("/api/roles", ([FromServices] IRolRepository repo) => Results.Ok(repo.GetAll()));

app.MapPost("/api/roles", (Rol r, [FromServices] IRolRepository repo) =>
{
    repo.Add(r);
    return Results.Created($"/api/roles/{r.IdRol}", r);
});
#endregion

#region SECTORES
app.MapGet("/api/sectores", ([FromServices] ISectorRepository repo) => Results.Ok(repo.GetAll()));

app.MapPost("/api/sectores", (Sector s, [FromServices] ISectorRepository repo) =>
{
    repo.Add(s);
    return Results.Created($"/api/sectores/{s.idSector}", s);
});
#endregion

#region TARIFAS
app.MapGet("/api/funciones/{funcionId}/tarifas", (int IdFuncion, [FromServices] ITarifaRepository repo) =>
{
    var tarifas = repo.GetByFuncionId(IdFuncion);
    return Results.Ok(tarifas);
});

app.MapPost("/api/tarifas", (Tarifa t, [FromServices] ITarifaRepository repo) =>
{
    repo.Add(t);
    return Results.Created($"/api/tarifas/{t.idTarifa}", t);
});
#endregion

#region USUARIOS
app.MapGet("/usuarios/{id}", (int IdUsuario, [FromServices] IUsuarioRepository repo) =>
{
    var usuario = repo.GetById(IdUsuario);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
});

app.MapPost("/auth/login", (Usuario request, [FromServices] IUsuarioRepository repo) =>
{
    var usuario = repo.Login(request.Email, request.Contrasena);
    return usuario is not null ? Results.Ok(usuario) : Results.Unauthorized();
});

app.MapPost("/usuarios", (Usuario nuevoUsuario, [FromServices] IUsuarioRepository repo) =>
{
    repo.Add(nuevoUsuario);
    return Results.Created($"/usuarios/{nuevoUsuario.IdUsuario}", nuevoUsuario);
});

app.MapGet("/usuarios/{id}/roles", (int IdUsuario, [FromServices] IUsuarioRepository repo) =>
{
    var roles = repo.GetRoles(IdUsuario);
    return Results.Ok(roles);
});

app.MapGet("/roles", ([FromServices] IUsuarioRepository repo) =>
{
    var roles = repo.GetAllRoles();
    return Results.Ok(roles);
});

app.MapPost("/usuarios/{id}/roles/{rolId}", (int IdUsuario, int IdRol, [FromServices] IUsuarioRepository repo) =>
{
    repo.AsignarRoles(IdUsuario, IdRol);
    return Results.Ok(new { mensaje = "Rol asignado correctamente" });
});
#endregion

#region QR
// GET /entradas/{idEntrada}/qr
app.MapGet("/entradas/{idEntrada}/qr", (int idEntrada, QrService qrService, IEntradaRepository repo) =>
{
    var entrada = repo.GetById(idEntrada);
    if (entrada == null) return Results.NotFound("Entrada no existe");

    string qrContent = $"{entrada.idEntrada}|{entrada.idFuncion}|{entrada.idCliente}|{builder.Configuration["Qr:Key"]}";
    var qrBytes = qrService.GenerarQrEntradaImagen(qrContent);

    return Results.File(qrBytes, "image/png");
});

// POST /qr/lote
app.MapPost("/qr/lote", (List<int> idEntradas, QrService qrService, IEntradaRepository repo) =>
{
    var resultados = new Dictionary<int, byte[]>();

    foreach (var idEntrada in idEntradas)
    {
        var entrada = repo.GetById(idEntrada);
        if (entrada == null) continue;

        string qrContent = $"{entrada.idEntrada}|{entrada.idFuncion}|{entrada.idCliente}|{builder.Configuration["Qr:Key"]}";
        var qrBytes = qrService.GenerarQrEntradaImagen(qrContent);
        resultados.Add(entrada.idEntrada, qrBytes);
    }

    return Results.Ok(resultados); // Opcional: comprimir a ZIP para producción
});

// POST /qr/validar
app.MapPost("/qr/validar", (string qrContent, QrService qrService) =>
{
    var resultado = qrService.ValidarQr(qrContent);
    return Results.Ok(resultado);
});
#endregion

app.Run();
