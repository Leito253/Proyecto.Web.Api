using Proyecto.Modelos.Entidades;
using System.Collections.Generic;

namespace Proyecto.Modelos.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> GetAll();
        Cliente? GetById(int dni);
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int dni);
    }
}
