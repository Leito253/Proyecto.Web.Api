using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos;

namespace Servicios;
public class EventoService : IEventoService
{
    private readonly List<Eventos> _eventos = new List<Eventos>();

    public EventoService()
    {
        // Inicializar con algunos datos de ejemplo
        _eventos.AddRange(new[]
        {
            new Eventos { idEvento = 1, Nombre = "Partido de Fútbol", Fecha = new DateTime(2023, 11, 15), LocalId = 1, Local = new Local { idLocal = 1, Nombre = "Cancha Monumental", Direccion = "Av. Figueroa Alcorta 7597, CABA"}, Precio = 20000 },
            new Eventos { idEvento = 2, Nombre = "Concierto de Rock", Fecha = new DateTime(2023, 12, 5), LocalId = 2, Local = new Local { idLocal = 2, Nombre = "Luna Park", Direccion = "Av. Eduardo Madero 470, C1106 Cdad. Autónoma de Buenos Aires" }, Precio = 15000 },
            new Eventos { idEvento = 3, Nombre = "Obra de Teatro", Fecha = new DateTime(2024, 1, 20), LocalId = 3, Local = new Local { idLocal = 3, Nombre = "Teatro Gran Rex", Direccion = "Av. Corrientes 857" }, Precio = 8000 },
            new Eventos { idEvento = 4, Nombre = "Festival de Música", Fecha = new DateTime(2024, 2, 10), LocalId = 4, Local = new Local { idLocal = 4, Nombre = "Estadio Único Diego Armando Maradona", Direccion = "Av. Pres. Juan Domingo Perón 3500, La Plata, Buenos Aires" }, Precio = 25000 },
            new Eventos { idEvento = 5, Nombre = "Exposición de Arte", Fecha = new DateTime(2024, 3, 15), LocalId = 5, Local = new Local { idLocal = 5, Nombre = "Museo de Arte Latinoamericano de Buenos Aires (MALBA)", Direccion = "Av. Figueroa Alcorta 3415, C1425 CABA" }, Precio = 5000 }
            
        });
    }

    public Eventos Create(Eventos newEvento)
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

    public IEnumerable<Eventos> GetAll()
    {
        return _eventos;
    }

    public Eventos? GetById(int id)
    {
        return _eventos.FirstOrDefault(e => e.idEvento == id);
    }

    public Eventos? Update(int id, Eventos updatedEvento)
    {
        throw new NotImplementedException();
    }
}
