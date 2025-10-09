using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;

namespace Proyecto.Web.Api.Repositorios
{
    public class TarifaRepository : ITarifaRepository
    {
        private readonly string _connectionString;

        public TarifaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public Tarifa Insert(Tarifa tarifa)
        {
            using var db = Connection;
            string sql = @"INSERT INTO Tarifa (idFuncion, Precio, Stock, Activa)
                           VALUES (@idFuncion, @Precio, @Stock, @Activa);
                           SELECT LAST_INSERT_ID();";
            int id = db.ExecuteScalar<int>(sql, tarifa);
            tarifa.idTarifa = id;
            return tarifa;
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
}
