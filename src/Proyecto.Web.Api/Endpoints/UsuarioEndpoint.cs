/*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app, UsuarioRepository repo)
        {
            app.MapGet("/usuarios", () => repo.GetAll());

            app.MapGet("/usuarios/{id}", (int id) =>
            {
                var usuario = repo.GetById(id);
                return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
            });

            app.MapPost("/usuarios", (Usuario usuario) =>
            {
                repo.Add(usuario);
                return Results.Created($"/usuarios/{usuario.IdUsuario}", usuario);
            });

            app.MapPut("/usuarios/{id}", (int id, Usuario usuario) =>
            {
                var existente = repo.GetById(id);
                if (existente is null) return Results.NotFound();

                usuario.IdUsuario = id;
                repo.Update(usuario);
                return Results.Ok(usuario);
            });

            app.MapDelete("/usuarios/{id}", (int id) =>
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