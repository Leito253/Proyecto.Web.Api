using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;

public class SectorRepository : ISectorRepository
{
    private readonly string _connectionString;
    private readonly string _connStr;

    public SectorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    public SectorRepository(string connStr)
    {
        _connStr = connStr;
    }

    private IDbConnection Connection =>
        new MySqlConnection(_connectionString ?? _connStr);

    public IEnumerable<Sector> GetAll()
    {
        using var connection = Connection;
        return connection.Query<Sector>("SELECT * FROM Sector");
    }

    public Sector? GetById(int idSector)
    {
        using var connection = Connection;

        return connection.QueryFirstOrDefault<Sector>(
            "SELECT * FROM Sector WHERE idSector = @idSector",
            new { idSector }
        );
    }

    public IEnumerable<Sector> GetByLocal(int idLocal)
    {
        using var connection = Connection;

        return connection.Query<Sector>(
            "SELECT * FROM Sector WHERE idLocal = @idLocal",
            new { idLocal }
        );
    }

    public int Add(Sector sector)
    {
        using var connection = Connection;

        var sql = @"
            INSERT INTO Sector (Nombre, idLocal, Capacidad, Precio)
            VALUES (@Nombre, @idLocal, @Capacidad, @Precio);
            SELECT LAST_INSERT_ID();";

        return connection.ExecuteScalar<int>(sql, sector);
    }

    public void Update(Sector sector)
    {
        using var connection = Connection;

        var sql = @"
            UPDATE Sector
            SET Nombre = @Nombre,
                idLocal = @idLocal,
                Capacidad = @Capacidad,
                Precio = @Precio
            WHERE idSector = @idSector";

        connection.Execute(sql, sector);
    }

    public void Delete(int idSector)
    {
        using var connection = Connection;

        var sql = "DELETE FROM Sector WHERE idSector = @idSector";

        connection.Execute(sql, new { idSector });
    }
}
