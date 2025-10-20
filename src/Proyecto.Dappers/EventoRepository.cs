using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;

namespace Proyecto.Modelos.Repositorios.ReposDapper
{
    public class EventoRepository : IEventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public void Add(Evento evento)
        {
            using var db = Connection;
            string sql = @"INSERT INTO Evento (Nombre, Descripcion, Fecha, Estado)
                       VALUES (@Nombre, @Descripcion, @Fecha, @Estado)";
            db.Execute(sql, evento);
        }

        public IEnumerable<Evento> GetAll()
        {
            using var db = Connection;
            return db.Query<Evento>("SELECT * FROM Evento");
        }

        public Evento? GetById(int idEvento)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Evento>(
                "SELECT * FROM Evento WHERE IdEvento = @Id", new { Id = idEvento });
        }

        public void Update(Evento evento)
        {
            using var db = Connection;
            string sql = @"UPDATE Evento 
                       SET Nombre=@Nombre, Descripcion=@Descripcion, Fecha=@Fecha 
                       WHERE IdEvento=@IdEvento";
            db.Execute(sql, evento);
        }

        public void Publicar(int idEvento)
        {
            using var db = Connection;
            db.Execute("UPDATE Evento SET Estado='Publicado' WHERE IdEvento=@Id", new { Id = idEvento });
        }

        public void Cancelar(int idEvento)
        {
            throw new NotImplementedException();
        }

        internal void Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Evento> IEventoRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Evento? IEventoRepository.GetById(int idEvento)
        {
            throw new NotImplementedException();
        }

        public void Add(Evento evento)
        {
            throw new NotImplementedException();
        }

        public void Update(Evento evento)
        {
            throw new NotImplementedException();
        }
    }
}