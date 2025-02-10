using Microsoft.AspNetCore.Mvc;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Models.Clientes;

namespace SubsistemaGerencialBackend.Endpoints.Clientes.Queries
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        [ProducesResponseType(typeof(Pagedresult<ClienteDto>), 200)] // Adiciona detalhes de resposta para Swagger
        public async Task<ActionResult<Pagedresult<ClienteDto>>> GetPaged([FromQuery] int pageNumber = 1,
                                                                          [FromQuery] int pageSize = 10,
                                                                          [FromQuery] string? nome = null,
                                                                          [FromQuery] string? cpf = null)
        {
            string nomeMaisculo = string.Empty;

            if (!string.IsNullOrEmpty(nome))
            {
                nomeMaisculo = nome.ToUpper();
            }
            var query = _context.Clientes
                .Where(c => 
                (string.IsNullOrWhiteSpace(nome) || (c.Nome != null && c.Nome.Contains(nome.ToUpper()))) && 
                (string.IsNullOrWhiteSpace(cpf) || (c.Cpf != null && c.Cpf.Contains(cpf))))
                .Select(c => new ClienteDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Cpf = c.Cpf,
                    Situacao = c.Situacao,
                    Email = c.Email,
                    Telefone = c.Telefone
                })
                .OrderBy(c => c.Nome)
                .AsQueryable();

            var pagedResult = await Pagedresult<ClienteDto>.ToPagedResultAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }
    }

}
