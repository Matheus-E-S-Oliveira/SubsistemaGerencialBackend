using SubsistemaGerencialBackend.Enums.Planos;
using SubsistemaGerencialBackend.Enums.StatusLicencas;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;

namespace SubsistemaGerencialBackend.Models.Licencas
{
    public class Licenca : Base
    {
        public Licenca(Guid clienteId,
                       DateTime? dataInico,
                       DateTime? dataVencimento,
                       string reference,
                       Plano plano,
                       StatusLicenca statusLicenca,
                       bool faturaGerada)
        { 
            ClienteId = clienteId;
            DataInico = dataInico;
            DataVencimento = dataVencimento;
            Reference = reference;
            Plano = plano;
            StatusLicenca = statusLicenca;
            FaturaGerada = faturaGerada;
        }

        public Guid Id { get; private set; }

        public Guid ClienteId { get; private set; }

        public DateTime? DataInico { get; private set; }

        public DateTime? DataVencimento { get; private set; }

        public string Reference { get; private set; }

        public Plano Plano { get; private set; }

        public StatusLicenca StatusLicenca { get; private set; }

        public bool FaturaGerada { get; private set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual DetalhesPagamento? DetalherPagamento { get; set; }
    }
}
