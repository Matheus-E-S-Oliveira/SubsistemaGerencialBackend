using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.Boletos;

namespace SubsistemaGerencialBackend.Configuration.BoletosConfiguration
{
    public class BoletoConfiguration : IEntityTypeConfiguration<Boleto>
    {
        public void Configure(EntityTypeBuilder<Boleto> builder)
        {
            builder.ToTable("boletos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.NossoNumero)
                .HasMaxLength(8)
                .IsRequired();
 
            builder.Property(x => x.SeuNumero)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.DataEntrada)
                .IsRequired(false);

            builder.Property(x => x.DataVencimento)
                .IsRequired(false);

            builder.Property(x => x.DataLimitePagamento)
                .IsRequired(false);

            builder.Property(x => x.ValorMora)
                .IsRequired(false);

            builder.Property(x => x.ValorDesconto)
                .IsRequired(false);

            builder.Property(x => x.ValorAcrescimos)
                .IsRequired(false);

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.HasOne(x => x.DetalherPagamento)
                .WithOne(x => x.Boleto)
                .HasForeignKey<Boleto>(x => x.PagamentoId);
        }
    }
}
