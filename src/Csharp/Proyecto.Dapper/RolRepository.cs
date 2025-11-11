using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Entidades;

namespace src.Proyecto.Dappers
{
    public class RolRepository : IRolRepository
    {
        private readonly string _connectionString;

        public RolRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<Rol> GetAll()
        {
            using var db = Connection;
            const string sql = "SELECT IdRol, Nombre FROM Rol;";
            return db.Query<Rol>(sql);
        }

        public void Add(Rol rol)
        {
            using var db = Connection;
            const string sql = "INSERT INTO Rol (Nombre) VALUES (@Nombre);";
            db.Execute(sql, new { rol.Nombre });
        }

        public Rol? GetById(int idRol)
        {
            using var db = Connection;
            const string sql = "SELECT IdRol, Nombre FROM Rol WHERE IdRol = @IdRol;";
            return db.QueryFirstOrDefault<Rol>(sql, new { Id = idRol });
        }

        public void Delete(int idRol)
        {
            using var db = Connection;
            const string sql = "DELETE FROM Rol WHERE IdRol = @IdRol;";
            db.Execute(sql, new { Id = idRol });
        }
    }
}
