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

        public IEnumerable<Clientes> GetAll()
        {
            using var db = Connection;
            return db.Query<Clientes>("SELECT * FROM Clientes");
        }

        public Clientes? GetById(int dni)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Clientes>("SELECT * FROM Clientes WHERE DNI=@DNI", new { DNI = DNI });
        }

        public void Add(Clientes clientes)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Clientes (DNI, Nombre, Apellido, Email, contrasenia) 
                        VALUES (@DNI, @Nombre, @Apellido, @Email, @contrasenia)";
            db.Execute(sql, clientes);
        }

        public void Update(Clientes clientes)
        {
            using var db = Connection;
            var sql = @"UPDATE Clientes SET DNI=@DNI, Nombre=@Nombre, Apellido=@Apellido, Email=@Email, contrasenia=@contrasenia 
                        WHERE DNI=@dni";
            db.Execute(sql, clientes);
        }

        public void Delete(int dni)
        {
            using var db = Connection;
            db.Execute("DELETE FROM Clientes WHERE DNI=@DNI", new { DNI = dni });
        }
    }
}