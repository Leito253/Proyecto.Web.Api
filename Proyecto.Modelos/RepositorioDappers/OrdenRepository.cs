using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Repositorios;
using Proyecto.Modelos.Entidades;
using System.Data;

namespace Proyecto.Modelos.RepositorioDappers;

public class OrdenRepository : IOrdenRepository
{
    private readonly string _connectionString;

        public OrdenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);

    public void Add(Orden orden)
    {
        throw new NotImplementedException();
    }

    public void Cancelar(int NumeroOrden)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Orden> GetAll()
    {
        throw new NotImplementedException();
    }

    public Orden? GetById(int NumeroOrden)
    {
        throw new NotImplementedException();
    }

    public void Pagar(int NumeroOrden)
    {
        throw new NotImplementedException();
    }

    public void Update(Orden orden)
    {
        throw new NotImplementedException();
    }
}
