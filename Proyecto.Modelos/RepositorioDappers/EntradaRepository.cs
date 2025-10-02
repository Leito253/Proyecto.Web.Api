using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;


namespace Proyecto.Web.Api.Repositorios
{
    public class EntradaRepository : IEntradaRepository
    {
       private readonly string _connectionString;

       public EntradaRepository(IConfiguration configuration)
        {   
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public void Anular(int id)
        {
            
        }

        public IEnumerable<Entrada> GetAll()
        {
            using var db = Connection;
            return db.Query<Entrada>("SELECT * FROM Entrada ");
        }

        public Entrada? GetById(int idEntrada)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Entrada>("SELECT * FROM Entrada WHERE idEntrada=@idEntrada", new { Id = idEntrada });
        }
    }
}