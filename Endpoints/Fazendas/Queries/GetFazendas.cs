using Microsoft.AspNetCore.Mvc;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.EnderecoFazendas;
using SubsistemaGerencialBackend.Models.Fazendas;

namespace SubsistemaGerencialBackend.Endpoints.Fazendas.Queries
{
    [ApiController]
    [Route("api/fazenda")]
    public class GetFazendas : ControllerBase
    {
        private readonly AppDbContext _context;

        public GetFazendas(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<FazendaDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<FazendaDto>>> GetPaged([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nome = null,
                                                                          [FromQuery] string? codigo = null)
        {
            var query = _context.Fazendas
                .Where(c =>
                (string.IsNullOrWhiteSpace(nome) || (c.Nome!= null && c.Nome.Contains(nome))) &&
                (string.IsNullOrWhiteSpace(codigo) || (c.CodigoFazenda != null && c.CodigoFazenda.Contains(codigo))))
                .Select(c => new FazendaDto
                {
                    Id = c.Id,
                    ClienteId = c.ClienteId,
                    CodigoFazenda = c.CodigoFazenda,
                    Nome = c.Nome,
                    DataCriacaoFazenda = c.DataCriacaoFazenda,
                    QuantidadeAnimais = c.QuantidadeAnimais,

                })
                .OrderBy(c => c.Nome)
                .AsQueryable();

            var pagedResult = await Pagedresult<FazendaDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<FazendaEnderecoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<FazendaEnderecoDto>>> GetPorId([FromRoute] Guid id,
                                                                          [FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10)
        {
            var query = _context.Fazendas
                .Where(c => c.Id == id)
                .Select(c => new FazendaEnderecoDto
                {
                    Id = c.Id,
                    ClienteId = c.ClienteId,
                    CodigoFazenda = c.CodigoFazenda,
                    Nome = c.Nome,
                    DataCriacaoFazenda = c.DataCriacaoFazenda,
                    QuantidadeAnimais = c.QuantidadeAnimais,
                    NomeCliente = c.Cliente!.Nome,
                    EnderecoFazendas = c.EnderecoFazendas!.ToList(),
                })
                .OrderBy(c => c.Nome)
                .AsQueryable();

            var pagedResult = await Pagedresult<FazendaEnderecoDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }
    }
}
