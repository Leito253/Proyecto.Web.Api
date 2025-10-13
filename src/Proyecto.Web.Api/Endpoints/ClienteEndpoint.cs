using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios.ReposDapper;

namespace Proyecto.Web.Api.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this WebApplication app, ClienteRepository repo)
    {
        app.MapGet("/clientes", () => repo.GetAll());

        app.MapGet("/clientes/{id}", (int id) =>
        {
            var cliente = repo.GetById(id);
            return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
        });

        app.MapPost("/clientes", (Cliente cliente) =>
        {
            repo.Add(cliente);
            return Results.Created($"/clientes/{cliente.idCliente}", cliente);
        });

        app.MapPut("/clientes/{id}", (int id, Cliente cliente) =>
        {
            var existente = repo.GetById(id);
            if (existente is null) return Results.NotFound();

            cliente.idCliente = id;
            repo.Update(cliente);
            return Results.Ok(cliente);
        });

        app.MapDelete("/clientes/{id}", (int id) =>
        {
            var existente = repo.GetById(id);
            if (existente is null) return Results.NotFound();

            repo.Delete(id);
            return Results.NoContent();
        });
    }
}
