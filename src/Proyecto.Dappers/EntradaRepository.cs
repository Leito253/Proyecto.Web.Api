using System.Data;

namespace Proyecto.Modelos.Repositorios.ReposDapper;
public class EntradaRepository : IEntradaRepository
{
   private readonly string _connectionString;

   public EntradaRepository(IConfiguration configuration)
    {   
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }
    private IDbConnection Connection => new MySqlConnection(_connectionString);

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

    public void Update(Entrada entrada)
    {
        throw new NotImplementedException();
    }
}