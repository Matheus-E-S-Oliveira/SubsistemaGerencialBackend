using Microsoft.AspNetCore.Mvc;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.Licencas;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SubsistemaGerencialBackend.Endpoints.Licencas.Queries
{
    [ApiController]
    [Route("api/licenca")]
    public class Licencas(ILicencaRepository licencaRepository) : ControllerBase
    {

        [HttpGet]
        [Route("vencidos")]
        public async Task<ActionResult<Pagedresult<LicencaDto>>> GetPagedVencido([FromQuery] int pageNumber = 1,
                                                                                 [FromQuery] int pageSize = 10,
                                                                                 [FromQuery] string? nomeCliente = null,
                                                                                 [FromQuery] string? cpfCliente = null)
        {
            var query = licencaRepository.GetLicencaVencida(nomeCliente, cpfCliente);

            var pagedResult = await Pagedresult<LicencaDto>.ToPagedResultAsync(query, pageNumber, pageSize);

            return Ok(pagedResult);
        }

        [HttpGet]
        [Route("vencendo")]
        public async Task<ActionResult<Pagedresult<LicencaDto>>> GetPagedVencendo([FromQuery] int pageNumber = 1,
                                                                                  [FromQuery] int pageSize = 10,
                                                                                  [FromQuery] string? nomeCliente = null,
                                                                                  [FromQuery] string? cpfCliente = null)
        {
            var query = licencaRepository.GetLicencaVencendo(nomeCliente, cpfCliente);

            var pagedResult = await Pagedresult<LicencaDto>.ToPagedResultAsync(query, pageNumber, pageSize);

            return Ok(pagedResult);
        }
    }
}
