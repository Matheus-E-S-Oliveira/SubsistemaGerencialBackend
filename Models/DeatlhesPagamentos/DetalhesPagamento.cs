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

        public Guid Id { get; private set; }

        public Guid LicencaId { get; private set; }

        public FormaPagamento FormaPagamneto { get; private set; }

        public decimal? Valor {  get; private set; }

        public decimal? ValorCobrado { get; private set; }

        public DateTime? DataLiquidacao { get; private set; }

        public StatusPagamento StatusPagamento { get; private set; }

        public virtual Licenca? Licenca { get; set; }

        public virtual Boleto? Boleto { get; set; }
    }
}
