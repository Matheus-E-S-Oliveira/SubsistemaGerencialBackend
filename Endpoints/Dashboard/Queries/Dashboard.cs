using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Enums.FormasPagamentos;
using SubsistemaGerencialBackend.Enums.SituacaoClientes;
using SubsistemaGerencialBackend.Enums.StatusPagamentos;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.Dashboard;

namespace SubsistemaGerencialBackend.Endpoints.Dashboard.Queries
{
    [ApiController]
    [Route("api/dashboard")]
    public class Dashboard(IDashboardRepository dashboardRepository) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Dashboard>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<IEnumerable<Dashboard>>> GetPaged()
        {
            var dashboard = await dashboardRepository.GetDashboardDataAsync();

            return Ok(dashboard);
        }
   
        [HttpGet("clientes")]
        public async Task<ActionResult<ClientesResumo>> GetClientesResumo()
        {
            var resumo = await dashboardRepository.GetClientesResumo();

            return Ok(resumo);
        }

        [HttpGet("pagamentos")]
        public async Task<ActionResult<FormasPagamentoResumo>> GetFormasPagamentoResumo()
        {
            var resumo = await dashboardRepository.GetFormasPagamentoResumo();

            return Ok(resumo);
        }

        [HttpGet("plano")]
        public async Task<ActionResult<PlanoResumo>> GetPlanoResumo()
        {
            var resumo = await dashboardRepository.GetPlanoResumo();

            return Ok(resumo);
        }

        [HttpGet("licenca")]
        public async Task<ActionResult<LicencaResumo>> GetLicencaResumo()
        {
            var resumo = await dashboardRepository.GetLicencaResumo();

            return Ok(resumo);
        }

        [HttpGet("total")]
        public async Task<ActionResult<TotalAnoResumo>> GetTotalLicencaAno()
        {
            var resumo = await dashboardRepository.GetTotalLicencaAno();

            return Ok(resumo);
        }

        [HttpGet("mensais")]
        public async Task<ActionResult<DadosMensais>> ObterDadosMensais()
        {
            var resumo = await dashboardRepository.ObterDadosMensais();

            return Ok(resumo);
        }

        [HttpGet("gerais")]
        public async Task<ActionResult<DadosDiversos>> GetDadosDiversos()
        {
            var resumo = await dashboardRepository.GetDadosDiversos();

            return Ok(resumo);
        }

        [HttpGet("contrato")]
        public async Task<ActionResult<Contratos>> GetConatrto()
        {
            var resumo = await dashboardRepository.GetContratos();

            return Ok(resumo);
        }
    }
}
