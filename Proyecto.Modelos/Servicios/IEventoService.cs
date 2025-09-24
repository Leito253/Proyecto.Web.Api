using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos.Entidades;
using Servicios;


namespace Servicios;

public interface IEventoService
{
    IEnumerable<Evento> GetAll();
    Evento? GetById(int id);
    Evento Create(Evento newEvento);
    Evento? Update(int id, Evento updatedEvento);
    bool Delete(int id);
    bool Exists(int id);
}
