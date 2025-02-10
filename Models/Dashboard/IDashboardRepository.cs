namespace SubsistemaGerencialBackend.Models.Dashboard
{
    public interface IDashboardRepository
    {
        public Task<DashboardDados> GetDashboardDataAsync();

        public Task<ClientesResumo> GetClientesResumo();

        public Task<FormasPagamentoResumo> GetFormasPagamentoResumo();

        public Task<PlanoResumo> GetPlanoResumo();

        public Task<LicencaResumo> GetLicencaResumo();

        public Task<TotalAnoResumo> GetTotalLicencaAno();

        public Task<DadosMensais> ObterDadosMensais();

        public Task<DadosDiversos> GetDadosDiversos();

        public Task<Contratos> GetContratos();

        //public Task CalcularAcumulacao(Dictionary<string, decimal> valoresAReceberPorMes);
    }
}
