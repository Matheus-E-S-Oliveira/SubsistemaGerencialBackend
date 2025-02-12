
using SubsistemaGerencialBackend.AppDbContexts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SubsistemaGerencialBackend.Models.Boletos
{
    public class BoletoRepository(AppDbContext context) : IBoletoRepository
    {
        public IQueryable<BoletoDto> GetBoletosVencendo(string? nome, string? cpf)
        {
            var hoje = DateTime.Today;
            var boletosQuery = context.Boletos
                    .Where(b => b.DataVencimento.HasValue &&
                    (b.DataVencimento >= hoje) &&
                    (b.DataVencimento < hoje.AddDays(30)));

            var query = GerandoModeloDeAcordoComResultado(boletosQuery);

            var result = AplicarFiltros(query, nome, cpf);

            return result;
        }

        public IQueryable<BoletoDto> GetBoletosVencidos(string? nome, string? cpf)
        {
            var hoje = DateTime.Today;

            var boletosQuery = context.Boletos
                    .Where(b => b.DataVencimento.HasValue &&
                    (b.DataVencimento >= hoje.AddDays(-90)) &&
                    (b.DataVencimento < hoje));

            var query = GerandoModeloDeAcordoComResultado(boletosQuery);

            var result = AplicarFiltros(query, nome, cpf);

            return result;

        }

        public IQueryable<BoletoDto> GetPaged(string? nome, string? cpf, DateTime? data)
        {
            var boletosQuery = context.Boletos
           .Where(b => data.HasValue && b.DataEntrada.HasValue &&
               b.DataEntrada.Value.Month == data.Value.Month &&
               b.DataEntrada.Value.Year == data.Value.Year);

            var query = GerandoModeloDeAcordoComResultado(boletosQuery);

            var result = AplicarFiltros(query, nome, cpf);

            return result;
        }

        private IQueryable<BoletoDto> GerandoModeloDeAcordoComResultado(IQueryable<Boleto> query)
        {
            return  from b in query
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
        }

        private IQueryable<BoletoDto> AplicarFiltros(IQueryable<BoletoDto> query, string? nome, string? cpf)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                nome = nome.ToUpper();
            }
            return query
                .Where(c =>
                (string.IsNullOrWhiteSpace(nome) || (c.Nome != null && c.Nome.Contains(nome))) &&
                (string.IsNullOrWhiteSpace(cpf) || (c.Cpf != null && c.Cpf.Contains(cpf))))
                .OrderByDescending(c => c.DataVencimento)
                .AsQueryable();
        }
    }
}
