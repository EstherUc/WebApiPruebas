using Dapper;
using Microsoft.Data.SqlClient;
using WebApiPruebas.Modelo;

namespace WebApiPruebas.Servicios
{
    public interface IRepositorioFacturas
    {
        Task<Factura> BucarPorId(int id);
        Task Crear(Factura factura);
        Task<Factura> FacturaPorId(int id);
        Task<List<Factura>> ListarFacturas();
    }

    public class RepositorioFacturas : IRepositorioFacturas
    {
        private readonly string connectionString;

        public RepositorioFacturas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task Crear(Factura factura)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync(@"INSERT INTO Facturas (Nom_Empresa, ClienteId)
                                                        VALUES (@nom_empresa,@clienteId);
                                                        SELECT SCOPE_IDENTITY();", new { factura.Nom_Empresa, factura.ClienteId });
        }

        public async Task<Factura> BucarPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Factura>(@"SELECT * 
                                                                        FROM Facturas
                                                                        WHERE Id = @Id;", new { id });
        }

        public async Task<List<Factura>> ListarFacturas()
        {
            using var connection = new SqlConnection(connectionString);
            return (List<Factura>)await connection.QueryAsync<Factura>(@"SELECT *
                                                                         FROM Facturas;");
        }

        public async Task<List<Factura>> FacturaPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return (List<Factura>)await connection.QueryAsync<Factura>(@"SELECT f.Id, f.Nom_Empresa, f.ClienteId, c.Nombre
                                                          FROM Facturas f, Clientes c
                                                          WHERE f.ClienteId = c.Id AND c.Id = @id;", new { id });
        }

    }
}
