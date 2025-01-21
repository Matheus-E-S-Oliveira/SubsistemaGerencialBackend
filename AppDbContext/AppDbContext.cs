using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.Configuration.AuditoriasConfiguration;
using SubsistemaGerencialBackend.Configuration.BoletosConfiguration;
using SubsistemaGerencialBackend.Configuration.ClientesConfiguration;
using SubsistemaGerencialBackend.Configuration.DetalhesPagamentosConfiguration;
using SubsistemaGerencialBackend.Configuration.EnderecosClienteConfiguration;
using SubsistemaGerencialBackend.Configuration.EnderecosFazendaConfiguration;
using SubsistemaGerencialBackend.Configuration.FazendasConfiguration;
using SubsistemaGerencialBackend.Configuration.LicencasConfiguration;
using SubsistemaGerencialBackend.Models;
using SubsistemaGerencialBackend.Models.Auditorias;
using SubsistemaGerencialBackend.Models.Boletos;
using SubsistemaGerencialBackend.Models.ClienteContratos;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;
using SubsistemaGerencialBackend.Models.EnderecoClientes;
using SubsistemaGerencialBackend.Models.EnderecoFazendas;
using SubsistemaGerencialBackend.Models.Fazendas;
using SubsistemaGerencialBackend.Models.Licencas;

namespace SubsistemaGerencialBackend.AppDbContexts
{
    public class AppDbContext : DbContext
    { 

        private readonly IHttpContextAccessor _httpContextAccessor; 

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Auditoria> Auditorias { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<ClienteContrato> ClienteContratos { get; set; }

        public DbSet<Fazenda> Fazendas { get; set; }

        public DbSet<EnderecoCliente> EnderecoClientes { get; set; }

        public DbSet<EnderecoFazenda> EnderecoFazendas { get; set; }

        public DbSet<Licenca> Licencas { get; set; }

        public DbSet<DetalhesPagamento> DetalhesPagamentos { get; set; }

        public DbSet<Boleto> Boletos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new FazendaConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoClienteConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoFazendaConfiguration());
            modelBuilder.ApplyConfiguration(new LicencaConfiguration());
            modelBuilder.ApplyConfiguration(new DetalhesPagamentoConfiguration());
            modelBuilder.ApplyConfiguration(new BoletoConfiguration());
            modelBuilder.ApplyConfiguration(new AuditoriaConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var usuarioAtual = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Base && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .ToList();

            foreach (var entry in entries)
            {
                var entidade = (Base)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entidade.DataCriacao = DateTime.Now;
                }

                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    entidade.DataAtualizacao = DateTime.Now;
                }

                var auditoria = new Auditoria
                {
                    Usuario = usuarioAtual,
                    Acao = entry.State.ToString(),
                    Tabela = entry.Entity.GetType().Name,
                    DataHora = DateTime.Now,
                    Descricao = entry.State == EntityState.Deleted
                            ? entry.OriginalValues.ToString() ?? "Valores Originais Nulos"
                            : entry.CurrentValues.ToString() ?? "Valores Atuais Nulos"
                };

                Auditorias.Add(auditoria);
            }

            return base.SaveChanges();
        }

    }
}
