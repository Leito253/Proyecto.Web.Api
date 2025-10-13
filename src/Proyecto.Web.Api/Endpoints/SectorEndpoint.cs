using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class SectorEndpoints
    {
        public static void MapSectorEndpoints(this WebApplication app, SectorRepository repo)
        {
            app.MapGet("/sectores", () => repo.GetAll());

            app.MapGet("/sectores/{id}", (int id) =>
            {
                var sector = repo.GetById(id);
                return sector is not null ? Results.Ok(sector) : Results.NotFound();
            });

            app.MapPost("/sectores", (Sector sector) =>
            {
                repo.Add(sector);
                return Results.Created($"/sectores/{sector.idSector}", sector);
            });

            app.MapPut("/sectores/{id}", (int id, Sector sector) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                sector.idSector = id;
                repo.Update(sector);
                return Results.Ok(sector);
            });

            app.MapDelete("/sectores/{id}", (int id) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                repo.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
