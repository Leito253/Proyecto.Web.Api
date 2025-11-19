using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;

public class TarifaRepository : ITarifaRepository
{
    private readonly string _connectionString;
    private readonly string _connStr;

    public TarifaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    public TarifaRepository(string connStr)
    {
        _connStr = connStr;
    }

    private IDbConnection Connection =>
        new MySqlConnection(_connectionString ?? _connStr);

    public int Add(Tarifa tarifa)
    {
        using var connection = Connection;

        var sql = @"
            INSERT INTO Tarifa (Nombre, Precio, idSector)
            VALUES (@Nombre, @Precio, @idSector);
            SELECT LAST_INSERT_ID();";

        return connection.ExecuteScalar<int>(sql, tarifa);
    }

    void ITarifaRepository.Add(Tarifa tarifa)
    {
        Add(tarifa);
    }

    public IEnumerable<Tarifa> GetByFuncionId(int idFuncion)
    {
        using var connection = Connection;

        var sql = @"
            SELECT t.* 
            FROM Tarifa t
            INNER JOIN Funcion f ON f.IdSector = t.idSector
            WHERE f.IdFuncion = @idFuncion";

        return connection.Query<Tarifa>(sql, new { idFuncion });
    }

    public Tarifa? GetById(int idTarifa)
    {
        using var connection = Connection;

        var sql = "SELECT * FROM Tarifa WHERE idTarifa = @idTarifa";
        return connection.QueryFirstOrDefault<Tarifa>(sql, new { idTarifa });
    }

    public void Update(Tarifa tarifa)
    {
        using var connection = Connection;

        var sql = @"
            UPDATE Tarifa
            SET Nombre = @Nombre,
                Precio = @Precio,
                idSector = @idSector
            WHERE idTarifa = @idTarifa";

        connection.Execute(sql, tarifa);
    }

    public bool TieneTarifasDeSector(int idSector)
    {
        using var connection = Connection;

        var sql = "SELECT COUNT(*) FROM Tarifa WHERE idSector = @idSector";
        return connection.ExecuteScalar<int>(sql, new { idSector }) > 0;
    }
}
