using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.EnderecoFazendas;

namespace SubsistemaGerencialBackend.Models.Fazendas
{
    public class Fazenda : Base
    {
        public Fazenda(Guid clienteId,
                       string codigoFazenda,
                       string codigoObjetoFazenda,
                       DateTime dataCriacaoFazenda,
                       string nome,
                       int quantidadeAnimais)
        {
            ClienteId = clienteId;
            CodigoFazenda = codigoFazenda;
            CodigoObjetoFazenda = codigoObjetoFazenda;
            DataCriacaoFazenda = dataCriacaoFazenda;
            Nome = nome;
            QuantidadeAnimais = quantidadeAnimais;
        }

        public Guid Id { get; private set; }

        public Guid ClienteId { get; private set; }

        public string CodigoFazenda {  get; private set; }

        public string CodigoObjetoFazenda { get; private set; }

        public DateTime DataCriacaoFazenda { get; private set; }

        public string Nome {  get; private set; }

        public int QuantidadeAnimais { get; private set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<EnderecoFazenda>? EnderecoFazendas { get; set; }
    }
}
