using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        /// <summary>
        /// Construtor da classe ClienteController.
        /// </summary>
        /// <param name="clienteService">Serviço de cliente injetado.</param>
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Cadastrar novo cliente.
        /// </summary>
        /// <param name="clienteRequestDto">DTO contendo os dados do cliente a ser inserido.</param>
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] ClienteRequestDto clienteRequestDto)
        {
            return Ok(await _clienteService.Inserir(clienteRequestDto));
        }

        /// <summary>
        /// Pesquisar todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> PesquisarTodos()
        {
            return Ok(await _clienteService.PesquisarTodos());
        }

        /// <summary>
        /// Pesquisar cliente pelo Id.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> PesquisarDtoPorId(string id)
        {
            return Ok(await _clienteService.PesquisarDtoPorId(id));
        }

        /// <summary>
        /// Inativar cliente.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        [HttpPatch]
        [Route("{id}/status")]
        public async Task<IActionResult> Inativar(string id)
        {
            await _clienteService.Inativar(id);
            return Ok();
        }
    }
}
