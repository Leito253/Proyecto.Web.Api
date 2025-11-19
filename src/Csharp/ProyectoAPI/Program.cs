using System.Text;
using Microsoft.OpenApi.Models;
using Proyecto.Core.Entidades;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Servicios;
using Proyecto.Core.Repositorios.ReposDapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Proyecto.Core.DTOs;
using src.Proyecto.Dappers;
using Proyecto.Dapper;
using System.Data;
using MySqlConnector;
using System.Security.Claims;

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
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IQRRepository, QRRepository>();

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MySqlConnection(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<QrService>();
builder.Services.AddScoped<Proyecto.Core.Servicios.IQrService, QrService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<Servicios.Interfaces.IClienteService, Proyecto.Core.Servicios.ClienteService>();

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

#region CLIENTES
app.MapPost("/api/clientes", async (ClienteCreateDTO dto, Servicios.Interfaces.IClienteService service) =>
{
    var cliente = new Cliente
    {
        DNI = dto.DNI,
        Nombre = dto.Nombre,
        Apellido = dto.Apellido,
        Email = dto.Email,
        Telefono = dto.Telefono
    };
    var ok = await service.Crear(cliente);
    if (!ok) return Results.BadRequest("Datos inválidos");
    return Results.Created($"/api/clientes/{cliente.idCliente}", cliente);
}).WithTags("Clientes");

app.MapGet("/api/clientes", async (Servicios.Interfaces.IClienteService service) =>
{
    var clientes = await service.ObtenerTodos();
    return Results.Ok(clientes);
}).WithTags("Clientes");

app.MapGet("/api/clientes/{idCliente}", async (int idCliente, Servicios.Interfaces.IClienteService service) =>
{
    var cliente = await service.ObtenerPorId(idCliente);
    if (cliente is null) return Results.NotFound("Cliente no encontrado.");
    return Results.Ok(cliente);
}).WithTags("Clientes");

app.MapPut("/api/clientes/{idCliente}", async (int idCliente, ClienteUpdateDTO dto, Servicios.Interfaces.IClienteService service) =>
{
    var cliente = await service.ObtenerPorId(idCliente);
    if (cliente is null) return Results.NotFound("Cliente no encontrado.");
    cliente.DNI = dto.DNI;
    cliente.Nombre = dto.Nombre;
    cliente.Apellido = dto.Apellido;
    cliente.Email = dto.Email;
    cliente.Telefono = dto.Telefono;
    var ok = await service.Actualizar(cliente);
    if (!ok) return Results.BadRequest("Datos inválidos");
    return Results.NoContent();
}).WithTags("Clientes");
#endregion

#region ORDENES
app.MapGet("/api/ordenes", (IOrdenRepository repo, int? clienteId, string? estado) =>
{
    var ordenes = repo.GetAll();
    if (clienteId.HasValue)
        ordenes = ordenes.Where(o => o.idCliente == clienteId);
    if (!string.IsNullOrWhiteSpace(estado))
        ordenes = ordenes.Where(o => o.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
    var dto = ordenes.Select(o => new OrdenDTO
    {
        idOrden = o.idOrden,
        idCliente = o.idCliente,
        Fecha = o.Fecha,
        Total = o.Total,
        Estado = o.Estado,
        Detalles = (o.Detalles ?? new List<DetalleOrden>())
            .Select(d => new DetalleOrdenDTO
            {
                IdDetalleOrden = d.IdDetalleOrden,
                IdEvento = d.IdEvento,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
    });

    return Results.Ok(dto);
}).WithTags("Orden");

app.MapGet("/api/ordenes/{id}", (int id, IOrdenRepository repo) =>
{
    var o = repo.GetByIdWithDetalles(id);
    if (o is null) return Results.NotFound();

    var dto = new OrdenDTO
    {
        idOrden = o.idOrden,
        idCliente = o.idCliente,
        Fecha = o.Fecha,
        Total = o.Total,
        Estado = o.Estado,
        Detalles = (o.Detalles ?? new List<DetalleOrden>())
            .Select(d => new DetalleOrdenDTO
            {
                IdDetalleOrden = d.IdDetalleOrden,
                IdEvento = d.IdEvento,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).ToList()
    };

    return Results.Ok(dto);
}).WithTags("Orden");

app.MapPost("/api/ordenes", (OrdenCreateDTO dto, IOrdenRepository repo) =>
{
    if (dto.idFunciones.Count != dto.idTarifas.Count || dto.idTarifas.Count != dto.Cantidades.Count)
        return Results.BadRequest("Las listas idFunciones, idTarifas y Cantidades deben tener la misma longitud.");

    var orden = new Orden
    {
        idCliente = dto.idCliente,
        Fecha = DateTime.Now,
        Estado = "Creada",
        Total = 0,
        Detalles = new List<DetalleOrden>()
    };

    for (int i = 0; i < dto.idFunciones.Count; i++)
    {
        orden.Detalles.Add(new DetalleOrden
        {
            IdEvento = dto.idFunciones[i],
            IdTarifa = dto.idTarifas[i],
            Cantidad = dto.Cantidades[i],
            PrecioUnitario = 0
        });
    }

    orden.Total = orden.Detalles.Sum(d => d.PrecioUnitario * d.Cantidad);

    repo.Add(orden);

    return Results.Created($"/api/ordenes/{orden.idOrden}", orden);
}).WithTags("Orden");

app.MapPost("/api/ordenes/{id}/generar-qr", (int id, IOrdenRepository ordenRepo, IEntradaRepository entradaRepo, IQrService qrService) =>
{
    var orden = ordenRepo.GetById(id);
    if (orden == null) return Results.NotFound();
    if (!orden.Estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
        return Results.BadRequest("La orden no está pagada.");

    foreach (var detalle in orden.Detalles ?? new List<DetalleOrden>())
    {
        var entrada = entradaRepo.GetByDetalleOrdenId(detalle.IdDetalleOrden);
        if (entrada == null) continue;

        string qrContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{builder.Configuration["Qr:Key"]}";
        qrService.GenerarQrImagen(qrContent);
    }

    return Results.Ok("QRs generados correctamente.");
}).WithTags("Orden");

app.MapPost("/api/ordenes/{ordenId}/pagar", (int ordenId, IOrdenRepository repo) =>
{
    var result = repo.PagarOrden(ordenId);
    if (result == "NotFound") return Results.NotFound();
    if (result == "BadRequest") return Results.BadRequest("Solo se pueden pagar órdenes en estado 'Creada'.");
    return Results.Ok("Orden pagada y entradas emitidas.");
}).WithTags("Orden");

app.MapPost("/api/ordenes/{ordenId}/cancelar", (int ordenId, IOrdenRepository repo) =>
{
    var result = repo.CancelarOrden(ordenId);
    if (result == "NotFound") return Results.NotFound();
    if (result == "BadRequest") return Results.BadRequest("Solo se pueden cancelar órdenes en estado 'Creada'.");
    return Results.Ok("Orden cancelada.");
}).WithTags("Orden");

#endregion

#region ENTRADAS
app.MapGet("/api/entradas", (IEntradaRepository repo, int? funcionId, int? ordenId, int? clienteId, string? estado) =>
{
    var entradas = repo.GetAll();
    if (funcionId.HasValue)
        entradas = entradas.Where(e => e.IdFuncion == funcionId);
    if (ordenId.HasValue)
        entradas = entradas.Where(e => e.IdDetalleOrden == ordenId);
    if (clienteId.HasValue)
        entradas = entradas.Where(e => e.IdCliente == clienteId);
    if (!string.IsNullOrWhiteSpace(estado))
    {
        if (estado.ToLower() == "usada") entradas = entradas.Where(e => e.Usada);
        else if (estado.ToLower() == "anulada") entradas = entradas.Where(e => e.Anulada);
        else if (estado.ToLower() == "valida") entradas = entradas.Where(e => !e.Usada && !e.Anulada);
    }
    var dto = entradas.Select(e => new EntradaDTO
    {
        idEntrada = e.IdEntrada,
        Precio = (int)e.Precio,
        Numero = e.Numero,
        Usada = e.Usada,
        Anulada = e.Anulada,
        QR = e.QR,
        IdDetalleOrden = e.IdDetalleOrden,
        IdSector = e.IdSector,
        IdFuncion = e.IdFuncion
    }).ToList();
    return Results.Ok(dto);
}).WithTags("Entradas");

app.MapGet("/api/entradas/{IdEntrada}", (int IdEntrada, IEntradaRepository repo) =>
{
    var entrada = repo.GetById(IdEntrada);
    if (entrada == null) return Results.NotFound();
    var dto = new EntradaDTO
    {
        idEntrada = entrada.IdEntrada,
        Precio = (int)entrada.Precio,
        Numero = entrada.Numero,
        Usada = entrada.Usada,
        Anulada = entrada.Anulada,
        QR = entrada.QR,
        IdDetalleOrden = entrada.IdDetalleOrden,
        IdSector = entrada.IdSector,
        IdFuncion = entrada.IdFuncion
    };
    return Results.Ok(dto);
}).WithTags("Entradas");

app.MapPost("/api/entradas", (EntradaCreateDTO dto, IEntradaRepository repo) =>
{
    var entrada = new Entrada
    {
        Precio = dto.Precio,
        Numero = dto.Numero,
        Usada = false,
        Anulada = false,
        QR = string.Empty,
        IdDetalleOrden = dto.IdDetalleOrden,
        IdSector = dto.IdSector,
        IdFuncion = dto.IdFuncion
    };
    repo.Add(entrada);
    return Results.Created($"/api/entradas/{entrada.IdEntrada}", entrada);
}).WithTags("Entradas");

app.MapPost("/api/entradas/{entradaId}/anular", (int IdEntrada, IEntradaRepository repo) =>
{
    try
    {
        repo.Anular(IdEntrada);
        return Results.Ok($"Entrada {IdEntrada} anulada correctamente.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithTags("Entradas");

#endregion

#region EVENTOS
app.MapGet("/api/eventos", (IEventoRepository repo, string? q, string? estado, int? localId, DateTime? desde, DateTime? hasta) =>
{
    var eventos = repo.GetAll();
    if (!string.IsNullOrWhiteSpace(q))
        eventos = eventos.Where(e => e.Nombre.Contains(q, StringComparison.OrdinalIgnoreCase));
    if (!string.IsNullOrWhiteSpace(estado))
        eventos = eventos.Where(e => e.Activo == (estado.ToLower() == "activo"));
    if (localId.HasValue)
        eventos = eventos.Where(e => e.idLocal == localId);
    if (desde.HasValue)
        eventos = eventos.Where(e => e.Fecha >= desde);
    if (hasta.HasValue)
        eventos = eventos.Where(e => e.Fecha <= hasta);
    return Results.Ok(eventos.Select(e => new EventoDTO
    {
        idEvento = e.idEvento,
        Nombre = e.Nombre,
        Fecha = e.Fecha,
        Activo = e.Activo,
        idLocal = e.idLocal
    }));
}).WithTags("Eventos");

app.MapGet("/api/eventos/{id}", (int id, IEventoRepository repo) =>
{
    var e = repo.GetById(id);
    if (e == null) return Results.NotFound("Evento no encontrado");

    return Results.Ok(new EventoDTO
    {
        idEvento = e.idEvento,
        Nombre = e.Nombre,
        Fecha = e.Fecha,
        Activo = e.Activo,
        idLocal = e.idLocal
    });
}).WithTags("Eventos");

app.MapPost("/api/eventos", (EventoCreateDTO dto, IEventoRepository repo) =>
{
    var ev = new Evento
    {
        Nombre = dto.Nombre,
        Fecha = dto.Fecha,
        Activo = false,
        idLocal = dto.idLocal,
        Lugar = dto.Lugar,
        Tipo = dto.Tipo
    };

    repo.Add(ev);
    return Results.Created($"/api/eventos/{ev.idEvento}", ev);
}).WithTags("Eventos");

app.MapPut("/api/eventos/{id}", (int id, EventoUpdateDTO dto, IEventoRepository repo) =>
{
    var evento = repo.GetById(id);
    if (evento == null) return Results.NotFound("Evento no encontrado");

    evento.Nombre = dto.Nombre;
    evento.Fecha = dto.Fecha;
    evento.idLocal = dto.IdLocal;
    evento.Lugar = dto.Lugar;
    evento.Tipo = dto.Tipo;

    repo.Update(evento);

    return Results.Ok(evento);
}).WithTags("Eventos");

app.MapPost("/api/eventos/{id}/publicar", (int id, IEventoRepository repo) =>
{
    var evento = repo.GetById(id);
    if (evento == null) return Results.NotFound();

    repo.Publicar(id);

    return Results.Ok("Evento publicado correctamente");
}).WithTags("Eventos");

app.MapPost("/api/eventos/{id}/cancelar", (int id, IEventoRepository repo) =>
{
    var evento = repo.GetById(id);
    if (evento == null) return Results.NotFound();

    repo.Cancelar(id);

    return Results.Ok("Evento cancelado correctamente");
}).WithTags("Eventos");

#endregion

#region FUNCIONES
app.MapGet("/api/funciones", (IFuncionRepository repo, int? eventoId, DateTime? desde, DateTime? hasta) =>
{
    var funciones = repo.GetAll();
    if (eventoId.HasValue)
        funciones = funciones.Where(f => f.IdEvento == eventoId);
    if (desde.HasValue)
        funciones = funciones.Where(f => f.FechaHora >= desde);
    if (hasta.HasValue)
        funciones = funciones.Where(f => f.FechaHora <= hasta);
    var dto = funciones.Select(f => new FuncionDTO
    {
        idFuncion = f.IdFuncion,
        idEvento = f.IdEvento,
        Fecha = f.FechaHora,
        idLocal = f.IdLocal
    }).ToList();
    return Results.Ok(dto);
}).WithTags("Funciones");

app.MapGet("/api/funciones/{id}", (int id, IFuncionRepository repo) =>
{
    var f = repo.GetById(id);
    if (f == null) return Results.NotFound("Función no encontrada");

    var dto = new FuncionDTO
    {
        idFuncion = f.IdFuncion,
        idEvento = f.IdEvento,
        Fecha = f.FechaHora,
        idLocal = f.IdLocal
    };

    return Results.Ok(dto);
}).WithTags("Funciones");

app.MapPost("/api/funciones", (FuncionCreateDTO dto, IFuncionRepository repo) =>
{
    var funcion = new Funcion
    {
        IdEvento = dto.idEvento,
        FechaHora = dto.Fecha,
        IdLocal = dto.idLocal,
        Estado = "Activa"
    };

    repo.Add(funcion);

    var dtoResult = new FuncionDTO
    {
        idFuncion = funcion.IdFuncion,
        idEvento = funcion.IdEvento,
        Fecha = funcion.FechaHora,
        idLocal = funcion.IdLocal
    };

    return Results.Created($"/api/funciones/{funcion.IdFuncion}", dtoResult);
}).WithTags("Funciones");

app.MapPut("/api/funciones/{id}", (int id, FuncionUpdateDTO dto, IFuncionRepository repo) =>
{
    var f = repo.GetById(id);
    if (f == null) return Results.NotFound("Función no encontrada");

    f.FechaHora = dto.Fecha;
    f.IdLocal = dto.IdLocal;
    f.Estado = dto.Estado;
    f.IdEvento = dto.IdEvento;

    repo.Update(f);

    var dtoResult = new FuncionDTO
    {
        idFuncion = f.IdFuncion,
        idEvento = f.IdEvento,
        Fecha = f.FechaHora,
        idLocal = f.IdLocal
    };

    return Results.Ok(dtoResult);
}).WithTags("Funciones");

app.MapPost("/api/funciones/{id}/cancelar", (int id, IFuncionRepository repo) =>
{
    var f = repo.GetById(id);
    if (f == null) return Results.NotFound("Función no encontrada");

    repo.Cancelar(id);

    return Results.Ok("Función cancelada correctamente");
}).WithTags("Funciones");
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
}).WithTags("Locales");
app.MapGet("/api/locales/{localId}", (int localId, ILocalRepository repo) =>
{
    var l = repo.GetById(localId);
    if (l == null) return Results.NotFound();

    return Results.Ok(new LocalDTO
    {
        idLocal = l.idLocal,
        Nombre = l.Nombre,
        Direccion = l.Direccion,
        Capacidad = l.Capacidad,
        Telefono = l.Telefono
    });
}).WithTags("Locales");
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
}).WithTags("Locales");
app.MapPut("/api/locales/{localId}", (int localId, LocalCreateDTO dto, ILocalRepository repo) =>
{
    var l = repo.GetById(localId);
    if (l == null) return Results.NotFound();

    l.Nombre = dto.Nombre;
    l.Direccion = dto.Direccion;
    l.Capacidad = dto.Capacidad;
    l.Telefono = dto.Telefono;

    repo.Update(l);

    return Results.Ok(l);
}).WithTags("Locales");
app.MapDelete("/api/locales/{localId}", (int localId, ILocalRepository repo) =>
{
    var l = repo.GetById(localId);
    if (l == null) return Results.NotFound();

    var eliminado = repo.Delete(localId);
    if (!eliminado) return Results.BadRequest("No se puede eliminar el local porque tiene funciones vigentes.");

    return Results.Ok("Local eliminado correctamente.");
}).WithTags("Locales");

#endregion

#region SECTORES
app.MapPost("/api/locales/{localId}/sectores", (int localId, SectorCreateDTO dto, ISectorRepository repo) =>
{
    var sector = new Sector
    {
        Nombre = dto.Nombre,
        Capacidad = dto.Capacidad,
        Precio = dto.Precio,
        idLocal = localId
    };

    repo.Add(sector);

    return Results.Created($"/api/sectores/{sector.idSector}", sector);
}).WithTags("Sectores");
app.MapGet("/api/locales/{localId}/sectores", (int localId, ISectorRepository repo) =>
{
    var sectores = repo.GetByLocal(localId);
    return Results.Ok(sectores.Select(s => new SectorDTO
    {
        idSector = s.idSector,
        Nombre = s.Nombre,
        idLocal = s.idLocal
    }));
}).WithTags("Sectores");
app.MapPut("/api/sectores/{sectorId}", (int sectorId, SectorUpdateDTO dto, ISectorRepository repo) =>
{
    var sector = repo.GetById(sectorId);
    if (sector is null)
        return Results.NotFound("Sector no encontrado");

    sector.Nombre = dto.Nombre;
    sector.Capacidad = dto.Capacidad;
    sector.Precio = dto.Precio;

    repo.Update(sector);

    return Results.Ok(sector);
}).WithTags("Sectores");
app.MapDelete("/api/sectores/{sectorId}", (int sectorId, ISectorRepository repo, ITarifaRepository tarifaRepo, IFuncionRepository funcionRepo) =>
{
    var sector = repo.GetById(sectorId);
    if (sector is null)
        return Results.NotFound("Sector no encontrado");

    if (tarifaRepo.TieneTarifasDeSector(sectorId) || funcionRepo.TieneFuncionesDeSector(sectorId))
        return Results.BadRequest("No se puede eliminar: el sector tiene tarifas o funciones asociadas.");

    repo.Delete(sectorId);
    return Results.Ok("Sector eliminado.");
}).WithTags("Sectores");
#endregion

#region TARIFAS
app.MapGet("/api/funciones/{funcionId}/tarifas", (int funcionId, ITarifaRepository repo) =>
{
    var tarifas = repo.GetByFuncionId(funcionId);

    return Results.Ok(tarifas.Select(t => new TarifaDTO
    {
        idTarifa = t.idTarifa,
        Precio = t.Precio,
        idFuncion = t.IdFuncion,
        Stock = t.Stock,
        Activa = t.Activa
    }));
}).WithTags("Tarifas");

app.MapGet("/api/tarifas/{tarifaId}", (int tarifaId, ITarifaRepository repo) =>
{
    var t = repo.GetById(tarifaId);
    if (t is null) return Results.NotFound("Tarifa no encontrada.");

    return Results.Ok(new TarifaDTO
    {
        idTarifa = t.idTarifa,
        Precio = t.Precio,
        idFuncion = t.IdFuncion,
        Stock = t.Stock,
        Activa = t.Activa
    });
}).WithTags("Tarifas");

app.MapPost("/api/tarifas", (TarifaCreateDTO dto, ITarifaRepository repo) =>
{
    var tarifa = new Tarifa
    {
        Precio = dto.Precio,
        IdFuncion = dto.idFuncion,
        Stock = dto.Stock,
        Activa = true
    };

    repo.Add(tarifa);

    return Results.Created($"/api/tarifas/{tarifa.idTarifa}", tarifa);
}).WithTags("Tarifas");

app.MapPut("/api/tarifas/{tarifaId}", (int tarifaId, TarifaUpdateDTO dto, ITarifaRepository repo) =>
{
    var t = repo.GetById(tarifaId);
    if (t is null) return Results.NotFound("Tarifa no encontrada.");

    t.Precio = dto.Precio;
    t.Stock = dto.Stock;
    t.Activa = dto.Activa;

    repo.Update(t);

    return Results.Ok(t);
}).WithTags("Tarifas");

#endregion

#region ROLES
app.MapGet("/api/roles", (IRolRepository repo) => Results.Ok(repo.GetAll())).WithTags("Roles");
app.MapPost("/api/roles", (Rol r, IRolRepository repo) =>
{
    repo.Add(r);
    return Results.Created($"/api/roles/{r.IdRol}", r);
}).WithTags("Roles");
#endregion

#region USUARIOS
app.MapPost("/auth/register", (UsuarioRegisterDTO dto, IUsuarioRepository repo, JwtService jwt) =>
{
    var u = new Usuario
    {
        NombreUsuario = dto.NombreUsuario,
        Email = dto.Email,
        Contrasena = PasswordHasher.Hash(dto.Contrasena)
    };

    repo.Add(u);
    var roles = repo.GetRoles(u.IdUsuario);
    var token = jwt.GenerarTokenAcceso(u, roles);

    return Results.Ok(new { token });
}).WithTags("Usuarios");
app.MapPost("/auth/login", (UsuarioLoginDTO dto, IUsuarioRepository repo, JwtService jwt) =>
{
    var u = repo.Login(dto.NombreUsuario, dto.Contrasena);
    if (u is null) return Results.Unauthorized();

    if (!PasswordHasher.Verify(dto.Contrasena, u.Contrasena))
        return Results.Unauthorized();

    var roles = repo.GetRoles(u.IdUsuario);
    var token = jwt.GenerarTokenAcceso(u, roles);

    return Results.Ok(new { token });
}).WithTags("Usuarios");
app.MapPost("/auth/refresh", (RefreshTokenDTO dto) =>
{
    return Results.Ok(new { token = "nuevo_token" });
}).WithTags("Usuarios");
app.MapPost("/auth/logout", () =>
{
    return Results.Ok("Logout exitoso.");
}).WithTags("Usuarios");
app.MapGet("/auth/me", (ClaimsPrincipal user, IUsuarioRepository repo) =>
{
    var id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var u = repo.GetById(id);
    if (u is null) return Results.NotFound();

    return Results.Ok(new UsuarioDTO
    {
        idUsuario = u.IdUsuario,
        NombreUsuario = u.NombreUsuario
    });
}).RequireAuthorization().WithTags("Usuarios");
app.MapGet("/auth/roles", (IUsuarioRepository repo) =>
{
    return Results.Ok(repo.GetAllRoles());
}).WithTags("Usuarios");

app.MapPost("/usuarios/{idUsuario}/roles", (int idUsuario, int idRol, IUsuarioRepository repo) =>
{
    repo.AsignarRol(idUsuario, idRol);
    return Results.Ok("Rol asignado.");
}).WithTags("Usuarios");
#endregion

#region QR
app.MapGet("/entradas/{entradaId}/qr", (int entradaId, IQrService qrService, IEntradaRepository repo) =>
{
    var entrada = repo.GetById(entradaId);
    if (entrada == null) return Results.NotFound("Entrada no existe");
    var qrBytes = qrService.GenerarQrImagen($"{entrada.IdEntrada}|{entrada.IdFuncion}");
    return Results.File(qrBytes, "image/png");
}).WithTags("QR");

app.MapPost("/qr/lote", (List<int> ids, IQrService qrService, IEntradaRepository repo) =>
{
    var resultado = new Dictionary<int, byte[]>();
    foreach (var id in ids)
    {
        var entrada = repo.GetById(id);
        if (entrada == null) continue;
        var qrBytes = qrService.GenerarQrImagen($"{entrada.IdEntrada}|{entrada.IdFuncion}");
        resultado.Add(id, qrBytes);
    }
    return Results.Ok(resultado);
}).WithTags("QR");

app.MapPost("/qr/validar", (string codigo, IQrService qrService) =>
{
    var resultado = qrService.ValidarQr(codigo);
    return Results.Ok(resultado);
}).WithTags("QR");
#endregion

app.Run();