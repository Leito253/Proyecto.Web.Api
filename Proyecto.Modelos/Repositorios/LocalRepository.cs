using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Web.Api.Repositorios
{
    public class LocalRepository
    {
        private readonly string _connectionString;

        public LocalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<Local> GetAll()
        {
            using var db = Connection;
            return db.Query<Local>("SELECT * FROM Local");
        }

        public Local? GetById(int id)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Local>(
                "SELECT * FROM Local WHERE Id = @Id", new { Id = id });
        }

        public void Add(Local local)
        {
            using var db = Connection;
            db.Open(); // Asegurarse de que la conexión esté abierta

            var sql = @"
                INSERT INTO Local (Nombre, Direccion, Capacidad, Telefono) 
                VALUES (@Nombre, @Direccion, @Capacidad, @Telefono);
                SELECT LAST_INSERT_ID();";

            /* // Ejecutar y asignar el ID
            local.Id = db.ExecuteScalar<int>(sql, local); */
        }

        public void Update(Local local)
        {
            using var db = Connection;
            var sql = "UPDATE Local SET Nombre=@Nombre, Direccion=@Direccion, Capacidad=@Capacidad, Telefono=@Telefono WHERE Id=@Id";
            db.Execute(sql, local);
        }

        public void Delete(int id)
        {
            using var db = Connection;
            var sql = "DELETE FROM Local WHERE Id=@Id";
            db.Execute(sql, new { Id = id });
        }
    }
}
