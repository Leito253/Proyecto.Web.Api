using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Web.Api.Repositorios;
using Proyecto.Web.Api.Servicios;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

app.Run();