using SubsistemaGerencialBackend.Enums.SituacaoContratos;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.Fazendas;

namespace SubsistemaGerencialBackend.Models.ClienteContratos
{
    public class ClienteContrato : Base
    {
        public ClienteContrato(Guid clienteId,
                               Guid fazendaId,
                               string codigoContrato,
                               DateTime? dataInicopagamento,
                               bool? assinadoPeloPortal,
                               DateTime? dataFimTryal,
                               SituacaoContrato situacao,
                               string codigoObjetoFazenda)
        {
            ClienteId = clienteId;
            FazendaId = fazendaId;
            CodigoContrato = codigoContrato;
            DataInicopagamento = dataInicopagamento;
            AssinadoPeloPortal = assinadoPeloPortal;
            DataFimTryal = dataFimTryal;
            Situacao = situacao;
            CodigoObjetoFazenda = codigoObjetoFazenda;
        }

        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public Guid FazendaId { get; set; }

        public string CodigoContrato { get; set; }

        public DateTime? DataInicopagamento { get; set; }

        public bool? AssinadoPeloPortal { get; set; }

        public DateTime? DataFimTryal { get; set; }

        public SituacaoContrato Situacao { get; set; }

        public string CodigoObjetoFazenda { get;  set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual Fazenda? Fazenda { get; set; }
    }

    public class ClienteContratoDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public Guid FazendaId { get; set; }

        public string NomeCliente { get; set; } = string.Empty;

        public string NomeFazenda { get; set; } = string.Empty;

        public string CodigoContrato { get; set; } = string.Empty;

        public DateTime? DataInicopagamento { get; set; }

        public bool? AssinadoPeloPortal { get; set; }

        public DateTime? DataFimTryal { get; set; }

        public SituacaoContrato Situacao { get; set; }

        public string CodigoObjetoFazenda { get; set; } = string.Empty;
    }
}
