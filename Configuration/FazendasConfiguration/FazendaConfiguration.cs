using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.Fazendas;

namespace SubsistemaGerencialBackend.Configuration.FazendasConfiguration
{
    public class FazendaConfiguration : IEntityTypeConfiguration<Fazenda>
    {
        public void Configure(EntityTypeBuilder<Fazenda> builder)
        {
            builder.ToTable("fazenda");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(t => t.CodigoFazenda)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.DataCriacaoFazenda)
                .IsRequired();

            builder.Property(t => t.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.QuantidadeAnimais)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.HasMany(t => t.EnderecoFazendas)
                .WithOne(t => t.Fazenda)
                .HasForeignKey(t => t.FazendaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.ClienteContrato)
                .WithOne(t => t.Fazenda)
                .HasForeignKey (t => t.FazendaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
