using Proyecto.Modelos.Entidades;
using System.Collections.Generic;

namespace Proyecto.Modelos.Interfaces
{
    public interface ITarifaRepository
    {
        void Add(Tarifa tarifa);
        IEnumerable<Tarifa> GetByFuncionId(int IdFuncion);
        Tarifa? GetById(int idTarifa);
        void Update(Tarifa tarifa);

        
    }
}
