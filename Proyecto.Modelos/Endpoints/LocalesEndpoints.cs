using Proyecto.Modelos.Entidades;
using Proyecto.Web.Api.Repositorios;

namespace Proyecto.Web.Api.Endpoints
{
    public static class LocalesEndpoints
    {
        public static void MapLocalesEndpoints(this WebApplication app, LocalRepository repo)
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
                return Results.Created($"/locales/{local.Id_local}", local);
            });

            app.MapPut("/locales/{id}", (int id, Local local) =>
            {
                var existente = repo.GetById(id);
                if (existente == null) return Results.NotFound();

                local.Id_local = id;
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
}
