using System.Data;

namespace Proyecto.Modelos.Repositorios.ReposDapper;
public class OrdenRepository : IOrdenRepository
{
    private readonly string _connectionString;

    public OrdenRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

    // Crear orden (INSERT)
    public void Add(Orden orden)
    {
        using var db = Connection;
        string sql = @"
                INSERT INTO Orden (IdUsuario, FechaCreacion, Estado)
                VALUES (@IdUsuario, @FechaCreacion, @Estado);
                SELECT LAST_INSERT_ID();";

        var id = db.ExecuteScalar<int>(sql, orden);
        orden.idOrden = id;

        // Insertar detalles
        foreach (var detalle in orden.Detalles)
        {
            string sqlDetalle = @"
                    INSERT INTO DetalleOrden (IdOrden, IdEvento, Cantidad, PrecioUnitario)
                    VALUES (@IdOrden, @IdEvento, @Cantidad, @PrecioUnitario);";
            detalle.IdOrden = id;
            db.Execute(sqlDetalle, detalle);
        }
    }

    // Listar todas las órdenes
    public IEnumerable<Orden> GetAll()
    {
        using var db = Connection;
        string sql = "SELECT * FROM Orden;";
        var ordenes = db.Query<Orden>(sql).ToList();

        foreach (var orden in ordenes)
        {
            string sqlDetalle = "SELECT * FROM DetalleOrden WHERE IdOrden = @IdOrden;";
            orden.Detalles = db.Query<DetalleOrden>(sqlDetalle, new { IdOrden = orden.idOrden }).ToList();
        }

        return ordenes;
    }

    // Buscar por ID
    public Orden? GetById(int idOrden)
    {
        using var db = Connection;
        string sql = "SELECT * FROM Orden WHERE IdOrden = @IdOrden;";
        var orden = db.QueryFirstOrDefault<Orden>(sql, new { IdOrden = idOrden });

        if (orden != null)
        {
            string sqlDetalle = "SELECT * FROM DetalleOrden WHERE IdOrden = @IdOrden;";
            orden.Detalles = db.Query<DetalleOrden>(sqlDetalle, new { IdOrden = idOrden }).ToList();
        }

        return orden;
    }

    // Actualizar orden (ej: cambiar estado o usuario)
    public void Update(Orden orden)
    {
        using var db = Connection;
        string sql = @"
                UPDATE Orden 
                SET IdUsuario = @IdUsuario,
                    Estado = @Estado
                WHERE IdOrden = @IdOrden;";
        db.Execute(sql, orden);
    }

    // Marcar como Pagada
    public void Pagar(int idOrden)
    {
        using var db = Connection;
        string sql = "UPDATE Orden SET Estado = 'Pagada' WHERE IdOrden = @IdOrden;";
        db.Execute(sql, new { IdOrden = idOrden });
    }

    // Cancelar orden
    public void Cancelar(int idOrden)
    {
        using var db = Connection;
        string sql = "UPDATE Orden SET Estado = 'Cancelada' WHERE IdOrden = @IdOrden AND Estado = 'Creada';";
        db.Execute(sql, new { IdOrden = idOrden });
    }
}