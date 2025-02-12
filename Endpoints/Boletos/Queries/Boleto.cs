using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Models.Boletos;
using SubsistemaGerencialBackend.Models.Clientes;

namespace SubsistemaGerencialBackend.Endpoints.Boletos.Queries
{
    [ApiController]
    [Route("api/boleto")]
    public class Boleto(IBoletoRepository boletoRepository) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<BoletoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<BoletoDto>>> GetPaged([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nome = null,
                                                                          [FromQuery] string? cpf = null,
                                                                          [FromQuery] DateTime? data = null)
        {
            var query = boletoRepository.GetPaged(nome, cpf, data);

            var pagedResult = await Pagedresult<BoletoDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet]
        [Route("vencidos")]
        [ProducesResponseType(typeof(Pagedresult<BoletoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<BoletoDto>>> GetBoletosVencidos([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nome = null,
                                                                          [FromQuery] string? cpf = null)
        {
            var query = boletoRepository.GetBoletosVencidos(nome, cpf);

            var pagedResult = await Pagedresult<BoletoDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet]
        [Route("vencendo")]
        [ProducesResponseType(typeof(Pagedresult<BoletoDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<BoletoDto>>> GetBoletosVencendo([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nome = null,
                                                                          [FromQuery] string? cpf = null)
        {
            var query = boletoRepository.GetBoletosVencendo(nome, cpf);

            var pagedResult = await Pagedresult<BoletoDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }
    }
}
