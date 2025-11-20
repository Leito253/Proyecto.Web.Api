using Proyecto.Core.Entidades;
using Proyecto.Core.Repositorios;
using Servicios.Interfaces;

namespace Proyecto.Core.Servicios;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;
    public ClienteService(IClienteRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Cliente>> ObtenerTodos()
    {
        return await Task.FromResult(_repo.GetAll());
    }

    public async Task<Cliente?> ObtenerPorId(int id)
    {
        return await Task.FromResult(_repo.GetById(id));
    }

    public async Task<bool> Crear(Cliente cliente)
    {
        if (cliente.DNI <= 0) return false;
        if (string.IsNullOrWhiteSpace(cliente.Nombre)) return false;
        if (string.IsNullOrWhiteSpace(cliente.Apellido)) return false;
        if (string.IsNullOrWhiteSpace(cliente.Email)) return false;
        var id = _repo.Add(cliente);
        return await Task.FromResult(id > 0);
    }

    public async Task<bool> Actualizar(Cliente cliente)
    {
        if (cliente.DNI <= 0) return false;
        if (string.IsNullOrWhiteSpace(cliente.Nombre)) return false;
        if (string.IsNullOrWhiteSpace(cliente.Apellido)) return false;
        if (string.IsNullOrWhiteSpace(cliente.Email)) return false;
        _repo.Update(cliente);
        return await Task.FromResult(true);
    }

    public async Task<bool> Eliminar(int id)
    {
        var cliente = _repo.GetById(id);
        if (cliente == null) return false;
        return await Task.FromResult(true);
    }
}
