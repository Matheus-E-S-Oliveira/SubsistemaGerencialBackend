using SubsistemaGerencialBackend.Models.Fazendas;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubsistemaGerencialBackend.Models.EnderecoFazendas
{
    public class EnderecoFazenda : Base
    {
        public EnderecoFazenda(Guid fazendaId,
                               string? comercialUf,
                               string? comercialCidade,
                               string? comercialCep,
                               string? comercialRua,
                               string? comercialBairro,
                               string? comercialNumero,
                               string? comercialComplemento,
                               string? faturaUf,
                               string? faturaCidade,
                               string? faturaCep,
                               string? faturaRua,
                               string? faturaBairro,
                               string? faturaNumero,
                               string? faturaComplemento)
        {
            FazendaId = fazendaId;
            ComercialUf = comercialUf;
            ComercialCidade = comercialCidade;
            ComercialCep = comercialCep;
            ComercialRua = comercialRua;
            ComercialBairro = comercialBairro;
            ComercialNumero = comercialNumero;
            ComercialComplemento = comercialComplemento;
            FaturaUf = faturaUf;
            FaturaCidade = faturaCidade;
            FaturaCep = faturaCep;
            FaturaRua = faturaRua;
            FaturaBairro = faturaBairro;
            FaturaNumero = faturaNumero;
            FaturaComplemento = faturaComplemento;
        }

        public Guid Id { get; private set; }

        public Guid FazendaId { get; private set; }

        public string? ComercialUf { get; private set; }

        public string? ComercialCidade { get; private set; }

        public string? ComercialCep { get; private set; }

        public string? ComercialRua { get; private set; }

        public string? ComercialBairro { get; private set; }

        public string? ComercialNumero { get; private set; }

        public string? ComercialComplemento { get; private set; }

        public string? FaturaUf { get; private set; }

        public string? FaturaCidade { get; private set;}

        public string? FaturaCep { get; private set; }

        public string? FaturaRua { get; private set; }

        public string? FaturaBairro { get; private set; }

        public string? FaturaNumero { get; private set ; }

        public string? FaturaComplemento { get; private set; }

        [NotMapped]
        public virtual Fazenda? Fazenda { get; set; }
    }

    public class EndereçoDto
    {
        public Guid Id { get; set; }

        public Guid FazendaId { get; set; }

        public string? ComercialUf { get; set; }

        public string? ComercialCidade { get; set; }

        public string? ComercialCep { get; set; }

        public string? ComercialRua { get; set; }

        public string? ComercialBairro { get; set; }

        public string? ComercialNumero { get; set; }

        public string? ComercialComplemento { get; set; }

        public string? FaturaUf { get; set; }

        public string? FaturaCidade { get; set; }

        public string? FaturaCep { get; set; }

        public string? FaturaRua { get; set; }

        public string? FaturaBairro { get; set; }

        public string? FaturaNumero { get; set; }

        public string? FaturaComplemento { get; set; }
    }
}
