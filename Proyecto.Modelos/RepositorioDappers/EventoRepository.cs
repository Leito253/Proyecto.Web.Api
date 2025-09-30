using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Modelos.Repositorios;

namespace Proyecto.Web.Api.Repositorios
{
    public class EventoRepository : IEventoRepository
    {
       private readonly string _connectionString;

        public EventoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        
        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public void Add(Evento evento)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Evento () 
                        VALUES (@Nombre, @Fecha, @Tipo, @IdLocal, @IdEvento, @Lugar, @Local)";
            db.Execute(sql, evento);
            
        }

        public void Cancelar(int idEvento)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> GetAll()
        {
            throw new NotImplementedException();
        }

        public Evento? GetById(int idEvento)
        {
            throw new NotImplementedException();
        }

        public void Publicar(int idEvento)
        {
            throw new NotImplementedException();
        }

        public void Update(Evento evento)
        {
            throw new NotImplementedException();
        }
    }
}
