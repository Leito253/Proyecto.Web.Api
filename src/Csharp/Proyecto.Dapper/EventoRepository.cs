using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper
{
    public class EventoRepository : IEventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection")!;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<Evento> GetAll()
        {
            using var db = Connection;
            return db.Query<Evento>("SELECT * FROM Evento");
        }

        public Evento? GetById(int idEvento)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Evento>(
                "SELECT * FROM Evento WHERE idEvento = @Id", new { Id = idEvento });
        }

        public void Add(Evento evento)
        {
            using var db = Connection;
            string sql = @"INSERT INTO Evento (Nombre, Fecha, Lugar, Tipo, IdLocal, Activo)
                            VALUES (@Nombre, @Fecha, @Lugar, @Tipo, @IdLocal, @Activo)";
            db.Execute(sql, evento);
        }

        public void Update(Evento evento)
        {
            using var db = Connection;
            string sql = @"UPDATE Evento 
                            SET Nombre=@Nombre, Descripcion=@Descripcion, Fecha=@Fecha 
                            WHERE idEvento=@idEvento";
            db.Execute(sql, evento);
        }

        public void Publicar(int idEvento)
        {
            using var db = Connection;
            db.Execute("UPDATE Evento SET ='Publicado' WHERE idEvento=@Id", new { Id = idEvento });
        }

        public void Cancelar(int idEvento)
        {
            using var db = Connection;
            db.Execute("UPDATE Evento SET Estado='Cancelado' WHERE idEvento=@Id", new { Id = idEvento });
        }

       public void Delete(int idEvento)
        {
            using var db = Connection;
            db.Execute("DELETE FROM Evento WHERE idEvento=@Id", new { Id = idEvento });
        }

    }
}
