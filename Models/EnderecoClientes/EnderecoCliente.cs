using SubsistemaGerencialBackend.Models.Clientes;

namespace SubsistemaGerencialBackend.Models.EnderecoClientes
{
    public class EnderecoCliente : Base
    {
        public EnderecoCliente(Guid clienteId,
                               string? uf,
                               string? cidade,
                               string? cep,
                               string? rua,
                               string? bairro,
                               string? numero,
                               string? complemento)
        {
            ClienteId = clienteId;
            Uf = uf;
            Cidade = cidade;
            Cep = cep;
            Rua = rua;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
        }

        public Guid Id { get; private set; }

        public Guid ClienteId { get; private set; }

        public string? Uf { get; private set; }

        public string? Cidade { get; private set; }

        public string? Cep { get; private set; }

        public string? Rua { get; private set; }

        public string? Bairro { get; private set; }

        public string? Numero { get; private set; }

        public string? Complemento { get; private set; }

        public virtual Cliente? Cliente { get; set; }
    }

    public class EnderecoClienteDto
    {
        public Guid EnderecoId { get; set; }
        public string? Uf { get; set; } = string.Empty;
        public string? Cidade { get; set; } = string.Empty;
        public string? Cep { get; set; } = string.Empty;
        public string? Rua { get; set; } = string.Empty;
        public string? Bairro { get; set; } = string.Empty;
        public string? Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;

    }

}
