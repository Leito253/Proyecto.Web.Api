using Proyecto.Modelos.Entidades;
using Proyecto.Web.Api.Repositorios;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("MySqlConnection")
                 ?? throw new InvalidOperationException("Cadena de conexión no encontrada");

var repo = new LocalRepository(connStr);

var app = builder.Build();

app.Urls.Clear();
app.Urls.Add("http://localhost:5001");

app.MapGet("/locales", () =>
{
    try
    {
        var locales = repo.GetAll();
        return Results.Ok(locales);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/locales/{id}", (int id) =>
{
    try
    {
        var local = repo.GetById(id);
        return local != null ? Results.Ok(local) : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/locales", (Local local) =>
{
    try
    {
        repo.Add(local);
        return Results.Created($"/locales/{local.idLocal}", local);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("/locales/{id}", (int id, Local local) =>
{
    try
    {
        var existente = repo.GetById(id);
        if (existente == null) return Results.NotFound();

        local.idLocal = id;
        repo.Update(local);
        return Results.Ok(local);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("/locales/{id}", (int id) =>
{
    try
    {
        var existente = repo.GetById(id);
        if (existente == null) return Results.NotFound();

        repo.Delete(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();