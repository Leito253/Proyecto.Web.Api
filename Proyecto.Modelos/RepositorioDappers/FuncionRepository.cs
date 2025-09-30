using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Proyecto.Modelos.RepositorioDappers
{

    public class FuncionRepository : IFuncionRepository
    {
         private readonly string _connectionString;

        public FuncionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);
        public void Add(Funcion funcion)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Funcion () 
                        VALUES (@Descripcion, @FechaHora, @Entradas, @IdFuncion)";
            db.Execute(sql, funcion);

        }

        public IEnumerable<Funcion> GetAll()
        {
            using var db = Connection;
            return db.Query<Funcion>("SELECT * FROM Funcion ");
        }

        public Funcion? GetById(int IdFuncion)
        {
            using var db = Connection;
            return db.QueryFirstOrDefault<Funcion>("SELECT * FROM Funcion WHERE IdFuncion=@IdFuncion", new { Id = IdFuncion });
        }

        public void Update(Funcion funcion)
        {
            using var db = Connection;
            var sql = @"UPDATE Funcion SET IdFuncion=@IdFuncion, Descripcion=@Descripcion, FechaHora=@FechaHora, Entradas=@Entradas
                        WHERE IdFuncion=@IdFuncion";
            db.Execute(sql, funcion);
        }
        
        public void Delete(int IdFuncion)
        {
            using var db = Connection;
            db.Execute("DELETE FROM Funcion WHERE IdFuncion=@IdFuncion", new { Id = IdFuncion });
        }
    }
}
