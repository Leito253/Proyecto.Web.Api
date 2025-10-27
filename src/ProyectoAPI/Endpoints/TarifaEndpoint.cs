/*
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;

public static class TarifaEndpoints
{
    public static void MapTarifaEndpoints(this WebApplication app)
    {
        app.MapGet("/api/tarifa", (ITarifaRepository repo) =>
        {
            return Results.Ok(repo.GetAll());
        });

        app.MapGet("/api/tarifa/{id:int}", (int id, ITarifaRepository repo) =>
        {
            var tarifa = repo.GetById(id);
            return tarifa is null ? Results.NotFound() : Results.Ok(tarifa);
        });

        app.MapPost("/api/tarifa", (Tarifa tarifa, ITarifaRepository repo) =>
        {
            repo.Add(tarifa);
            return Results.Created($"/api/tarifa/{tarifa.IdTarifa}", tarifa);
        });

        app.MapPut("/api/tarifa/{id:int}", (int id, Tarifa tarifa, ITarifaRepository repo) =>
        {
            if (tarifa.IdTarifa != id)
                return Results.BadRequest();

            repo.Update(tarifa);
            return Results.NoContent();
        });

        app.MapDelete("/api/tarifa/{id:int}", (int id, ITarifaRepository repo) =>
        {
            repo.Delete(id);
            return Results.NoContent();
        });
    }
}
*/
