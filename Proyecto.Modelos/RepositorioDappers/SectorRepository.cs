using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Repositorios;
using Proyecto.Modelos.Entidades;
using System.Data;
using Dapper;

namespace Proyecto.Modelos.RepositorioDappers;

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

    public object? GetByLocal(int idLocal)
    {
        throw new NotImplementedException();
    }

    public void Add(int idLocal, Sector sector)
    {
        throw new NotImplementedException();
    }
}