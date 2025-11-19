using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios;
public interface IClienteRepository
{
    int Add(Cliente cliente);
    IEnumerable<Cliente> GetAll();
    Cliente? GetById(int idCliente);
    void Update(Cliente cliente);
}