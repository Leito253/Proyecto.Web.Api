using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper
{
public class EntradaRepository : IEntradaRepository
{
    private readonly string _connectionString;

    public EntradaRepository(IConfiguration configuration)
    {   
        _connectionString = configuration.GetConnectionString("MySqlConnection")!;
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

    public IEnumerable<Entrada> GetAll()
    {
        using var db = Connection;
        string sql = "SELECT * FROM Entrada;";
        return db.Query<Entrada>(sql);
    }

    public Entrada? GetById(int idEntrada)
    {
        using var db = Connection;
        string sql = "SELECT * FROM Entrada WHERE idEntrada = @idEntrada;";
        return db.QueryFirstOrDefault<Entrada>(sql, new { idEntrada });
    }

    public void Update(Entrada entrada)
    {
        using var db = Connection;
        string sql = @"
            UPDATE Entrada SET
                Precio = @Precio,
                idFuncion = @idFuncion,
                idCliente = @idCliente,
                Usada = @Usada,
                Anulada = @Anulada,
                Numero = @Numero,
                QR = @QR
            WHERE idEntrada = @idEntrada;";
        db.Execute(sql, entrada);
    }

    public void Anular(int idEntrada)
    {
        using var db = Connection;
        string sqlCheck = "SELECT Estado FROM Entrada WHERE idEntrada = @idEntrada;";
        var estado = db.QueryFirstOrDefault<string>(sqlCheck, new { idEntrada });

        if (estado == null) throw new Exception("La entrada no existe.");
        if (estado == "Anulada") throw new Exception("La entrada ya está anulada.");

        string sqlUpdate = "UPDATE Entrada SET Estado = 'Anulada' WHERE idEntrada = @idEntrada;";
        db.Execute(sqlUpdate, new { idEntrada });
    }

    public void Add(Entrada entrada)
    {
        using var db = Connection;
        string sql = @"
            INSERT INTO Entrada (Precio, idFuncion, idCliente, Usada, Anulada, Numero, QR)
            VALUES (@Precio, @idFuncion, @idCliente, @Usada, @Anulada, @Numero, @QR);
            SELECT LAST_INSERT_ID();";
        entrada.IdEntrada = db.ExecuteScalar<int>(sql, entrada);
    }
}
}