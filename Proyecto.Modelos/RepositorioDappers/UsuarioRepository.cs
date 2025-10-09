using Dapper;
using MySql.Data.MySqlClient;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;
using System.Data;

namespace Proyecto.Modelos.RepositorioDappers;

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

    public Usuario? GetByUsername(string usuario)
    {
        using var db = Connection;
        return db.QueryFirstOrDefault<Usuario>(
            "SELECT * FROM Usuario WHERE Usuario = @usuario", new { Usuario = usuario });
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

    public Usuario? GetByUsuario(string usuario)
    {
        throw new NotImplementedException();
    }
}
