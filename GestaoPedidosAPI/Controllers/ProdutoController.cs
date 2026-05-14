using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        /// <summary>
        /// Construtor da classe ProdutoController.
        /// </summary>
        /// <param name="produtoService">Serviço de produto injetado.</param>
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Cadastrar novo produto.
        /// </summary>
        /// <param name="produtoRequestDto">DTO contendo os dados do produto a ser inserido.</param>
        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody] ProdutoRequestDto produtoRequestDto)
        {
            return Ok(await _produtoService.Inserir(produtoRequestDto));
        }

        /// <summary>
        /// Pesquisar todos os produtos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> PesquisarTodos()
        {
            return Ok(await _produtoService.PesquisarTodos());
        }

        /// <summary>
        /// Pesquisar produto pelo Id.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> PesquisarDtoPorId(string id)
        {
            return Ok(await _produtoService.PesquisarDtoPorId(id));
        }

        /// <summary>
        /// Atualizar produto.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="produtoRequestDto">DTO contendo os dados do produto a ser atualizado.</param>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] ProdutoRequestDto produtoRequestDto)
        {
            await _produtoService.Atualizar(id, produtoRequestDto);
            return Ok();
        }

        /// <summary>
        /// Inativar produto.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Inativar(string id)
        {
            await _produtoService.Inativar(id);
            return Ok();
        }
    }
}
