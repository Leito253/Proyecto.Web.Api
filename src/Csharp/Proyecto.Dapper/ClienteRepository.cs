using System.Data;
using Proyecto.Core.Entidades;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;



namespace Proyecto.Core.Repositorios.ReposDapper;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnection");
    }
    private IDbConnection Connection => new MySqlConnection(_connectionString);


    public IEnumerable<Cliente> GetAll()
    {
        using var db = Connection;
        var clientes = db.Query<Cliente>("SELECT * FROM Cliente").ToList();
        return clientes;
    }

    Cliente? IClienteRepository.GetById(int DNI)
    {
        using var db = Connection;
        return db.QueryFirstOrDefault<Cliente>("SELECT * FROM Cliente WHERE DNI=@DNI", new { dni = DNI });
    }


    public void Add(Cliente cliente)
    {
        using var connection = new MySqlConnection(_connectionString);
        string sql = @"INSERT INTO Cliente (Dni, Nombre, Apellido, Email, Telefono)
                    VALUES (@Dni, @Nombre, @Apellido, @Email, @Telefono);
                    SELECT LAST_INSERT_ID();";

        var id = connection.ExecuteScalar<int>(sql, cliente);
        cliente.idCliente = id;
    }
    public void Update(Cliente cliente)
{
    using var db = Connection;
    var sql = @"UPDATE Cliente 
                SET Dni = @Dni, 
                    Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    Email = @Email, 
                    Telefono = @Telefono
                WHERE idCliente = @idCliente;";
    db.Execute(sql, cliente);
}

public void Delete(int idCliente)
{
    using var db = Connection;
    db.Execute("DELETE FROM Cliente WHERE idCliente = @idCliente;", new { idCliente });
}

}