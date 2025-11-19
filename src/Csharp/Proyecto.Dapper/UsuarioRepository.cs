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

        const string sql = @"
            INSERT INTO Usuario (NombreUsuario, Email, Contrasena, Activo)
            VALUES (@NombreUsuario, @Email, @Contrasena, @Activo);
            SELECT LAST_INSERT_ID();
        ";

        usuario.IdUsuario = db.ExecuteScalar<int>(sql, usuario);
    }

    public Usuario? Login(string nombreUsuario, string contrasena)
    {
        using var db = Connection;

        const string sql = @"
            SELECT * FROM Usuario 
            WHERE NombreUsuario = @nombreUsuario 
                AND Contrasena = @contrasena
                AND Activo = 1
        ";

        return db.QueryFirstOrDefault<Usuario>(sql, new { nombreUsuario, contrasena });
    }

    public Usuario? GetById(int idUsuario)
    {
        using var db = Connection;

        const string sql = @"SELECT * FROM Usuario WHERE IdUsuario = @idUsuario";

        return db.QueryFirstOrDefault<Usuario>(sql, new { idUsuario });
    }

    public Usuario? GetByNombreUsuario(string nombreUsuario)
    {
        using var db = Connection;

        const string sql = @"SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario";

        return db.QueryFirstOrDefault<Usuario>(sql, new { nombreUsuario });
    }

    public IEnumerable<Rol> GetRoles(int idUsuario)
    {
        using var db = Connection;

        const string sql = @"
            SELECT r.* 
            FROM Rol r
            INNER JOIN UsuarioRol ur ON r.IdRol = ur.IdRol
            WHERE ur.IdUsuario = @idUsuario
        ";

        return db.Query<Rol>(sql, new { idUsuario });
    }
    public IEnumerable<Rol> GetAllRoles()
    {
        using var db = Connection;
        return db.Query<Rol>("SELECT * FROM Rol");
    }
    public void AsignarRol(int idUsuario, int idRol)
    {
        using var db = Connection;

        const string sql = @"
            INSERT INTO UsuarioRol (IdUsuario, IdRol)
            VALUES (@idUsuario, @idRol)
            ON DUPLICATE KEY UPDATE IdRol = @idRol;
        ";

        db.Execute(sql, new { idUsuario, idRol });
    }
}