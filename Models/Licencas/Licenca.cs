using SubsistemaGerencialBackend.Enums.Planos;
using SubsistemaGerencialBackend.Enums.StatusLicencas;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;

namespace SubsistemaGerencialBackend.Models.Licencas
{
    public class Licenca : Base
    {
        public Licenca() 
        {
            Reference = string.Empty;
        }

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

        public Guid Id { get;  set; }

        public Guid ClienteId { get; set; }

        public DateTime? DataInico { get; set; }

        public DateTime? DataVencimento { get; set; }

        public string Reference { get; set; }

        public Plano Plano { get; set; }

        public StatusLicenca StatusLicenca { get; set; }

        public bool FaturaGerada { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual DetalhesPagamento? DetalherPagamento { get; set; }
    }
}
