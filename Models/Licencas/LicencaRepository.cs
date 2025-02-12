
using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;

namespace SubsistemaGerencialBackend.Models.Licencas
{
    public class LicencaRepository(AppDbContext context) : ILicencaRepository
    {
        public  IQueryable<LicencaDto> GetLicencaVencida(string? nome, string? cpf)
        {
            var hoje = DateTime.Today;
            var licenca = context.Licencas
                .Where(l => l.DataVencimento.HasValue &&
                l.DataVencimento >= hoje.AddDays(-90) &&
                l.DataVencimento < hoje)
                .Select(l => new LicencaDto
                {
                    Id = l.Id,
                    ClienteId = l.ClienteId,
                    NomeCliente = l.Cliente!.Nome,
                    CpfCliente = l.Cliente.Cpf ?? "",
                    StatusLicenca = l.StatusLicenca,
                    DataInico = l.DataInico,
                    DataVencimento = l.DataVencimento,
                    Plano = l.Plano,
                    Reference = l.Reference,
                    FaturaGerada = l.FaturaGerada
                });

            licenca = licenca.Where(c =>
               (string.IsNullOrWhiteSpace(nome) || (c.NomeCliente.Contains(nome.ToUpper()))) &&
               (string.IsNullOrWhiteSpace(cpf) || (c.CpfCliente.Contains(cpf))))
               .OrderBy(l => l.DataVencimento);

            return licenca;
        }

        public IQueryable<LicencaDto> GetLicencaVencendo(string? nome, string? cpf)
        {
            var hoje = DateTime.Today;
            var licenca = context.Licencas
                .Where(l => l.DataVencimento.HasValue &&
                l.DataVencimento >= hoje &&
                l.DataVencimento < hoje.AddDays(30))
                .Select(l => new LicencaDto
                {
                    Id = l.Id,
                    ClienteId = l.ClienteId,
                    NomeCliente = l.Cliente!.Nome,
                    CpfCliente = l.Cliente.Cpf ?? "",
                    StatusLicenca = l.StatusLicenca,
                    DataInico = l.DataInico,
                    DataVencimento = l.DataVencimento,
                    Plano = l.Plano,
                    Reference = l.Reference,
                    FaturaGerada = l.FaturaGerada
                });
                

            licenca = licenca.Where(c =>
                (string.IsNullOrWhiteSpace(nome) || (c.NomeCliente.Contains(nome.ToUpper()))) &&
                (string.IsNullOrWhiteSpace(cpf) || (c.CpfCliente.Contains(cpf))))
                .OrderBy(l => l.DataVencimento);


            return licenca;
        }
    }
}
