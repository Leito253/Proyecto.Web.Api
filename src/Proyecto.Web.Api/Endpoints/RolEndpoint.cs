/*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class RolEndpoints
    {
        public static void MapRolEndpoints(this WebApplication app, RolRepository repo)
        {
            app.MapGet("/roles", () => repo.GetAll());

            app.MapGet("/roles/{id}", (int id) =>
            {
                var rol = repo.GetById(id);
                return rol is not null ? Results.Ok(rol) : Results.NotFound();
            });

            app.MapPost("/roles", (Rol rol) =>
            {
                repo.Add(rol);
                return Results.Created($"/roles/{rol.IdRol}", rol);
            });

            app.MapPut("/roles/{id}", (int id, Rol rol) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                rol.IdRol = id;
                repo.Update(rol);
                return Results.Ok(rol);
            });

            app.MapDelete("/roles/{id}", (int id) =>
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