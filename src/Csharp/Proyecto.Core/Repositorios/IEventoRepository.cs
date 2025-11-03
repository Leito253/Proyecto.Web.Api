namespace Proyecto.Core.Interfaces
{
    public interface IEventoRepository
    {
        IEnumerable<Evento> GetAll();
        Evento? GetById(int idEvento);
        void Add(Evento evento);
        void Update(Evento evento);
        void Publicar(int idEvento);
        void Cancelar(int idEvento);
        void Delete(int idEvento);
    }
}
