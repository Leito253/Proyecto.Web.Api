using Proyecto.Modelos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var Local = new List<Local>
{
    new Local {Nombre = "Cancha Monumental",Direccion = "Av. Figueroa Alcorta 7597, CABA",CapacidadMax = 83000},
    new Local {Nombre = "Estadio Libertadores de América",Direccion = "Av. Pres. Figueroa Alcorta 7597, CABA",CapacidadMax = 66000},
    new Local {Nombre = "Estadio Ciudad de La Plata",Direccion = "Av. 25 y 32, La Plata, Buenos Aires",CapacidadMax = 53000},
    new Local {Nombre = "Luna Park", Direccion = "Av. Eduardo Madero 470, C1106 Cdad. Autónoma de Buenos Aires", CapacidadMax = 8400}




};

app.MapGet("/locales", () => Local);

app.MapPost("/locales", (Local local) =>
{
    Local.Add(local);
    return Results.Created($"/locales/{local.Nombre}", local);
});


app.Run();

