using Microsoft.AspNetCore.Mvc;
using WebApiPruebas.Modelo;
using WebApiPruebas.Servicios;

namespace WebApiPruebas.Controllers
{
    [ApiController]
    [Route("api/Facturas")]
    public class FacturaController : ControllerBase
    {
        private readonly IRepositorioFacturas repositorioFacturas;
        private readonly IRepositorioClientes repositorioClientes;

        public FacturaController(IRepositorioFacturas repositorioFacturas, IRepositorioClientes repositorioClientes)
        {
            this.repositorioFacturas = repositorioFacturas;
            this.repositorioClientes = repositorioClientes;
        }

        [HttpGet("Listado")]
        public async Task<ActionResult<IEnumerable<Factura>>> ListarFacturas()
        {
            return await repositorioFacturas.ListarFacturas(); 
        }

       /*[HttpGet("id:int")]
        public async Task<ActionResult<IEnumerable<Factura>>> FacturaPorId(int id)
        {
            var factura = await repositorioFacturas.BucarPorId(id);

            if(factura == null)
            {
                return NotFound();
            }

            return await repositorioFacturas.FacturaPorId(id);
        }*/

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(Factura factura)
        {
            var cliente = await repositorioClientes.BucarPorId(factura.ClienteId);

            if (cliente == null)
            {
                return NotFound();
            }

            await repositorioFacturas.Crear(factura);
            return Ok();
        }
    }
}
