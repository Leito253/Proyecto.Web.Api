namespace Proyecto.Core.Repositorios;

public interface IClienteRepository
{
    IEnumerable<Cliente> GetAll();
    Cliente? GetById(int DNI);
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(int DNI);

}
