using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.ClienteContratos;

namespace SubsistemaGerencialBackend.Configuration.ClienteContratosConfiguration
{
    public class ClienteContratoConfiguration : IEntityTypeConfiguration<ClienteContrato>
    {
        public void Configure(EntityTypeBuilder<ClienteContrato> builder)
        {
            builder.ToTable("cliente_contrato");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.CodigoContrato)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(c => c.DataInicopagamento)
                .IsRequired(false);

            builder.Property(c => c.AssinadoPeloPortal)
                .IsRequired(false);

            builder.Property(c => c.DataFimTryal)
                .IsRequired(false);

            builder.Property(c => c.Situacao)
                .HasConversion<int?>()
                .IsRequired();

            builder.Property(t => t.CodigoObjetoFazenda)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();
        }
    }
}
