using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;

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

        public Guid Id { get; private set; }

        public Guid PagamentoId { get; private set; }

        public string NossoNumero { get; private set; }

        public string SeuNumero { get; private set; }

        public DateTime? DataEntrada { get; private set; }

        public DateTime? DataVencimento { get; private set; }

        public DateTime? DataLimitePagamento { get; private set; }

        public decimal? ValorMora { get; private set; }

        public decimal? ValorDesconto { get; private set; }

        public decimal? ValorAcrescimos { get; private set; }

        public virtual DetalhesPagamento? DetalherPagamento { get; set; }
    }
}
