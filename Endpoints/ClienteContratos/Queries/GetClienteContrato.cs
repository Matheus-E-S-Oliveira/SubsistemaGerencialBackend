using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Enums.SituacaoContratos;
using SubsistemaGerencialBackend.Models.ClienteContratos;
using SubsistemaGerencialBackend.Models.Fazendas;

namespace SubsistemaGerencialBackend.Endpoints.ContratoClientes.Queries
{
    [ApiController]
    [Route("api/clienteContrato")]
    public class GetClienteContrato : ControllerBase
    {
        private readonly AppDbContext _context;

        public GetClienteContrato(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<ClienteContratoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<ClienteContratoDto>>> GetPaged([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nomeCliente = null,
                                                                          [FromQuery] string? nomeFazenda = null,
                                                                          [FromQuery] int? situacao = null)
        {


            var query = from cc in _context.ClienteContratos
                        join c in _context.Clientes on cc.ClienteId equals c.Id
                        join f in _context.Fazendas on cc.FazendaId equals f.Id
                        where (string.IsNullOrWhiteSpace(nomeCliente) || (c.Nome != null && c.Nome.ToUpper().Contains(nomeCliente.ToUpper()))) &&
                              (string.IsNullOrWhiteSpace(nomeFazenda) || (f.Nome != null && f.Nome.ToUpper().Contains(nomeFazenda.ToUpper()))) &&
                              (!situacao.HasValue || cc.Situacao == (SituacaoContrato)situacao)
                        select (new ClienteContratoDto
                        {
                            Id = cc.Id,
                            ClienteId = cc.ClienteId,
                            FazendaId = cc.FazendaId,
                            NomeCliente = c.Nome,
                            NomeFazenda = f.Nome,
                            CodigoContrato = cc.CodigoContrato,
                            DataInicopagamento = cc.DataInicopagamento,
                            DataFimTryal = cc.DataFimTryal,
                            Situacao = cc.Situacao,
                            AssinadoPeloPortal = cc.AssinadoPeloPortal,
                            CodigoObjetoFazenda = cc.CodigoObjetoFazenda,
                        });

            query = query.OrderBy(x => x.NomeCliente).AsQueryable();


            var pagedResult = await Pagedresult<ClienteContratoDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }
    }
}
