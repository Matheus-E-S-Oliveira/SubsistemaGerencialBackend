using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Models.Boletos;
using SubsistemaGerencialBackend.Models.Clientes;

namespace SubsistemaGerencialBackend.Endpoints.Boletos.Queries
{
    [ApiController]
    [Route("api/boleto")]
    public class Boleto(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<BoletoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<BoletoDto>>> GetPaged([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nome = null,
                                                                          [FromQuery] string? cpf = null,
                                                                          [FromQuery] DateTime? data = null)
        {
            var boletosQuery = context.Boletos
            .Where(b => data.HasValue && b.DataEntrada.HasValue &&
                b.DataEntrada.Value.Month == data.Value.Month &&
                b.DataEntrada.Value.Year == data.Value.Year);

            var query = from b in boletosQuery
                        join dp in context.DetalhesPagamentos on b.PagamentoId equals dp.Id
                        join l in context.Licencas on dp.LicencaId equals l.Id
                        join c in context.Clientes on l.ClienteId equals c.Id
                        select new BoletoDto
                        {
                            Id = b.Id,
                            PagamentoId = b.PagamentoId,
                            Nome = c.Nome,
                            Cpf = c.Cpf,
                            NossoNumero = b.NossoNumero,
                            SeuNumero = b.SeuNumero,
                            DataEntrada = b.DataEntrada,
                            DataVencimento = b.DataVencimento,
                            DataLimitePagamento = b.DataLimitePagamento,
                            Valor = dp.Valor,
                            ValorAcrescimos = b.ValorAcrescimos,
                            ValorMora = b.ValorMora,
                            ValorDesconto = b.ValorDesconto,
                            ValorCobrado = dp.ValorCobrado,
                            StatusPagamento = dp.StatusPagamento,
                        };

            var nomeMaisculo = string.Empty;
            if (!string.IsNullOrEmpty(nome))
            {
                nomeMaisculo = nome.ToUpper();
            }

            query = query
                .Where(c =>
                (string.IsNullOrWhiteSpace(nome) || (c.Nome != null && c.Nome.Contains(nomeMaisculo))) &&
                (string.IsNullOrWhiteSpace(cpf) || (c.Cpf != null && c.Cpf.Contains(cpf))))
                .OrderBy(c => c.Nome)
                .AsQueryable();

            var pagedResult = await Pagedresult<BoletoDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }
    }
}
