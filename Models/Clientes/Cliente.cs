using SubsistemaGerencialBackend.Enums.SituacaoClientes;
using SubsistemaGerencialBackend.Models.ClienteContratos;
using SubsistemaGerencialBackend.Models.EnderecoClientes;
using SubsistemaGerencialBackend.Models.Fazendas;
using SubsistemaGerencialBackend.Models.Licencas;

namespace SubsistemaGerencialBackend.Models.Clientes
{
    public class Cliente : Base
    {
        public Cliente(string nome,
                       string? cpf,
                       SituacaoCliente situacao,
                       string? email,
                       string? telefone)
        {
            Nome = nome;
            Cpf = cpf;
            Situacao = situacao;
            Email = email;
            Telefone = telefone;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }
    
        public string? Cpf { get; set; }

        public SituacaoCliente Situacao { get; set; }

        public string? Email { get; set; }
            
        public string? Telefone { get; set; }

        public virtual ICollection<EnderecoCliente>? EnderecosCliente { get; set; }

        public virtual ICollection<ClienteContrato>? ClienteContratos { get; set; }

        public virtual ICollection<Fazenda>? Fazendas { get; set; }

        public virtual ICollection<Licenca>? Licenca { get; set; }
    }
}
