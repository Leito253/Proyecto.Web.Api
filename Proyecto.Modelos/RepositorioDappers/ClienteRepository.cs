using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Web.Api.Repositorios
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<Cliente> GetAll()
        {
            using var db = Connection;
            return db.Query<Cliente>("SELECT * FROM Clientes");
        }

        public Cliente? GetById(int dni)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Cliente>("SELECT * FROM Clientes WHERE DNI=@DNI", new { DNI = dni });
        }

        public void Add(Cliente cliente)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Clientes (DNI, Nombre, Apellido, Email, contrasenia) 
                        VALUES (@DNI, @Nombre, @Apellido, @Email, @contrasenia)";
            db.Execute(sql, cliente);
        }

        public void Update(Cliente cliente)
        {
            using var db = Connection;
            var sql = @"UPDATE Clientes SET DNI=@DNI, Nombre=@Nombre, Apellido=@Apellido, Email=@Email, contrasenia=@contrasenia 
                        WHERE DNI=@dni";
            db.Execute(sql, cliente);
        }

        public void Delete(int dni)
        {
            using var db = Connection;
            db.Execute("DELETE FROM Clientes WHERE DNI=@DNI", new { DNI = dni });
        }
    }
}