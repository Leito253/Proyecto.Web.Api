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
            return db.Query<Cliente>("SELECT * FROM Cliente").ToList();
        }
        public Cliente? GetById(int idCliente)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Cliente>(
                "SELECT * FROM Cliente WHERE idCliente = @idCliente",
                new { idCliente }
            );
        }
        public int Add(Cliente cliente)
        {
            using var connection = new MySqlConnection(_connectionString);

            string sql = @"
                INSERT INTO Cliente (Dni, Nombre, Apellido, Email, Telefono)
                VALUES (@Dni, @Nombre, @Apellido, @Email, @Telefono);
                SELECT LAST_INSERT_ID();";

            int id = connection.ExecuteScalar<int>(sql, cliente);
            cliente.idCliente = id;

            return id;
        }
        public void Update(Cliente cliente)
        {
            using var db = Connection;

            var sql = @"
                UPDATE Cliente 
                SET Dni = @Dni, 
                    Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    Email = @Email, 
                    Telefono = @Telefono
                WHERE idCliente = @idCliente;";

            db.Execute(sql, cliente);
        }
    }
}