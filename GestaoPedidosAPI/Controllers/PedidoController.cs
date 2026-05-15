using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        /// <summary>
        /// Construtor da classe PedidoController.
        /// </summary>
        /// <param name="pedidoService">Serviço de pedido injetado.</param>
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Cadastrar novo pedido.
        /// </summary>
        /// <param name="pedidoRequestDto">DTO contendo os dados do pedido a ser inserido.</param>
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] PedidoRequestDto pedidoRequestDto)
        {
            return Ok(await _pedidoService.Inserir(pedidoRequestDto));
        }

        /// <summary>
        /// Pesquisar todos os pedidos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> PesquisarTodos()
        {
            return Ok(await _pedidoService.PesquisarTodos());
        }

        /// <summary>
        /// Pesquisar pedido pelo Id.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            return Ok(await _pedidoService.PesquisarPorId(id));
        }

        /// <summary>
        /// Alterar status do pedido.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <param name="pedidoStatusRequestDto">Dto contando novo status e motivo da alteração.</param>
        [HttpPatch]
        [Route("{id}/status")]
        public async Task<IActionResult> AlterarStatus(string id, [FromBody] PedidoStatusRequestDto pedidoStatusRequestDto)
        {
            await _pedidoService.AlterarStatus(id, pedidoStatusRequestDto);
            return Ok();
        }
    }
}
