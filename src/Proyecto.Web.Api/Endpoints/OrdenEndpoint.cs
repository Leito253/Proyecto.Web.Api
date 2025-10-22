using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class OrdenEndpoints
    {
        public static void MapOrdenEndpoints(this WebApplication app, OrdenRepository repo)
        {
            app.MapGet("/ordenes", () => repo.GetAll());

            app.MapGet("/ordenes/{id}", (int id) =>
            {
                var orden = repo.GetById(id);
                return orden is not null ? Results.Ok(orden) : Results.NotFound();
            });

            app.MapPost("/ordenes", (Orden orden) =>
            {
                repo.Add(orden);
                return Results.Created($"/ordenes/{orden.idOrden}", orden);
            });

            app.MapPut("/ordenes/{id}", (int id, Orden orden) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                orden.idOrden = id;
                repo.Update(orden);
                return Results.Ok(orden);
            });

        }
    }
}
