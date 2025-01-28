using SubsistemaGerencialBackend.Models.ClienteContratos;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.EnderecoFazendas;

namespace SubsistemaGerencialBackend.Models.Fazendas
{
    public class Fazenda : Base
    {
        public Fazenda(Guid clienteId,
                       string codigoFazenda,
                       DateTime dataCriacaoFazenda,
                       string nome,
                       int quantidadeAnimais)
        {
            ClienteId = clienteId;
            CodigoFazenda = codigoFazenda;
            DataCriacaoFazenda = dataCriacaoFazenda;
            Nome = nome;
            QuantidadeAnimais = quantidadeAnimais;
        }

        public Guid Id { get; private set; }

        public Guid ClienteId { get; private set; }

        public string CodigoFazenda {  get; private set; }

        public DateTime DataCriacaoFazenda { get; private set; }

        public string Nome {  get; private set; }

        public int QuantidadeAnimais { get; private set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<EnderecoFazenda>? EnderecoFazendas { get; set; }

        public virtual ICollection<ClienteContrato>? ClienteContrato { get; set; }
    }

    public class FazendaDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public string CodigoFazenda { get; set; } = string.Empty;

        public DateTime DataCriacaoFazenda { get; set; }

        public string Nome { get; set; } = string.Empty;

        public int QuantidadeAnimais { get; set; }
    }
}
