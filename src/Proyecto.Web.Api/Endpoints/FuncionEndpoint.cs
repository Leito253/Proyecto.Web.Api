using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class FuncionEndpoints
    {
        public static void MapFuncionEndpoints(this WebApplication app, FuncionRepository repo)
        {
            app.MapGet("/funciones", () => repo.GetAll());

            app.MapGet("/funciones/{id}", (int id) =>
            {
                var funcion = repo.GetById(id);
                return funcion is not null ? Results.Ok(funcion) : Results.NotFound();
            });

            app.MapPost("/funciones", (Funcion funcion) =>
            {
                repo.Add(funcion);
                return Results.Created($"/funciones/{funcion.IdFuncion}", funcion);
            });

            app.MapPut("/funciones/{id}", (int id, Funcion funcion) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                funcion.IdFuncion = id;
                repo.Update(funcion);
                return Results.Ok(funcion);
            });

            app.MapDelete("/funciones/{id}", (int id) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                repo.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
