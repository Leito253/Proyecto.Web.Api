using Proyecto.Modelos.Entidades;
using System.Collections.Generic;

namespace Proyecto.Modelos.Interfaces
{
    public interface ILocalRepository
    {
        IEnumerable<Local> GetAll();
        Local? GetById(int id); // <- nullabilidad correcta
        void Add(Local local);
        void Update(Local local);
        void Delete(int id);
    }
}
