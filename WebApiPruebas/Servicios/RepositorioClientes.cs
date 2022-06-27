using Dapper;
using Microsoft.Data.SqlClient;
using WebApiPruebas.Modelo;

namespace WebApiPruebas.Servicios
{
    public interface IRepositorioClientes
    {
        Task ActualizarNombre(Cliente cliente, int id);
        Task BorrarCliente(int id);
        Task<Cliente> BucarPorId(int id);
        Task<List<Cliente>> BuscarPorNombre(string nombre);
        Task Crear(Cliente cliente);
        Task<List<Cliente>> ListarClientes();
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

        public async Task<List<Cliente>> ListarClientes()
        {
            using var connection = new SqlConnection(connectionString);
            return (List<Cliente>)await connection.QueryAsync<Cliente>(@"SELECT *
                                                        FROM Clientes;");
        }

        public async Task<Cliente> BucarPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Cliente>(@"SELECT * 
                                                        FROM Clientes
                                                        WHERE Id = @Id;", new { id });
        }

       public async Task<List<Cliente>> BuscarPorNombre(string nombre)
        {
            using var connection = new SqlConnection(connectionString);
            return (List<Cliente>) await connection.QueryAsync<Cliente>(@"SELECT * 
                                                                        FROM Clientes
                                                                        WHERE LOWER(Nombre) LIKE LOWER('%@nombre%');", new { nombre });
        }

        public async Task ActualizarNombre(Cliente cliente, int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Clientes
                                            SET Nombre = @nombre
                                            WHERE Id = @Id;", new { cliente.Nombre, id });
        }

        public async Task BorrarCliente(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE Clientes
                                            WHERE Id = @id;", new { id });
        }


    }
}
