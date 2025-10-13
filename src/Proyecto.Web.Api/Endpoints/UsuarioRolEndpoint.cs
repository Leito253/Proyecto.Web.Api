using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class UsuarioRolEndpoints
    {
        public static void MapUsuarioRolEndpoints(this WebApplication app, UsuarioRolRepository repo)
        {
            app.MapGet("/usuarioroles", () => repo.GetAll());

            app.MapGet("/usuarioroles/{idUsuario}/{idRol}", (int idUsuario, int idRol) =>
            {
                var ur = repo.GetById(idUsuario, idRol);
                return ur is not null ? Results.Ok(ur) : Results.NotFound();
            });

            app.MapPost("/usuarioroles", (UsuarioRol ur) =>
            {
                repo.Add(ur);
                return Results.Created($"/usuarioroles/{ur.IdUsuario}/{ur.IdRol}", ur);
            });

            app.MapDelete("/usuarioroles/{idUsuario}/{idRol}", (int idUsuario, int idRol) =>
            {
                var existente = repo.GetById(idUsuario, idRol);
                if (existente is null) return Results.NotFound();

                repo.Delete(idUsuario, idRol);
                return Results.NoContent();
            });
        }
    }
}
