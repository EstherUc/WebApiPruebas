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

        [HttpPost("Crear")]
        public async Task<ActionResult> Crear(Cliente cliente)
        {
            await repositorioClientes.Crear(cliente);
            return Ok();
        }

        //[HttpGet] //api/clientes
        //[HttpGet("/listado")] //listado
        [HttpGet("listado")] //api/clientes/listado
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarClientes()
        {
            return await repositorioClientes.ListarClientes();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> BucarPorId(int id)
        {
            var cliente = await repositorioClientes.BucarPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> BuscarPorNombre(string nombre)
        {
            var cliente = await repositorioClientes.BuscarPorNombre(nombre);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> ActualizarNombre(Cliente cliente, int id)
        {
            if(cliente.Id != id)
            {
                return BadRequest("El id del cliente no coincide con el id de la URL");
            }

            await repositorioClientes.ActualizarNombre(cliente,id); 
            return Ok();
        }    

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> BorrarCliente(int id)
        {
            var cliente = await repositorioClientes.BucarPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            await repositorioClientes.BorrarCliente(id);
            return Ok();
        }



    }
}
