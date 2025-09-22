using Proyecto.Modelos;
using Servicios;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();
app.MapGet("/Local", (ILocalService LocalService) =>
{
    return LocalService.GetAll();
});

app.MapGet("/Local/{id}", (int id, ILocalService LocalService) =>
{
    var local = LocalService.GetById(id);
    return local is not null ? Results.Ok(local) : Results.NotFound();
});
app.MapPost("/Local", (Local newLocal, ILocalService LocalService) =>
{
    var local = LocalService.Create(newLocal);
    return Results.Created($"/Local/{local.idLocal}", local);
});
app.MapPut("/Local/{id}", (int id, Local updatedLocal, ILocalService LocalService) =>
{
    var local = LocalService.Update(id, updatedLocal);
    return local is not null ? Results.Ok(local) : Results.NotFound();
});
app.MapDelete("/Local/{id}", (int id, ILocalService LocalService) =>
{
    return LocalService.Delete(id) ? Results.NoContent() : Results.NotFound();
});
/* Evento */
app.MapGet("/Evento", (IEventoService EventoService) =>
{
    return EventoService.GetAll();
});
app.MapGet("/Evento/{id}", (int id, IEventoService EventoService) =>
{
    var evento = EventoService.GetById(id);
    return evento is not null ? Results.Ok(evento) : Results.NotFound();
});
app.MapPost("/Evento", (Eventos newEvento, IEventoService EventoService) =>
{
    var evento = EventoService.Create(newEvento);
    return Results.Created($"/Evento/{evento.idEvento}", evento);
});
app.MapPut("/Evento/{id}", (int id, Eventos updatedEvento, IEventoService
    EventoService) =>
    {
        var evento = EventoService.Update(id, updatedEvento);
        return evento is not null ? Results.Ok(evento) : Results.NotFound();
    });
app.MapDelete("/Evento/{id}", (int id, IEventoService EventoService) =>
{
    return EventoService.Delete(id) ? Results.NoContent() : Results.NotFound();
});

app.Run();

