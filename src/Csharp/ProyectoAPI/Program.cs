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
using Proyecto.Core.Servicios.Interfaces;
using Proyecto.Dapper;
using System.Data;
using MySqlConnector;

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
builder.Services.AddScoped<IQrService, QrService>();

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
app.MapPost("/api/clientes", (ClienteCreateDTO dto, IClienteRepository repo) =>
{
    if (dto.DNI <= 0) return Results.BadRequest("El DNI debe ser mayor a 0.");
    if (string.IsNullOrWhiteSpace(dto.Nombre)) return Results.BadRequest("El nombre es obligatorio.");
    if (string.IsNullOrWhiteSpace(dto.Apellido)) return Results.BadRequest("El apellido es obligatorio.");
    if (string.IsNullOrWhiteSpace(dto.Email)) return Results.BadRequest("El email es obligatorio.");

    var cliente = new Cliente
    {
        DNI = dto.DNI,
        Nombre = dto.Nombre,
        Apellido = dto.Apellido,
        Email = dto.Email,
        Telefono = dto.Telefono
    };
    repo.Add(cliente);

    var clienteDTO = new ClienteDTO
    {
        idCliente = cliente.idCliente,
        DNI = cliente.DNI,
        Nombre = cliente.Nombre,
        Apellido = cliente.Apellido,
        Email = cliente.Email,
        Telefono = cliente.Telefono
    };

    return Results.Created($"/api/clientes/{cliente.idCliente}", clienteDTO);
}).WithTags("Cliente");

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
}).WithTags("Cliente");

app.MapGet("/api/clientes/{idCliente}", (int idCliente, IClienteRepository repo) =>
{
    var c = repo.GetById(idCliente);
    if (c is null) return Results.NotFound("Cliente no encontrado.");
    return Results.Ok(new ClienteDTO
    {
        idCliente = c.idCliente,
        DNI = c.DNI,
        Nombre = c.Nombre,
        Apellido = c.Apellido,
        Email = c.Email,
        Telefono = c.Telefono
    });
}).WithTags("Cliente");

app.MapPut("/api/clientes/{idCliente}", (int idCliente, ClienteUpdateDTO dto, IClienteRepository repo) =>
{
    var cliente = repo.GetById(idCliente);
    if (cliente is null) return Results.NotFound("Cliente no encontrado.");

    if (dto.DNI <= 0) return Results.BadRequest("El DNI debe ser mayor a 0.");
    if (string.IsNullOrWhiteSpace(dto.Nombre)) return Results.BadRequest("El nombre es requerido.");
    if (string.IsNullOrWhiteSpace(dto.Apellido)) return Results.BadRequest("El apellido es requerido.");
    if (string.IsNullOrWhiteSpace(dto.Email)) return Results.BadRequest("El email es requerido.");

    cliente.DNI = dto.DNI;
    cliente.Nombre = dto.Nombre;
    cliente.Apellido = dto.Apellido;
    cliente.Email = dto.Email;
    cliente.Telefono = dto.Telefono;

    repo.Update(cliente);
    return Results.NoContent();
}).WithTags("Cliente");
#endregion

#region ORDENES
app.MapGet("/api/ordenes", (IOrdenRepository repo) =>
{
    var ordenes = repo.GetAll();
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
#endregion

#region ENTRADAS
app.MapGet("/api/entradas", (IEntradaRepository repo) =>
{
    var entradas = repo.GetAll();
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

app.MapGet("/api/entradas/{id}", (int id, IEntradaRepository repo) =>
{
    var entrada = repo.GetById(id);
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

app.MapPost("/api/entradas/{id}/anular", (int id, IEntradaRepository repo) =>
{
    try
    {
        repo.Anular(id);
        return Results.Ok($"Entrada {id} anulada correctamente.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
}).WithTags("Entradas");

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
}).WithTags("Funciones");

app.MapGet("/api/funciones/{id}", (int id, IFuncionRepository repo) =>
{
    var f = repo.GetById(id);
    if (f == null) return Results.NotFound("Función no encontrada");

    return Results.Ok(new FuncionDTO
    {
        idFuncion = f.IdFuncion,
        idEvento = f.IdEvento,
        Fecha = f.FechaHora,
        idLocal = f.IdLocal
    });
}).WithTags("Funciones");

app.MapPost("/api/funciones", (FuncionCreateDTO dto, IFuncionRepository repo) =>
{
    var funcion = new Funcion
    {
        IdEvento = dto.idEvento,
        FechaHora = dto.Fecha,
        IdLocal = dto.idLocal
    };

    repo.Add(funcion);
    return Results.Created($"/api/funciones/{funcion.IdFuncion}", funcion);
}).WithTags("Funciones");

app.MapPut("/api/funciones/{id}", (int id, FuncionUpdateDTO dto, IFuncionRepository repo) =>
{
    var f = repo.GetById(id);
    if (f == null) return Results.NotFound("Función no encontrada");

    f.FechaHora = dto.Fecha;
    f.IdLocal = dto.IdLocal;

    repo.Update(f);

    return Results.Ok(f);
}).WithTags("Funciones");

app.MapPost("/api/funciones/{id}/cancelar", (int id, IFuncionRepository repo) =>
{
    var f = repo.GetById(id);
    if (f == null) return Results.NotFound();

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
}).WithTags("Local");
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
}).WithTags("Local");
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
}).WithTags("Local");
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
}).WithTags("Local");
app.MapDelete("/api/locales/{localId}", (int localId, ILocalRepository repo) =>
{
    var l = repo.GetById(localId);
    if (l == null) return Results.NotFound();

    var eliminado = repo.Delete(localId);
    if (!eliminado) return Results.BadRequest("No se puede eliminar el local porque tiene funciones vigentes.");

    return Results.Ok("Local eliminado correctamente.");
}).WithTags("Local");

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
}).WithTags("Sector");
app.MapGet("/api/locales/{localId}/sectores", (int localId, ISectorRepository repo) =>
{
    var sectores = repo.GetByLocal(localId);
    return Results.Ok(sectores.Select(s => new SectorDTO
    {
        idSector = s.idSector,
        Nombre = s.Nombre,
        idLocal = s.idLocal
    }));
}).WithTags("Sector");
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
}).WithTags("Sector");
app.MapDelete("/api/sectores/{sectorId}", (int sectorId, ISectorRepository repo, ITarifaRepository tarifaRepo, IFuncionRepository funcionRepo) =>
{
    var sector = repo.GetById(sectorId);
    if (sector is null)
        return Results.NotFound("Sector no encontrado");

    if (tarifaRepo.TieneTarifasDeSector(sectorId) || funcionRepo.TieneFuncionesDeSector(sectorId))
        return Results.BadRequest("No se puede eliminar: el sector tiene tarifas o funciones asociadas.");

    repo.Delete(sectorId);
    return Results.Ok("Sector eliminado.");
}).WithTags("Sector");
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
}).WithTags("Tarifas");

