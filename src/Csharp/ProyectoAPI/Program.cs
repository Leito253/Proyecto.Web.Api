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
using Proyecto.Core.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

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
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: 'Bearer {token}'",
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

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<IFuncionRepository, FuncionRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITarifaRepository, TarifaRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<QrService>();

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

#region CLIENTES
app.MapGet("/api/clientes", (IClienteRepository repo) =>
{
    var clientes = repo.GetAll();
    return Results.Ok(clientes.Select(c => new ClienteDTO
    {
        idCliente = c.idCliente,
        DNI = c.DNI,
        Nombre = c.Nombre,
        Apellido = c.Apellido,
        Email = c.Email,
        Telefono = c.Telefono
    }));
});

app.MapGet("/api/clientes/{id}", (int idCliente, IClienteRepository repo) =>
{
    var c = repo.GetById(idCliente);
    if (c is null) return Results.NotFound();
    return Results.Ok(new ClienteDTO
    {
        idCliente = c.idCliente,
        DNI = c.DNI,
        Nombre = c.Nombre,
        Apellido = c.Apellido,
        Email = c.Email,
        Telefono = c.Telefono
    });
});

app.MapPost("/api/clientes", (ClienteCreateDTO dto, IClienteRepository repo) =>
{
    var cliente = new Cliente
    {
        DNI = dto.DNI,
        Nombre = dto.Nombre,
        Apellido = dto.Apellido,
        Email = dto.Email,
        Telefono = dto.Telefono
    };
    repo.Add(cliente);
    return Results.Created($"/api/clientes/{cliente.idCliente}", cliente);
});

app.MapPut("/api/clientes/{id}", (int id, ClienteUpdateDTO dto, IClienteRepository repo) =>
{
    var c = repo.GetById(id);
    if (c is null) return Results.NotFound();
    c.DNI = dto.DNI;
    c.Nombre = dto.Nombre;
    c.Apellido = dto.Apellido;
    c.Email = dto.Email;
    c.Telefono = dto.Telefono;
    repo.Update(c);
    return Results.NoContent();
});

app.MapDelete("/api/clientes/{id}", (int id, IClienteRepository repo) =>
{
    repo.Delete(id);
    return Results.NoContent();
});
#endregion

#region ORDENES
app.MapGet("/api/ordenes", (IOrdenRepository repo) =>
{
    var ordenes = repo.GetAll();
    return Results.Ok(ordenes.Select(o => new OrdenDTO
    {
        idOrden = o.idOrden,
        idCliente = o.idCliente,
        Fecha = o.Fecha,
        Total = o.Total,
        Detalles = o.Detalles.Select(d => new DetalleOrdenDTO
        {
            idDetalleOrden = d.IdDetalleOrden,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario
        }).ToList()
    }));
});

app.MapGet("/api/ordenes/{id}", (int idOrden, IOrdenRepository repo) =>
{
    var o = repo.GetById(idOrden);
    if (o is null) return Results.NotFound();
    return Results.Ok(new OrdenDTO
    {
        idOrden = o.idOrden,
        idCliente = o.idCliente,
        Fecha = o.Fecha,
        Total = o.Total,
        Detalles = o.Detalles.Select(d => new DetalleOrdenDTO
        {
            idDetalleOrden = d.IdDetalleOrden,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario
        }).ToList()
    });
});

app.MapPost("/api/ordenes", (OrdenCreateDTO dto, IOrdenRepository repo) =>
{
    var nueva = new Orden
    {
        idCliente = dto.idCliente,
        Fecha = DateTime.Now,
    };
    repo.Add(nueva);
    return Results.Created($"/api/ordenes/{nueva.idOrden}", nueva);
});
#endregion

#region ENTRADAS
app.MapGet("/api/entradas", (IEntradaRepository repo) =>
{
    var entradas = repo.GetAll();
    return Results.Ok(entradas.Select(e => new EntradaDTO
    {
        idEntrada = e.idEntrada,
        Precio = e.Precio,
        Numero = e.Numero,
        Usada = e.Usada,
        Anulada = e.Anulada,
        QR = e.QR
    }));
});
#endregion

#region USUARIOS
app.MapPost("/auth/login", (UsuarioLoginDTO login, IUsuarioRepository repo) =>
{
    var usuario = repo.Login(login.usuario, login.Contrasena);
    if (usuario is null) return Results.Unauthorized();

    return Results.Ok(new UsuarioDTO
    {
        idUsuario = usuario.IdUsuario,
        usuario = usuario.usuario,
        Rol = usuario.Rol
    });
});
#endregion

app.Run();