using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;

public class EntradaRepository : IEntradaRepository
{
    private readonly string _connectionString;
    private readonly string _connStr;

    public EntradaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    public EntradaRepository(string connStr)
    {
        _connStr = connStr;
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString ?? _connStr);

    public IEnumerable<Entrada> GetAll()
    {
        using var connection = Connection;
        return connection.Query<Entrada>("SELECT * FROM Entrada");
    }
    public Entrada? GetById(int idEntrada)
    {
        using var connection = Connection;
        return connection.QueryFirstOrDefault<Entrada>(
            "SELECT * FROM Entrada WHERE IdEntrada = @idEntrada",
            new { idEntrada });
    }
    public void Add(Entrada entrada)
    {
        using var connection = Connection;

        var sql = @"
            INSERT INTO Entrada (Precio, QR, Usada, Anulada, FechaUso, Numero, IdDetalleOrden, IdCliente, IdFuncion, IdSector)
            VALUES (@Precio, @QR, @Usada, @Anulada, @FechaUso, @Numero, @IdDetalleOrden, @IdCliente, @IdFuncion, @IdSector);
            SELECT LAST_INSERT_ID();";

        entrada.IdEntrada = connection.ExecuteScalar<int>(sql, entrada);
    }
    public void Update(Entrada entrada)
    {
        using var connection = Connection;

        var sql = @"
            UPDATE Entrada
            SET Precio = @Precio,
                QR = @QR,
                Usada = @Usada,
                Anulada = @Anulada,
                FechaUso = @FechaUso
                Numero = @Numero,
                IdDetalleOrden = @IdDetalleOrden,
                IdSector = @IdSector,
                IdFuncion = @IdFuncion
            WHERE IdEntrada = @IdEntrada";

        connection.Execute(sql, entrada);
    }
    public void Anular(int idEntrada)
    {
        using var connection = Connection;

        var sqlCheck = "SELECT Anulada FROM Entrada WHERE IdEntrada = @idEntrada";
        var anulada = connection.QueryFirstOrDefault<bool?>(sqlCheck, new { idEntrada });

        if (anulada == null)
            throw new Exception("La entrada no existe");

        if (anulada.Value)
            throw new Exception("La entrada ya está anulada");

        var sqlUpdate = "UPDATE Entrada SET Anulada = 1 WHERE IdEntrada = @idEntrada";
        connection.Execute(sqlUpdate, new { idEntrada });
    }
    public Entrada? GetByDetalleOrdenId(int idDetalleOrden)
    {
        using var db = Connection;
        string sql = "SELECT * FROM Entrada WHERE IdDetalleOrden = @idDetalleOrden;";
        return db.QueryFirstOrDefault<Entrada>(sql, new { idDetalleOrden });
    }

}
