using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;
using src.Proyecto.Modelos.Repositorios;

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

        // 🔹 Obtener todos los roles
        public IEnumerable<Rol> GetAll()
        {
            using var db = Connection;
            const string sql = "SELECT IdRol, Nombre FROM Rol;";
            return db.Query<Rol>(sql);
        }

        // 🔹 Agregar un nuevo rol
        public void Add(Rol rol)
        {
            using var db = Connection;
            const string sql = "INSERT INTO Rol (Nombre) VALUES (@Nombre);";
            db.Execute(sql, new { rol.Nombre });
        }

        // 🔹 Obtener un rol por ID (opcional)
        public Rol? GetById(int idRol)
        {
            using var db = Connection;
            const string sql = "SELECT IdRol, Nombre FROM Rol WHERE IdRol = @IdRol;";
            return db.QueryFirstOrDefault<Rol>(sql, new { Id = idRol });
        }

        // 🔹 Eliminar un rol (opcional)
        public void Delete(int idRol)
        {
            using var db = Connection;
            const string sql = "DELETE FROM Rol WHERE IdRol = @IdRol;";
            db.Execute(sql, new { Id = idRol });
        }
    }
}
