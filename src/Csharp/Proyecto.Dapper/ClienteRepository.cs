using System.Data;
using Proyecto.Core.Entidades;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;



namespace Proyecto.Core.Repositorios.ReposDapper
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {   
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);

    
        public IEnumerable<Cliente> GetAll()
        {
            using var db = Connection;
            var clientes = db.Query<Cliente>("SELECT * FROM Cliente").ToList();
            return clientes;
        }

        Cliente? IClienteRepository.GetById(int DNI)
        {
                using var db = Connection;
            return db.QueryFirstOrDefault<Cliente>("SELECT * FROM Cliente WHERE DNI=@DNI", new { dni = DNI });
        }


        public void Add(Cliente cliente)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Cliente (DNI, Nombre, Apellido, Email, telefono) 
                        VALUES (@DNI, @Nombre, @Apellido, @Email, @telefono)";
            db.Execute(sql, cliente);
        }

        public void Update(Cliente cliente)
        {
            using var db = Connection;
            var sql = @"UPDATE Cliente SET DNI=@DNI, Nombre=@Nombre, Apellido=@Apellido, Email=@Email, Telefono=@Telefono 
                        WHERE DNI=@dni";
            db.Execute(sql, cliente);
        }

        public void Delete(int DNI)
        {
            using var db = Connection;
            db.Execute("DELETE FROM Cliente WHERE DNI=@DNI", new { dni = DNI });
        }
    
    }
}