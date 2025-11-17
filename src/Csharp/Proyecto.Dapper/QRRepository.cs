using Dapper;
using Proyecto.Core.Entidades;
using Proyecto.Core.Repositorios;
using System.Data;

namespace Proyecto.Dapper
{
    public class QRRepository : IQRRepository
    {
        private readonly IDbConnection _db;

        public QRRepository(IDbConnection db)
        {
            _db = db;
        }

        public void Add(QR qr)
        {
            string sql = @"INSERT INTO QR (IdEntrada, Codigo, FechaCreacion)
                           VALUES (@IdEntrada, @Codigo, @FechaCreacion);
                           SELECT LAST_INSERT_ID();";

            qr.idQR = _db.ExecuteScalar<int>(sql, qr);
        }

        public QR GetByEntrada(int idEntrada)
        {
            string sql = "SELECT * FROM QR WHERE IdEntrada = @idEntrada";
            return _db.QueryFirstOrDefault<QR>(sql, new { idEntrada });
        }
    }
}
