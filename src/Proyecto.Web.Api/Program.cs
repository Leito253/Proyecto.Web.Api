using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Modelos.Repositorios;
using Proyecto.Modelos.Repositorios.ReposDapper;
using Proyecto.Modelos.Servicios;
using src.Proyecto.Modelos.Repositorios;

var builder = WebApplication.CreateBuilder(args);

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

// MVC y Swagger
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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();


app.UseHttpsRedirection();

/* ENDPOINTS */ 

#region Clientes
app.MapGet("/api/clientes", (IClienteRepository repo) => Results.Ok(repo.GetAll()));
app.MapGet("/api/clientes/{id}", (int idCliente, IClienteRepository repo) =>
{
    var item = repo.GetById(idCliente);
    return item is null ? Results.NotFound() : Results.Ok(item);
});
app.MapPost("/api/clientes", (Cliente c, IClienteRepository repo) =>
{
    repo.Add(c);
    return Results.Created($"/api/clientes/{c.DNI}", c);
});
app.MapPut("/api/clientes/{id}", (int idCliente, Cliente c, IClienteRepository repo) =>
{
    if (idCliente != c.DNI) return Results.BadRequest();
    repo.Update(c);
    return Results.NoContent();
});
app.MapDelete("/api/clientes/{id}", (int idCliente, IClienteRepository repo) =>
{
    repo.Delete(idCliente);
    return Results.NoContent();
});

#endregion

#region Ordenes
app.MapGet("/api/ordenes", (IOrdenRepository repo) => Results.Ok(repo.GetAll()));
app.MapGet("/api/ordenes/{id}", (int idOrden, IOrdenRepository repo) =>
{
    var item = repo.GetById(idOrden);
    return item is null ? Results.NotFound() : Results.Ok(item);
});
app.MapPost("/api/ordenes", (Orden o, IOrdenRepository repo) =>
{
    repo.Add(o);
    return Results.Created($"/api/ordenes/{o.idOrden}", o);
});
app.MapPut("/api/ordenes/{id}", (int idOrden, Orden o, IOrdenRepository repo) =>
{
    if (idOrden != o.idOrden) return Results.BadRequest();
    repo.Update(o);
    return Results.NoContent();
});


#endregion

#region ENTRADA

app.MapGet("/api/entradas", (IEntradaRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/entradas", (Entrada e, IEntradaRepository repo) =>
{
    repo.Add(e);
    return Results.Created($"/api/entradas/{e.idEntrada}", e);
});

#endregion

#region EVENTO 
app.MapGet("/api/eventos", (IEventoRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/eventos", (Evento e, IEventoRepository repo) =>
{
    repo.Add(e);
    return Results.Created($"/api/eventos/{e.idEvento}", e);
});

#endregion

#region FUNCION 
app.MapGet("/api/funciones", (IFuncionRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/funciones", (Funcion f, IFuncionRepository repo) =>
{
    repo.Add(f);
    return Results.Created($"/api/funciones/{f.IdFuncion}", f);
});

#endregion

#region LOCAL 
app.MapGet("/api/locales", (ILocalRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/locales", (Local l, ILocalRepository repo) =>
{
    repo.Add(l);
    return Results.Created($"/api/locales/{l.idLocal}", l);
});

#endregion

#region ROL 
app.MapGet("/api/roles", (IRolRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/roles", (Rol r, IRolRepository repo) =>
{
    repo.Add(r);
    return Results.Created($"/api/roles/{r.IdRol}", r);
});

#endregion

#region SECTOR 
app.MapGet("/api/sectores", (ISectorRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/sectores", (Sector s, ISectorRepository repo) =>
{
    repo.Add(s);
    return Results.Created($"/api/sectores/{s.idSector}", s);
});

#endregion

#region TARIFA 
app.MapGet("/api/funciones/{funcionId}/tarifas", (int IdFuncion, ITarifaRepository repo) =>
{
    var tarifas = repo.GetByFuncionId(IdFuncion);
    return Results.Ok(tarifas);
});
app.MapPost("/api/tarifas", (Tarifa t, ITarifaRepository repo) =>
{
    repo.Add(t);
    return Results.Created($"/api/tarifas/{t.idTarifa}", t);
});

#endregion

#region USUARIO
app.MapGet("/api/usuarios", (IUsuarioRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/usuarios", (Usuario u, IUsuarioRepository repo) =>
{
    repo.Add(u);
    return Results.Created($"/api/usuarios/{u.IdUsuario}", u);
});


#endregion
/*
#region USUARIO ROL 
app.MapGet("/api/usuariorol", (IUsuarioRolRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/usuariorol", (UsuarioRol ur, IUsuarioRolRepository repo) =>
{
    repo.Add(ur);
    return Results.Created($"/api/usuariorol/{ur.IdUsuario}", ur);
});

#endregion
*/

app.Run();
