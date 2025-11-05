using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Interfaces;
using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios.ReposDapper;
public class TarifaRepository : ITarifaRepository
{
    private readonly string _connectionString;

    public TarifaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

     public void Add(Tarifa tarifa)
        {
            using var db = Connection;
            const string sql = @"INSERT INTO Tarifa (IdFuncion, Precio, Stock, Activa)
                                 VALUES (@IdFuncion, @Precio, @Stock, @Activa);";
            db.Execute(sql, tarifa);
        }

    public IEnumerable<Tarifa> GetByFuncionId(int idFuncion)
    {
        using var db = Connection;
        string sql = "SELECT * FROM Tarifa WHERE idFuncion = @funcionId;";
        return db.Query<Tarifa>(sql, new { idFuncion });
    }

    public Tarifa GetById(int idTarifa)
    {
        using var db = Connection;
        string sql = "SELECT * FROM Tarifa WHERE idTarifa = @tarifaId;";
        return db.QueryFirstOrDefault<Tarifa>(sql, new { idTarifa });
    }

    public void Update(Tarifa tarifa)
    {
        using var db = Connection;
        string sql = @"UPDATE Tarifa
                           SET Precio = @Precio,
                               Stock = @Stock,
                               Activa = @Activa
                           WHERE idTarifa = @idTarifa;";
        db.Execute(sql, tarifa);
    }

    
}
