using SubsistemaGerencialBackend.Enums.FormasPagamentos;
using SubsistemaGerencialBackend.Enums.StatusPagamentos;
using SubsistemaGerencialBackend.Models.Boletos;
using SubsistemaGerencialBackend.Models.Licencas;

namespace SubsistemaGerencialBackend.Models.DeatlhesPagamentos
{
    public class DetalhesPagamento : Base
    {
        public DetalhesPagamento() { }
        public DetalhesPagamento(Guid licencaId,
                                 FormaPagamento formaPagamneto,
                                 decimal valor,
                                 decimal valorCobrado,
                                 DateTime? dataLiquidacao,
                                 StatusPagamento statusPagamento)
        {
            LicencaId = licencaId;
            FormaPagamneto = formaPagamneto;
            Valor = valor;
            ValorCobrado = valorCobrado;
            DataLiquidacao = dataLiquidacao;
            StatusPagamento = statusPagamento;
        }

        public Guid Id { get; set; }

        public Guid LicencaId { get; set; }

        public FormaPagamento FormaPagamneto { get; set; }

        public decimal? Valor {  get; set; }

        public decimal? ValorCobrado { get; set; }

        public DateTime? DataLiquidacao { get; set; }

        public StatusPagamento StatusPagamento { get; set; }

        public virtual Licenca? Licenca { get; set; }

        public virtual Boleto? Boleto { get; set; }
    }
}