app.MapPost("/api/tarifas", (TarifaCreateDTO dto, ITarifaRepository repo) =>
{
    var t = new Tarifa
    {
        Precio = dto.Precio,
        IdFuncion = dto.idFuncion
    };
    repo.Add(t);
    return Results.Created($"/api/tarifas/{t.idTarifa}", t);
}).WithTags("Tarifas");
#endregion

#region ROLES
app.MapGet("/api/roles", (IRolRepository repo) => Results.Ok(repo.GetAll()));
app.MapPost("/api/roles", (Rol r, IRolRepository repo) =>
{
    repo.Add(r);
    return Results.Created($"/api/roles/{r.IdRol}", r);
}).WithTags("Roles");
#endregion

#region USUARIOS
app.MapGet("/usuarios/{id}", (int idUsuario, IUsuarioRepository repo) =>
{
    var usuario = repo.GetById(idUsuario);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
}).WithTags("Usuario");

app.MapPost("/auth/login", (UsuarioLoginDTO login, IUsuarioRepository repo) =>
{
    var usuario = repo.Login(login.NombreUsuario, login.Contrasena);
    if (usuario is null) return Results.Unauthorized();

    return Results.Ok(new UsuarioDTO
    {
        idUsuario = usuario.IdUsuario,
        NombreUsuario = usuario.NombreUsuario,
    });
}).WithTags("Usuario");

app.MapPost("/usuarios", (Usuario nuevoUsuario, IUsuarioRepository repo) =>
{
    repo.Add(nuevoUsuario);
    return Results.Created($"/usuarios/{nuevoUsuario.IdUsuario}", nuevoUsuario);
}).WithTags("Usuario");

app.MapGet("/usuarios/{id}/roles", (int idUsuario, IUsuarioRepository repo) =>
{
    var roles = repo.GetRoles(idUsuario);
    return Results.Ok(roles);
}).WithTags("Usuario");

app.MapGet("/roles", (IUsuarioRepository repo) => Results.Ok(repo.GetAllRoles()));

app.MapPost("/usuarios/{id}/roles/{rolId}", (int idUsuario, int rolId, IUsuarioRepository repo) =>
{
    repo.AsignarRol(idUsuario, rolId);
    return Results.Ok(new { mensaje = "Rol asignado correctamente" });
}).WithTags("Rol");
#endregion

#region QR
app.MapGet("/entradas/{idEntrada}/qr", (int idEntrada, IQrService qrService, IEntradaRepository repo) =>
{
    var entrada = repo.GetById(idEntrada);
    if (entrada == null) return Results.NotFound("Entrada no existe");

    string qrContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{builder.Configuration["Qr:Key"]}";
    var qrBytes = qrService.GenerarQrImagen(qrContent);
    return Results.File(qrBytes, "image/png");
}).WithTags("QR");

app.MapPost("/qr/lote", (List<int> idEntradas, IQrService qrService, IEntradaRepository repo) =>
{
    var resultados = new Dictionary<int, byte[]>();
    foreach (var idEntrada in idEntradas)
    {
        var entrada = repo.GetById(idEntrada);
        if (entrada == null) continue;
        string qrContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{builder.Configuration["Qr:Key"]}";
        var qrBytes = qrService.GenerarQrImagen(qrContent);
        resultados.Add(entrada.IdEntrada, qrBytes);
    }
    return Results.Ok(resultados);
}).WithTags("QR");

app.MapPost("/qr/validar", (string qrContent, IQrService qrService) =>
{
    var resultado = qrService.ValidarQr(qrContent);
    return Results.Ok(resultado);
});

app.MapPost("/api/qr/{idEntrada}", (int idEntrada, IQrService qrService) =>
{
    var qr = qrService.GenerarQr(idEntrada);
    return Results.Ok(qr);
}).WithTags("QR");
#endregion

app.Run();