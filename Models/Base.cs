namespace SubsistemaGerencialBackend.Models
{
    public abstract class Base
    {
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
