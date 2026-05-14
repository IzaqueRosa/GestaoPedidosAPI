using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Exceptions;
using GestaoPedidos.Domain.Helpers;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;

namespace GestaoPedidos.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Inativar(string id)
        {
            var cliente = await _clienteRepository.PesquisarClientePorId(int.Parse(id));

            if (cliente == null)
                throw new BusinessException("Cliente não encontrado.");

            if (!cliente.Ativo)
                throw new BusinessException("Cliente já está inativo.");

            cliente.Ativo = false;
            cliente.DataAtualizacao = DateTime.UtcNow;

            await _clienteRepository.Atualizar();
        }

        public async Task<ClienteResponseDto> Inserir(ClienteRequestDto clienteRequestDto)
        {
            if (await _clienteRepository.ExisteEmailAtivo(clienteRequestDto.Email))
                throw new BusinessException("E-mail já cadastrado.");

            if (await _clienteRepository.ExisteDocumentoAtivo(clienteRequestDto.Documento))
                throw new BusinessException("Documento já cadastrado.");

            DocumentoValidator.ValidarDocumento(clienteRequestDto);

            var cliente = new Cliente
            {
                Nome = clienteRequestDto.Nome,
                Email = clienteRequestDto.Email.Trim().ToLower(),
                Documento = clienteRequestDto.Documento,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            await _clienteRepository.Inserir(cliente);

            return new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Documento = cliente.Documento,
                Ativo= cliente.Ativo,
                DataCriacao= DateTime.UtcNow
            };
        }

        public async Task<ClienteResponseDto?> PesquisarDtoPorId(string id)
        {
            var clienteResponseDto = await _clienteRepository.PesquisarDtoPorId(int.Parse(id));

            if (clienteResponseDto == null)
                throw new BusinessException("Cliente não encontrado.");

            return clienteResponseDto;
        }

        public async Task<List<ClienteResponseDto>> PesquisarTodos()
        {
            return await _clienteRepository.PesquisarTodos();
        }
    }
}
