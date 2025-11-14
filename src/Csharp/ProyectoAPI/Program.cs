using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Core.Entidades;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Servicios;
using Proyecto.Core.Repositorios.ReposDapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Proyecto.Core.DTOs;
using src.Proyecto.Dappers;

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
builder.Services.AddScoped<IRolRepository, RolRepository>();

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

app.MapGet("/api/clientes/{idCliente}", (int idCliente, IClienteRepository repo) =>
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

app.MapPut("/api/clientes/{id}", (int idCliente, ClienteUpdateDTO dto, IClienteRepository repo) =>
{
    var c = repo.GetById(idCliente);
    if (c is null) return Results.NotFound();
    c.DNI = dto.DNI;
    c.Nombre = dto.Nombre;
    c.Apellido = dto.Apellido;
    c.Email = dto.Email;
    c.Telefono = dto.Telefono;
    repo.Update(c);
    return Results.NoContent();
});

app.MapDelete("/api/clientes/{id}", (int idCliente, IClienteRepository repo) =>
{
    repo.Delete(idCliente);
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
        Detalles = (o.Detalles ?? new List<DetalleOrden>()).Select(d => new DetalleOrdenDTO
        {
            IdDetalleOrden = d.IdDetalleOrden,
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
        Detalles = (o.Detalles ?? new List<DetalleOrden>()).Select(d => new DetalleOrdenDTO
        {
            IdDetalleOrden = d.IdDetalleOrden,
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
        Total = 0,
        Detalles = new List<DetalleOrden>()
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
        idEntrada = e.IdEntrada,
        Precio = (int)e.Precio,
        Numero = e.Numero,
        Usada = e.Usada,
        Anulada = e.Anulada,
        QR = e.QR
    }));
});

app.MapPost("/api/entradas", (Entrada e, IEntradaRepository repo) =>
{
    repo.Add(e);
    return Results.Created($"/api/entradas/{e.IdEntrada}", e);
});
#endregion

#region EVENTOS
app.MapGet("/api/eventos", (IEventoRepository repo) =>
{
    var eventos = repo.GetAll();
    return Results.Ok(eventos.Select(e => new EventoDTO
    {
        idEvento = e.idEvento,
        Nombre = e.Nombre,
        Fecha = e.Fecha,
        Activo = e.Activo,
        idLocal = e.Local?.idLocal ?? 0
    }));
});

app.MapPost("/api/eventos", (EventoCreateDTO dto, IEventoRepository repo) =>
{
    var ev = new Evento
    {
        Nombre = dto.Nombre,
        Fecha = dto.Fecha,
        Activo = true,
        Lugar = dto.Lugar,
        Tipo = dto.Tipo
    };
    repo.Add(ev);
    return Results.Created($"/api/eventos/{ev.idEvento}", ev);
});
#endregion

#region FUNCIONES
app.MapGet("/api/funciones", (IFuncionRepository repo) =>
{
    var funciones = repo.GetAll();
    return Results.Ok(funciones.Select(f => new FuncionDTO
    {
        idFuncion = f.IdFuncion,
        idEvento = f.IdEvento,
        Fecha = f.FechaHora,
        idLocal = f.IdLocal
    }));
});

app.MapPost("/api/funciones", (FuncionCreateDTO dto, IFuncionRepository repo) =>
{
    var f = new Funcion
    {
        IdEvento = dto.idEvento,
        FechaHora = dto.Fecha,
        IdLocal = dto.idLocal
    };
    repo.Add(f);
    return Results.Created($"/api/funciones/{f.IdFuncion}", f);
});
#endregion

#region LOCALES
app.MapGet("/api/locales", (ILocalRepository repo) =>
{
    var locales = repo.GetAll();
    return Results.Ok(locales.Select(l => new LocalDTO
    {
        idLocal = l.idLocal,
        Nombre = l.Nombre,
        Direccion = l.Direccion,
        Capacidad = l.Capacidad,
        Telefono = l.Telefono
    }));
});

app.MapPost("/api/locales", (LocalCreateDTO dto, ILocalRepository repo) =>
{
    var local = new Local
    {
        Nombre = dto.Nombre,
        Direccion = dto.Direccion,
        Capacidad = dto.Capacidad,
        Telefono = dto.Telefono
    };
    var id = repo.Add(local);
    return Results.Created($"/api/locales/{id}", local);
});
#endregion

#region SECTORES
app.MapGet("/api/sectores", (ISectorRepository repo) =>
{
    var sectores = repo.GetAll();
    return Results.Ok(sectores.Select(s => new SectorDTO
    {
        idSector = s.idSector,
        Nombre = s.Nombre,
        idLocal = s.idLocal
    }));
});
app.MapPost("/api/sectores", (SectorCreateDTO dto, ISectorRepository repo) =>
{
    var s = new Sector
    {
        Nombre = dto.Nombre,
        idLocal = dto.idLocal,
        Capacidad = dto.Capacidad, 
        Precio = dto.Precio
    };
    repo.Add(s);
    return Results.Created($"/api/sectores/{s.idSector}", s);
});
#endregion

#region TARIFAS
app.MapGet("/api/funciones/{funcionId}/tarifas", (int funcionId, ITarifaRepository repo) =>
{
    var tarifas = repo.GetByFuncionId(funcionId);
    return Results.Ok(tarifas.Select(t => new TarifaDTO
    {
        idTarifa = t.idTarifa,
        Precio = t.Precio,
        idFuncion = t.IdFuncion
    }));
});

app.MapPost("/api/tarifas", (TarifaCreateDTO dto, ITarifaRepository repo) =>
{
    var t = new Tarifa
    {
        Precio = dto.Precio,
        IdFuncion = dto.idFuncion
    };
    repo.Add(t);
    return Results.Created($"/api/tarifas/{t.idTarifa}", t);
});
#endregion

#region ROLES
app.MapGet("/api/roles", (IRolRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/roles", (Rol r, IRolRepository repo) =>
{
    repo.Add(r);
    return Results.Created($"/api/roles/{r.IdRol}", r);
}) .WithTags("Tarifa");
#endregion

#region USUARIOS
app.MapGet("/usuarios/{id}", (int idUsuario, IUsuarioRepository repo) =>
{
    var usuario = repo.GetById(idUsuario);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
}) .WithTags("Tarifa");

app.MapPost("/auth/login", (UsuarioLoginDTO login, IUsuarioRepository repo) =>
{
    var usuario = repo.Login(login.NombreUsuario, login.Contrasena);
    if (usuario is null) return Results.Unauthorized();

    return Results.Ok(new UsuarioDTO
    {
        idUsuario = usuario.IdUsuario,
        NombreUsuario = usuario.NombreUsuario,
    });
});

app.MapPost("/usuarios", (Usuario nuevoUsuario, IUsuarioRepository repo) =>
{
    repo.Add(nuevoUsuario);
    return Results.Created($"/usuarios/{nuevoUsuario.IdUsuario}", nuevoUsuario);
});

app.MapGet("/usuarios/{id}/roles", (int idUsuario, IUsuarioRepository repo) =>
{
    var roles = repo.GetRoles(idUsuario);
    return Results.Ok(roles);
});

app.MapGet("/roles", (IUsuarioRepository repo) => Results.Ok(repo.GetAllRoles()));

app.MapPost("/usuarios/{id}/roles/{rolId}", (int idUsuario, int rolId, IUsuarioRepository repo) =>
{
    repo.AsignarRol(idUsuario, rolId);
    return Results.Ok(new { mensaje = "Rol asignado correctamente" });
});
#endregion

#region QR
app.MapGet("/entradas/{idEntrada}/qr", (int idEntrada, QrService qrService, IEntradaRepository repo) =>
{
    var entrada = repo.GetById(idEntrada);
    if (entrada == null) return Results.NotFound("Entrada no existe");

    string qrContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{builder.Configuration["Qr:Key"]}";
    var qrBytes = qrService.GenerarQrEntradaImagen(qrContent);
    return Results.File(qrBytes, "image/png");
}) .WithTags("Tarifa");

app.MapPost("/qr/lote", (List<int> idEntradas, QrService qrService, IEntradaRepository repo) =>
{
    var resultados = new Dictionary<int, byte[]>();
    foreach (var idEntrada in idEntradas)
    {
        var entrada = repo.GetById(idEntrada);
        if (entrada == null) continue;
        string qrContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{builder.Configuration["Qr:Key"]}";
        var qrBytes = qrService.GenerarQrEntradaImagen(qrContent);
        resultados.Add(entrada.IdEntrada, qrBytes);
    }
    return Results.Ok(resultados);
}); 

app.MapPost("/qr/validar", (string qrContent, QrService qrService) =>
{
    var resultado = qrService.ValidarQr(qrContent);
    return Results.Ok(resultado);
});
#endregion

app.Run();
