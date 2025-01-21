using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;
using SubsistemaGerencialBackend.Models.Licencas;

namespace SubsistemaGerencialBackend.Configuration.LicencasConfiguration
{
    public class LicencaConfiguration : IEntityTypeConfiguration<Licenca>
    {
        public void Configure(EntityTypeBuilder<Licenca> builder)
        {
            builder.ToTable("licenca");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.DataInico)
                .IsRequired(false);

            builder.Property(c => c.DataVencimento)
                .IsRequired(false);

            builder.Property(c => c.Reference)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(c => c.Plano)
                .HasConversion<int?>()
                .IsRequired();

            builder.Property(c => c.StatusLicenca)
                .HasConversion<int?>()
                .IsRequired();

            builder.Property(c => c.FaturaGerada)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.HasOne(c => c.DetalherPagamento)
                .WithOne(c => c.Licenca)
                .HasForeignKey<DetalhesPagamento>(c => c.LicencaId);
        }
    }
}
