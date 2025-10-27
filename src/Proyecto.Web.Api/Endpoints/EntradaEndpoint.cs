/*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class EntradaEndpoints
    {
        public static void MapEntradaEndpoints(this WebApplication app, EntradaRepository repo)
        {
            app.MapGet("/entradas", () => repo.GetAll());

            app.MapGet("/entradas/{id}", (int id) =>
            {
                var entrada = repo.GetById(id);
                return entrada is not null ? Results.Ok(entrada) : Results.NotFound();
            });

            app.MapPost("/entradas", (Entrada entrada) =>
            {
                repo.Add(entrada);
                return Results.Created($"/entradas/{entrada.idEntrada}", entrada);
            });

            app.MapPut("/entradas/{id}", (int id, Entrada entrada) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                entrada.idEntrada = id;
                repo.Update(entrada);
                return Results.Ok(entrada);
            });

            app.MapDelete("/entradas/{id}", (int id) =>
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