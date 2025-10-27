/*
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;

namespace Proyecto.Web.Api.Endpoints;

public static class LocalesEndpoints
{
    public static void MapLocalesEndpoints(this WebApplication app, ILocalRepository repo)
    {
        app.MapGet("/locales", () => repo.GetAll());

        app.MapGet("/locales/{id}", (int id) =>
        {
            var local = repo.GetById(id);
            return local is not null ? Results.Ok(local) : Results.NotFound();
        });

        app.MapPost("/locales", (Local local) =>
        {
            repo.Add(local);
            return Results.Created($"/locales/{local.idLocal}", local);
        });

        app.MapPut("/locales/{id}", (int id, Local local) =>
        {
            var existente = repo.GetById(id);
            if (existente == null) return Results.NotFound();

            local.idLocal = id;
            repo.Update(local);
            return Results.Ok(local);
        });

        app.MapDelete("/locales/{id}", (int id) =>
        {
            var existente = repo.GetById(id);
            if (existente == null) return Results.NotFound();

            repo.Delete(id);
            return Results.NoContent();
        });
    }
}
*/