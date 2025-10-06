using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.RepositorioDappers;
using Proyecto.Web.Api.Repositorios;

namespace Proyecto.Web.Api.Endpoints
{
    public static class EventosEndpoints
    {
        public static void MapEventosEndpoints(this WebApplication app, EventoRepository repo)
        {
            app.MapGet("/eventos", () => repo.GetAll());

            app.MapGet("/eventos/{id}", (int id) =>
            {
                var evento = repo.GetById(id);
                return evento is not null ? Results.Ok(evento) : Results.NotFound();
            });

            app.MapPost("/eventos", (Evento evento) =>
            {
                repo.Add(evento);
                return Results.Created($"/eventos/{evento.idEvento}", evento);
            });

            app.MapPut("/eventos/{id}", (int id, Evento evento) =>
            {
                var existente = repo.GetById(id);
                if (existente == null) return Results.NotFound();

                evento.idEvento = id;
                repo.Update(evento);
                return Results.Ok(evento);
            });

            app.MapDelete("/eventos/{id}", (int id) =>
            {
                var existente = repo.GetById(id);
                if (existente == null) return Results.NotFound();

                repo.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
