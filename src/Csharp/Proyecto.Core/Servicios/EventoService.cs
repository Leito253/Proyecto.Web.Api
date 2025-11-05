using Proyecto.Modelos.Entidades;

namespace Servicios;
public class EventoService : IEventoService
{
    private readonly List<Evento> _eventos = new List<Evento>();

    /* public EventoService()
    {
        // Inicializar con algunos datos de ejemplo
        _eventos.AddRange(new[]
        {
            new Evento { idEvento = 1, Nombre = "Partido de Fútbol", Fecha = new DateTime(2023, 11, 15), LocalId = 1, Local = new Local { idLocal = 1, Nombre = "Cancha Monumental", Direccion = "Av. Figueroa Alcorta 7597, CABA"}, Precio = 20000 },
            new Evento { idEvento = 2, Nombre = "Concierto de Rock", Fecha = new DateTime(2023, 12, 5), LocalId = 2, Local = new Local { idLocal = 2, Nombre = "Luna Park", Direccion = "Av. Eduardo Madero 470, C1106 Cdad. Autónoma de Buenos Aires" }, Precio = 15000 },
            new Evento { idEvento = 3, Nombre = "Obra de Teatro", Fecha = new DateTime(2024, 1, 20), LocalId = 3, Local = new Local { idLocal = 3, Nombre = "Teatro Gran Rex", Direccion = "Av. Corrientes 857" }, Precio = 8000 },
            new Evento { idEvento = 4, Nombre = "Festival de Música", Fecha = new DateTime(2024, 2, 10), LocalId = 4, Local = new Local { idLocal = 4, Nombre = "Estadio Único Diego Armando Maradona", Direccion = "Av. Pres. Juan Domingo Perón 3500, La Plata, Buenos Aires" }, Precio = 25000 },
            new Evento { idEvento = 5, Nombre = "Exposición de Arte", Fecha = new DateTime(2024, 3, 15), LocalId = 5, Local = new Local { idLocal = 5, Nombre = "Museo de Arte Latinoamericano de Buenos Aires (MALBA)", Direccion = "Av. Figueroa Alcorta 3415, C1425 CABA" }, Precio = 5000 }
            
        });
    } */

    public Evento Create(Evento newEvento)
    {
        throw new NotImplementedException();
        
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Evento> GetAll()
    {
        return _eventos;
    }

    public Evento? GetById(int id)
    {
        return _eventos.FirstOrDefault(e => e.idEvento == id);
    }

    public Evento? Update(int id, Evento updatedEvento)
    {
        throw new NotImplementedException();
    }
}
