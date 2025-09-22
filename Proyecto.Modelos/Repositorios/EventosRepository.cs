using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Web.Api.Repositorios
{
    public class EventRepository
    {
        private readonly string _connectionString;

        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<Evento> GetAll()
        {
            using var db = Connection;
            return db.Query<Evento>("SELECT * FROM Eventos");
        }

        public Evento? GetById(int id)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Evento>("SELECT * FROM Eventos WHERE Id=@Id", new { Id = id });
        }

        public void Add(Evento evento)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Eventos (Nombre, Fecha, LocalId, Descripcion) 
                        VALUES (@Nombre, @Fecha, @LocalId, @Descripcion)";
            db.Execute(sql, evento);
        }

        public void Update(Evento evento)
        {
            using var db = Connection;
            var sql = @"UPDATE Eventos SET Nombre=@Nombre, Fecha=@Fecha, LocalId=@LocalId, Descripcion=@Descripcion 
                        WHERE Id=@Id";
            db.Execute(sql, evento);
        }

        public void Delete(int id)
        {
            using var db = Connection;
            db.Execute("DELETE FROM Eventos WHERE Id=@Id", new { Id = id });
        }
    }
}
