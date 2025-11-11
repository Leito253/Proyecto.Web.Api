using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios.ReposDapper;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

    public void Add(Usuario usuario)
    {
        using var db = Connection;
        string sql = @"INSERT INTO Usuario (Usuario, Email, Password, Activo)
                       VALUES (@Usuario, @Email, @Password, @Activo)";
       int filas = db.Execute(sql, usuario );
    }

    public Usuario? Login(string Correo, string Contrasena)
        {
            using var db = Connection;
            const string sql = "SELECT * FROM Usuario WHERE correo = @correo AND contrasena = @contrasena;";
            return db.QueryFirstOrDefault<Usuario>(sql, new { Correo, Contrasena });
        }

         public Usuario? GetById(int IdUsuario)
    {
        using var db = Connection;
        return db.QueryFirstOrDefault<Usuario>(
            "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario", new { Id = IdUsuario });
    }

    public IEnumerable<Rol> GetRoles(int IdUsuario)
    {
        using var db = Connection;
        string sql = @"SELECT r.* FROM Rol r
                       INNER JOIN UsuarioRol ur ON r.IdRol = ur.IdRol
                       WHERE ur.IdUsuario = @IdUsuario";
        return db.Query<Rol>(sql, new { Id = IdUsuario });
    }

    public IEnumerable<Rol> GetAllRoles()
    {
        using var db = Connection;
        return db.Query<Rol>("SELECT * FROM Rol");
    }

    public void AsignarRoles(int IdUsuario, int idRol)
    {
        using var db = Connection;
        string sql = @"INSERT INTO UsuarioRol (IdUsuario, IdRol)
                       VALUES (@IdUsuario, @IdRol)
                       ON DUPLICATE KEY UPDATE IdRol = @IdRol";
        db.Execute(sql, new { Id = IdUsuario, IdRol = idRol });
    }

    public object Login(object nombreUsuario, string contrasena)
    {
        throw new NotImplementedException();
    }
}
