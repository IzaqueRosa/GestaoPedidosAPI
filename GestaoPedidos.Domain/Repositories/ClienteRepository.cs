using GestaoPedidos.Data.DataBase.GestaoPedidos.Data.DataBase;
using GestaoPedidos.Data.Models;
using GestaoPedidos.Data.Models.Dtos;
using GestaoPedidos.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Domain.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly GestaoPedidosContext _context;

        public ClienteRepository(GestaoPedidosContext context)
        {
            _context = context;
        }
        public async Task<bool> ExisteEmailAtivo(string email)
        {
            return await _context.Cliente
                .AnyAsync(x => x.Email == email && x.Ativo);
        }

        public async Task<bool> ExisteDocumentoAtivo(string documento)
        {
            return await _context.Cliente
                .AnyAsync(x => x.Documento == documento && x.Ativo);
        }

        public async Task<Cliente> Inserir(Cliente cliente)
        {
            _context.Cliente.Add(cliente);

            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<List<ClienteResponseDto>> PesquisarTodos()
        {
            return await _context.Cliente
                .AsNoTracking()
                .Select(s => new ClienteResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Email = s.Email,
                    Documento = s.Documento,
                    Ativo = s.Ativo,
                    DataCriacao = s.DataCriacao,
                    DataAtualizacao = s.DataAtualizacao
                }).ToListAsync();
        }

        public async Task<ClienteResponseDto?> PesquisarDtoPorId(int clienteId)
        {
            return await _context.Cliente
                .AsNoTracking()
                .Where(w => w.Id == clienteId)
                .Select(s => new ClienteResponseDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Email = s.Email,
                    Documento = s.Documento,
                    Ativo = s.Ativo,
                    DataCriacao = s.DataCriacao,
                    DataAtualizacao = s.DataAtualizacao
                }).FirstOrDefaultAsync();
        }

        public async Task<Cliente?> PesquisarClientePorId(int clienteId)
        {
            return await _context.Cliente
                .FirstOrDefaultAsync(x => x.Id == clienteId);
        }

        public async Task Atualizar()
        {
            await _context.SaveChangesAsync();
        }
    }
}
