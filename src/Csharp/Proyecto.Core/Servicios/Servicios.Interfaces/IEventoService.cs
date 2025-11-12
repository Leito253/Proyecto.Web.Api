using Proyecto.Core.Entidades;

namespace Servicios.Interfaces
{
    public interface IEventoService
    {
        IEnumerable<Evento> GetAll();
        Evento? GetById(int idEvento);
        Evento Create(Evento newEvento);
        Evento? Update(int idEvento, Evento updatedEvento);
        bool Delete(int idEvento);
        bool Exists(int idEvento);
    }
}
