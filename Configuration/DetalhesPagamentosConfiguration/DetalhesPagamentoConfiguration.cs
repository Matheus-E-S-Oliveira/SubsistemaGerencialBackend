using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.Boletos;
using SubsistemaGerencialBackend.Models.DeatlhesPagamentos;

namespace SubsistemaGerencialBackend.Configuration.DetalhesPagamentosConfiguration
{
    public class DetalhesPagamentoConfiguration : IEntityTypeConfiguration<DetalhesPagamento>
    {
        public void Configure(EntityTypeBuilder<DetalhesPagamento> builder)
        {
            builder.ToTable("detalhes_pagamento");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.FormaPagamneto)
                .HasConversion<int?>()
                .IsRequired();

            builder.Property(c => c.Valor)
                .IsRequired(false);

            builder.Property(c => c.ValorCobrado)
                .IsRequired(false);

            builder.Property(c => c.DataLiquidacao)
                .IsRequired(false);

            builder.Property(c => c.StatusPagamento)
                .HasConversion<int?>()
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.HasOne(c => c.Licenca)
                .WithOne(c => c.DetalherPagamento)
                .HasForeignKey<DetalhesPagamento>(c => c.LicencaId);

            builder.HasOne(c => c.Boleto)
                .WithOne(c => c.DetalherPagamento)
                .HasForeignKey<Boleto>(c => c.PagamentoId);
        }
    }
}
