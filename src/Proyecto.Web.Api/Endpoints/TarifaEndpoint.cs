using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class TarifaEndpoints
    {
        public static void MapTarifaEndpoints(this WebApplication app, TarifaRepository repo)
        {
            app.MapGet("/tarifas", () => repo.GetAll());

            app.MapGet("/tarifas/{id}", (int id) =>
            {
                var tarifa = repo.GetById(id);
                return tarifa is not null ? Results.Ok(tarifa) : Results.NotFound();
            });

            app.MapPost("/tarifas", (Tarifa tarifa) =>
            {
                repo.Add(tarifa);
                return Results.Created($"/tarifas/{tarifa.idTarifa}", tarifa);
            });

            app.MapPut("/tarifas/{id}", (int id, Tarifa tarifa) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                tarifa.idTarifa = id;
                repo.Update(tarifa);
                return Results.Ok(tarifa);
            });

            app.MapDelete("/tarifas/{id}", (int id) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                repo.Delete(id);
                return Results.NoContent();
            });
        }
    }
}
