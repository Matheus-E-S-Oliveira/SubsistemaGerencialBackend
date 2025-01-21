namespace SubsistemaGerencialBackend.Models.Auditorias
{
    public class Auditoria : Base
    {
        public Guid Id { get; set; } 

        public string Usuario { get; set; } = string.Empty;

        public string Acao { get; set; } = string.Empty;

        public string Tabela { get; set; } = string.Empty;

        public DateTime DataHora { get; set; } = DateTime.Now;

        public string Descricao { get; set; } = string.Empty;
    }
}
