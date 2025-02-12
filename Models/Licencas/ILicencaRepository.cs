namespace SubsistemaGerencialBackend.Models.Licencas
{
    public interface ILicencaRepository
    {
        public IQueryable<LicencaDto> GetLicencaVencida(string? nome, string? cpf);

        public IQueryable<LicencaDto> GetLicencaVencendo(string? nome, string? cpf);
    }
}
