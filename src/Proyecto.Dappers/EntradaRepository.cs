using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios.ReposDapper;
public class EntradaRepository : IEntradaRepository
{
   private readonly string _connectionString;

   public EntradaRepository(IConfiguration configuration)
    {   
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }
    private IDbConnection Connection => new MySqlConnection(_connectionString);

    public IEnumerable<Entrada> GetAll()
    {
        throw new NotImplementedException();
    }

    public Entrada? GetById(int idEntrada)
    {
        throw new NotImplementedException();
    }

    public void Update(Entrada entrada)
    {
        throw new NotImplementedException();
    }

    public void Anular(int IdEntrada)
    {
         using var db = Connection;

        string sqlCheck = "SELECT Estado FROM Entrada WHERE IdEntrada = @IdEntrada;";
        var estado = db.QueryFirstOrDefault<string>(sqlCheck, new { Id = IdEntrada });

        if (estado == null)
            throw new Exception("La entrada no existe.");

        if (estado == "Anulada")
            throw new Exception("La entrada ya está anulada.");

        string sqlUpdate = "UPDATE Entrada SET Estado = 'Anulada' WHERE IdEntrada = @IdEntrada;";
        db.Execute(sqlUpdate, new { Id = IdEntrada });
        
    }

    public void Add(Entrada entrada)
    {
        throw new NotImplementedException();
    }
}