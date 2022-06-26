using Dapper;
using Microsoft.Data.SqlClient;
using WebApiPruebas.Modelo;

namespace WebApiPruebas.Servicios
{
    public interface IRepositorioClientes
    {
        Task Crear(Cliente cliente);
    }

    public class RepositorioClientes : IRepositorioClientes
    {
        private readonly string connectionString;

        public RepositorioClientes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task Crear(Cliente cliente)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                                                    ($@"INSERT INTO Clientes (Nombre)
                                                     VALUES (@Nombre);
                                                     SELECT SCOPE_IDENTITY();",cliente);

            cliente.Id = id;
        }


    }
}
