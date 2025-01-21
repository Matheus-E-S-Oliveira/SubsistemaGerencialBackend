using SubsistemaGerencialBackend.Enums.SituacaoContratos;
using SubsistemaGerencialBackend.Models.Clientes;

namespace SubsistemaGerencialBackend.Models.ClienteContratos
{
    public class ClienteContrato : Base
    {
        public ClienteContrato(Guid clienteId,
                               string codigoContrato,
                               DateTime? dataInicopagamento,
                               bool? assinadoPeloPortal,
                               DateTime? dataFimTryal,
                               SituacaoContrato situacao)
        {
            ClienteId = clienteId;
            CodigoContrato = codigoContrato;
            DataInicopagamento = dataInicopagamento;
            AssinadoPeloPortal = assinadoPeloPortal;
            DataFimTryal = dataFimTryal;
            Situacao = situacao;
        }

        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public string CodigoContrato { get; set; }

        public DateTime? DataInicopagamento { get; set; }

        public bool? AssinadoPeloPortal { get; set; }

        public DateTime? DataFimTryal { get; set; }

        public SituacaoContrato Situacao { get; set; }

        public virtual Cliente? Cliente { get; set; }
    }
}
