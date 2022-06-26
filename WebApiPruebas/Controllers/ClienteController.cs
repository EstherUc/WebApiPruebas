using Microsoft.AspNetCore.Mvc;
using WebApiPruebas.Modelo;
using WebApiPruebas.Servicios;

namespace WebApiPruebas.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IRepositorioClientes repositorioClientes;

        public ClienteController(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes; 
        }

        [HttpPost]
        public async Task<ActionResult> Crear(Cliente cliente)
        {
            await repositorioClientes.Crear(cliente);
            return Ok();
        }

    }
}
