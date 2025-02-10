using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.Dashboard;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;
using System.Reflection.Emit;

namespace SubsistemaGerencialBackend.Configuration.DashboardConfiguration
{
    public class DashboardConfiguration : IEntityTypeConfiguration<DadosGeraisEntity>
    {
        public void Configure(EntityTypeBuilder<DadosGeraisEntity> builder)
        {
            builder.ToTable("dados_dashboard");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
            .Property(d => d.DistribuicaoLicencasPorAnoJson)
            .HasColumnType("TEXT"); // Armazena como JSON

            builder
                .Property(d => d.ValoresAReceberPorMesJson)
            .HasColumnType("TEXT");

            builder
                .Property(d => d.ReceitaPorMesJson)
                .HasColumnType("TEXT");

            builder
                .Property(d => d.DistribuicaoLicencasPorMesJson)
                .HasColumnType("TEXT");

            builder
                .Property(d => d.FormasPagamentoJson)
                .HasColumnType("TEXT");

            builder
                .Property(d => d.PlanosJson)
                .HasColumnType("TEXT");

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();
        }
    }
}
