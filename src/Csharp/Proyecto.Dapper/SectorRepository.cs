using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;
public class SectorRepository : ISectorRepository
{
    private readonly string _connectionString;

    public SectorRepository(IConfiguration configuration)
    {   
            _connectionString = configuration.GetConnectionString("MySqlConnection");
    }
    private IDbConnection Connection => new MySqlConnection(_connectionString);

     public void Add(Sector sector)
    {
        using var db = Connection;
        string sql = "INSERT INTO Sectores (Nombre, idLocal ) VALUES (@Nombre, @idLocal );";
        db.Execute(sql, new { sector.Nombre, sector.idLocal  });
    }


    public IEnumerable<Sector> GetAll()
    {
        using var db = Connection;
        string sql = "SELECT * FROM Sectores;";
        return db.Query<Sector>(sql);
    }


    public Sector? GetById(int idSector)
    {
        using var db = Connection;
        string sql = "SELECT * FROM Sectores WHERE Id = @Id;";
        return db.QueryFirstOrDefault<Sector>(sql, new { Id = idSector });
    }


    public void Update(Sector sector)
    {
        using var db = Connection;
        string sql = "UPDATE Sectores SET Nombre = @Nombre WHERE Id = @Id;";
        db.Execute(sql, new { sector.Nombre, Id = sector.idSector });
    }


    public void Delete(int idSector)
    {
        using var db = Connection;
        string sql = "DELETE FROM Sectores WHERE Id = @Id;";
        db.Execute(sql, new { Id = idSector });
    }

    public void Add(int idLocal, Sector sector)
    {
               using var db = Connection;
        string sql = "INSERT INTO Sectores (Nombre, idLocal ) VALUES (@Nombre, @idLocal );";
        db.Execute(sql, new { sector.Nombre, sector.idLocal  });
    }
   
}