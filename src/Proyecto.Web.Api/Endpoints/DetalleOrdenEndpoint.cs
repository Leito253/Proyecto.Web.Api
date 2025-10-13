using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class DetalleOrdenEndpoints
    {
        public static void MapDetalleOrdenEndpoints(this WebApplication app, DetalleOrdenRepository repo)
        {
            app.MapGet("/detalleordenes", () => repo.GetAll());

            app.MapGet("/detalleordenes/{id}", (int id) =>
            {
                var detalle = repo.GetById(id);
                return detalle is not null ? Results.Ok(detalle) : Results.NotFound();
            });

            app.MapPost("/detalleordenes", (DetalleOrden detalle) =>
            {
                repo.Add(detalle);
                return Results.Created($"/detalleordenes/{detalle.IdDetalleOrden}", detalle);
            });

            app.MapPut("/detalleordenes/{id}", (int id, DetalleOrden detalle) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                detalle.IdDetalleOrden = id;
                repo.Update(detalle);
                return Results.Ok(detalle);
            });

            app.MapDelete("/detalleordenes/{id}", (int id) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                repo.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
