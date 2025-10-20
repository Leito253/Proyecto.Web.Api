using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Interfaces;

public interface IEventoRepository
{
    IEnumerable<Evento> GetAll();
    Evento? GetById(int idEvento);
    void Add(Evento evento);
    void Update(Evento evento);

    void Publicar(int idEvento);

    void Cancelar(int idEvento); 


}