
public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this WebApplication app)
    {
        app.MapGet("/api/cliente", (IClienteRepository repo) =>
        {
            return Results.Ok(repo.GetAll());
        });

        app.MapGet("/api/cliente/{DNI:int}", (int DNI, IClienteRepository repo) =>
        {
            var cliente = repo.GetById(DNI);
            return cliente is null ? Results.NotFound() : Results.Ok(cliente);
        });

        app.MapPost("/api/cliente", (Cliente cliente, IClienteRepository repo) =>
        {
            repo.Add(cliente);
            return Results.Created($"/api/cliente/{cliente.DNI}", cliente);
        });

        app.MapPut("/api/cliente/{DNI:int}", (int DNI, Cliente cliente, IClienteRepository repo) =>
        {
            if (cliente.DNI != DNI)
                return Results.BadRequest();

            repo.Update(cliente);
            return Results.NoContent();
        });

        app.MapDelete("/api/cliente/{DNI:int}", (int DNI, IClienteRepository repo) =>
        {
            repo.Delete(DNI);
            return Results.NoContent();
        });
    }
}
