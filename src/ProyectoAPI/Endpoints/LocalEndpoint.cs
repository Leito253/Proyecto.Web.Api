/*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class LocalEndpoints
    {
        public static void MapLocalEndpoints(this WebApplication app, LocalRepository repo)
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
                if (existente is null) return Results.NotFound();

                local.idLocal = id;
                repo.Update(local);
                return Results.Ok(local);
            });

            app.MapDelete("/locales/{id}", (int id) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                repo.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
*/