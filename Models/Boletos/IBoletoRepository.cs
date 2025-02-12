namespace SubsistemaGerencialBackend.Models.Boletos
{
    public interface IBoletoRepository
    {
        public IQueryable<BoletoDto> GetBoletosVencidos(string? nome, string? cpf);

        public IQueryable<BoletoDto> GetBoletosVencendo(string? nome, string? cpf);

        public IQueryable<BoletoDto> GetPaged(string? nome, string? cpf, DateTime? data);
    }
}
