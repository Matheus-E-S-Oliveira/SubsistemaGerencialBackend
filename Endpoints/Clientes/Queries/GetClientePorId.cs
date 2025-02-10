using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.EnderecoClientes;
using SubsistemaGerencialBackend.Models.Fazendas;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace SubsistemaGerencialBackend.Endpoints.Clientes.Queries
{
    [ApiController]
    [Route("api/cliente/{id}")]
    public class GetClientePorId : ControllerBase
    {
        private readonly AppDbContext _context;

        public GetClientePorId(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<ClienteComEnderecoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<ClienteComEnderecoDto>>> GetPaged([FromRoute] Guid id,
                                                                          [FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10)
        {
            var query = _context.Clientes
                .Where(c => c.Id == id) // Filtrar pelo ID do cliente
                .Select(cliente => new ClienteComEnderecoDto
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Cpf = cliente.Cpf,
                    Situacao = cliente.Situacao,
                    Email = cliente.Email,
                    Telefone = cliente.Telefone,

                    // Mapeamento de Endereços diretamente no Select
                    Enderecos = cliente.EnderecosCliente!
                        .Select(e => new EnderecoClienteDto
                        {
                            EnderecoId = e.Id,
                            Uf = e.Uf, 
                            Cidade = e.Cidade,
                            Cep = e.Cep,
                            Rua = e.Rua,
                            Bairro = e.Bairro,
                            Numero = e.Numero,
                            Complemento = e.Complemento
                        })
                        .ToList(),

                    // Mapeamento de Fazendas diretamente no Select
                    Fazendas = cliente.Fazendas!
                        .Select(f => new FazendaDto
                        {
                            Id = f.Id,
                            Nome = f.Nome,
                            CodigoFazenda = f.CodigoFazenda,
                            DataCriacaoFazenda = f.DataCriacaoFazenda,
                            QuantidadeAnimais = f.QuantidadeAnimais
                        })
                        .OrderBy(x => x.DataCriacaoFazenda)
                        .ToList()
                });


            var pagedResult = await Pagedresult<ClienteComEnderecoDto>.ToPagedResultAsync(query, pageNumber, pageSize);

            return Ok(pagedResult);
        }
    }
}
