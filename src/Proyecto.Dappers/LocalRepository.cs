using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios.ReposDapper;

public class LocalRepository : ILocalRepository
{
    private readonly string _connectionString;
    private string connStr;

    public LocalRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    public LocalRepository(string connStr)
    {
        this.connStr = connStr;
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

    public IEnumerable<Local> GetAll()
    {
        using var connection = new MySqlConnection(_connectionString);
        return connection.Query<Local>("SELECT * FROM Local");
    }

    public Local? GetById(int idLocal)
    {
        using var connection = new MySqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Local>(
            "SELECT * FROM Local WHERE idLocal = @id", new { idLocal });
    }

    public int Add(Local local)
    {
        using var connection = new MySqlConnection(_connectionString);
        var sql = @"
                INSERT INTO Local (Nombre, Direccion, Capacidad)
                VALUES (@Nombre, @Direccion, @Capacidad);
                SELECT LAST_INSERT_ID();";
        return connection.ExecuteScalar<int>(sql, local);
    }

    public void Update(Local local)
    {
        using var connection = new MySqlConnection(_connectionString);
        var sql = @"UPDATE Local 
                        SET Nombre = @Nombre, Direccion = @Direccion, Capacidad = @Capacidad 
                        WHERE idLocal = @idLocal";
        connection.Execute(sql, local);
    }

    public bool Delete(int idLocal)
    {
        using var connection = new MySqlConnection(_connectionString);
        var sql = "DELETE FROM Local WHERE idLocal = @id";
        return connection.Execute(sql, new { idLocal }) > 0;
    }

    public bool TieneFuncionesVigentes(int idLocal)
    {
        using var connection = new MySqlConnection(_connectionString);
        var sql = @"SELECT COUNT(*) 
                        FROM Funcion 
                        WHERE idLocal = @id AND Estado = 'Activa'";
        return connection.ExecuteScalar<int>(sql, new { idLocal }) > 0;
    }

}
