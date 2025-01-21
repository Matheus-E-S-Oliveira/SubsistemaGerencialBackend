using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.EnderecoFazendas;

namespace SubsistemaGerencialBackend.Configuration.EnderecosFazendaConfiguration
{
    public class EnderecoFazendaConfiguration : IEntityTypeConfiguration<EnderecoFazenda>
    {
        public void Configure(EntityTypeBuilder<EnderecoFazenda> builder)
        {
            builder.ToTable("endereco_fazenda");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.ComercialUf)
                .HasMaxLength(2)
                .IsRequired(false);

            builder.Property(c => c.ComercialCidade)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.ComercialCep)
                .HasMaxLength(9)
                .IsRequired(false);

            builder.Property(c => c.ComercialRua)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.ComercialBairro)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.ComercialNumero)
                .HasMaxLength(20)
                .IsRequired(false);
        
            builder.Property(c => c.ComercialComplemento)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.FaturaUf)
                .HasMaxLength(2)
                .IsRequired(false);

            builder.Property(c => c.FaturaCidade)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.FaturaCep)
                .HasMaxLength(9)
                .IsRequired(false);

            builder.Property(c => c.FaturaRua)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.FaturaBairro)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.FaturaNumero)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(c => c.FaturaComplemento)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();
        }
    }
}
