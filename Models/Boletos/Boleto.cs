using SubsistemaGerencialBackend.Enums.StatusPagamentos;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubsistemaGerencialBackend.Models.Boletos
{
    public class Boleto : Base
    {
        public Boleto() 
        {
            NossoNumero = string.Empty;
            SeuNumero = string.Empty;
        }

        public Boleto(Guid pagamentoId,
                      string nossoNumero,
                      string seuNumero,
                      DateTime? dataEntrada,
                      DateTime? dataVencimento,
                      DateTime? dataLimitePagamento,
                      decimal valorMora,
                      decimal valorDesconto,
                      decimal valorAcrescimos)
        {
            PagamentoId = pagamentoId;
            NossoNumero = nossoNumero;
            SeuNumero = seuNumero;
            DataEntrada = dataEntrada;
            DataVencimento = dataVencimento;
            DataLimitePagamento = dataLimitePagamento;
            ValorMora = valorMora;
            ValorDesconto = valorDesconto;
            ValorAcrescimos = valorAcrescimos;
        }

        public Guid Id { get; set; }

        public Guid PagamentoId { get; set; }

        public string NossoNumero { get; set; }

        public string SeuNumero { get; set; }

        public DateTime? DataEntrada { get; set; }

        public DateTime? DataVencimento { get; set; }

        public DateTime? DataLimitePagamento { get; set; }

        public decimal? ValorMora { get; set; }

        public decimal? ValorDesconto { get; set; }

        public decimal? ValorAcrescimos { get; set; }

        [NotMapped]
        public virtual DetalhesPagamento? DetalherPagamento { get; set; }
    }

    public class BoletoDto
    {
        public Guid Id { get; set; }

        public Guid PagamentoId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Cpf { get; set; } = string.Empty;

        public string NossoNumero { get; set; } = string.Empty;

        public string SeuNumero { get; set; } = string.Empty;

        public DateTime? DataEntrada { get; set; }

        public DateTime? DataVencimento { get; set; }

        public DateTime? DataLimitePagamento { get; set; }

        public decimal? ValorMora { get; set; }

        public decimal? ValorDesconto { get; set; }

        public decimal? ValorAcrescimos { get; set; }

        public decimal? Valor { get; set; }

        public decimal? ValorCobrado { get; set; }

        public StatusPagamento StatusPagamento { get; set; }
    }
}
