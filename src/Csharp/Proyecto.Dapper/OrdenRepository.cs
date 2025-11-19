using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;

public class OrdenRepository : IOrdenRepository
{
    private readonly string _connectionString;

    public OrdenRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

    public void Add(Orden orden)
    {
        using var db = Connection;

        string sqlOrden = @"
            INSERT INTO Orden (IdCliente, FechaCreacion, Estado)
            VALUES (@IdCliente, @FechaCreacion, @Estado);
            SELECT LAST_INSERT_ID();
        ";

        orden.idOrden = db.ExecuteScalar<int>(sqlOrden, orden);

        foreach (var detalle in orden.Detalles)
        {
            string sqlDetalle = @"
                INSERT INTO DetalleOrden (IdOrden, IdFuncion, Cantidad, PrecioUnitario)
                VALUES (@IdOrden, @IdFuncion, @Cantidad, @PrecioUnitario);
            ";

            detalle.IdOrden = orden.idOrden;
            db.Execute(sqlDetalle, detalle);
        }
    }

    public IEnumerable<Orden> GetAll()
    {
        using var db = Connection;

        string sqlOrden = "SELECT * FROM Orden;";
        var ordenes = db.Query<Orden>(sqlOrden).ToList();

        foreach (var orden in ordenes)
        {
            string sqlDetalle = "SELECT * FROM DetalleOrden WHERE IdOrden = @IdOrden;";
            orden.Detalles = db.Query<DetalleOrden>(sqlDetalle, new { IdOrden = orden.idOrden }).ToList();
        }

        return ordenes;
    }

    public Orden? GetById(int idOrden)
    {
        using var db = Connection;

        string sqlOrden = "SELECT * FROM Orden WHERE IdOrden = @IdOrden;";
        var orden = db.QueryFirstOrDefault<Orden>(sqlOrden, new { IdOrden = idOrden });

        if (orden != null)
        {
            string sqlDetalle = "SELECT * FROM DetalleOrden WHERE IdOrden = @IdOrden;";
            orden.Detalles = db.Query<DetalleOrden>(sqlDetalle, new { IdOrden = idOrden }).ToList();
        }

        return orden;
    }

    public void Update(Orden orden)
    {
        using var db = Connection;

        string sql = @"
            UPDATE Orden
            SET IdCliente = @IdCliente,
                Estado = @Estado
            WHERE IdOrden = @IdOrden;
        ";

        db.Execute(sql, orden);
    }

    public void Pagar(int idOrden)
    {
        using var db = Connection;

        string sql = "UPDATE Orden SET Estado = 'Pagada' WHERE IdOrden = @IdOrden;";
        db.Execute(sql, new { IdOrden = idOrden });
    }
    public void Cancelar(int idOrden)
    {
        using var db = Connection;

        string sql = @"
            UPDATE Orden 
            SET Estado = 'Cancelada' 
            WHERE IdOrden = @IdOrden 
            AND Estado = 'Creada';
        ";

        db.Execute(sql, new { IdOrden = idOrden });
    }

    public Orden? GetByIdWithDetalles(int idOrden)
    {
        using var db = Connection;

        string sqlOrden = "SELECT * FROM Orden WHERE IdOrden = @IdOrden;";
        var orden = db.QueryFirstOrDefault<Orden>(sqlOrden, new { IdOrden = idOrden });

        if (orden != null)
        {
            string sqlDetalle = "SELECT * FROM DetalleOrden WHERE IdOrden = @IdOrden;";
            orden.Detalles = db.Query<DetalleOrden>(sqlDetalle, new { IdOrden = idOrden }).ToList();
        }

        return orden;
    }

    public bool EstaPagada(int idOrden)
    {
        using var db = Connection;

        string sql = "SELECT Estado FROM Orden WHERE IdOrden = @IdOrden;";
        var estado = db.QueryFirstOrDefault<string>(sql, new { IdOrden = idOrden });

        return string.Equals(estado, "Pagada", StringComparison.OrdinalIgnoreCase);
    }

    public string PagarOrden(int ordenId)
    {
        var orden = GetById(ordenId);
        if (orden == null) return "NotFound";
        if (orden.Estado != "Creada") return "BadRequest";
        orden.Estado = "Pagada";
        Update(orden);
        return "Ok";
    }

    public string CancelarOrden(int ordenId)
    {
        var orden = GetById(ordenId);
        if (orden == null) return "NotFound";
        if (orden.Estado != "Creada") return "BadRequest";
        orden.Estado = "Cancelada";
        Update(orden);
        return "Ok";
    }
}
