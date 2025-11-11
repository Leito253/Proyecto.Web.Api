using Proyecto.Core.Entidades;
namespace Proyecto.Core.Repositorios
{
    public interface ITarifaRepository
    {
        void Add(Tarifa tarifa);
        IEnumerable<Tarifa> GetByFuncionId(int IdFuncion);
        Tarifa? GetById(int idTarifa);
        void Update(Tarifa tarifa);


    }
}
