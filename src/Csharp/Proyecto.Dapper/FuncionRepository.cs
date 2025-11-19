using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;

public class FuncionRepository : IFuncionRepository
{
    private readonly string _connectionString;
    private readonly string _connStr;

    public FuncionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    public FuncionRepository(string connStr)
    {
        _connStr = connStr;
    }

    private IDbConnection Connection =>
        new MySqlConnection(_connectionString ?? _connStr);

    public IEnumerable<Funcion> GetAll()
    {
        using var connection = Connection;
        return connection.Query<Funcion>("SELECT * FROM Funcion");
    }

    public Funcion? GetById(int id)
    {
        using var connection = Connection;
        return connection.QueryFirstOrDefault<Funcion>(
            "SELECT * FROM Funcion WHERE idFuncion = @id", new { id });
    }

    public void Add(Funcion funcion)
    {
        using var connection = Connection;

        var sql = @"
                INSERT INTO Funcion (Fecha, Horario, idLocal, idEvento, Estado)
                VALUES (@Fecha, @Horario, @idLocal, @idEvento, @Estado);";

connection.Execute(sql, funcion);

    }

    public void Update(Funcion funcion)
    {
        using var connection = Connection;

        var sql = @"UPDATE Funcion
                    SET Fecha = @Fecha, Horario = @Horario, 
                        idLocal = @idLocal, idEvento = @idEvento, Estado = @Estado
                    WHERE idFuncion = @idFuncion";

        connection.Execute(sql, funcion);
    }

    public void Delete(int id)
    {
        using var connection = Connection;

        var sql = "DELETE FROM Funcion WHERE idFuncion = @id";

        connection.Execute(sql, new { id });
    }

    public void Cancelar(int idFuncion)
    {
        using var connection = Connection;

        var sql = @"UPDATE Funcion SET Estado = 'Cancelada' 
                    WHERE idFuncion = @idFuncion";

        connection.Execute(sql, new { idFuncion });
    }

    public bool TieneFuncionesDeSector(int idSector)
    {
        using var connection = Connection;

        var sql = @"SELECT COUNT(*) 
                    FROM Funcion
                    WHERE idSector = @idSector";

        return connection.ExecuteScalar<int>(sql, new { idSector }) > 0;
    }
}
