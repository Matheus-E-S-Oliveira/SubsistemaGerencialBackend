using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.Clientes;

namespace SubsistemaGerencialBackend.Configuration.ClientesConfiguration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.Nome)
                .HasMaxLength(100);

            builder.Property(c => c.Cpf)
                .HasMaxLength(14)
                .IsRequired(false);

            builder.Property(c => c.Situacao)
                .HasConversion<int?>()
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(60);

            builder.Property(c => c.Telefone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();
            
            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.HasMany(c => c.EnderecosCliente)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ClienteContratos)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Fazendas)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Licenca)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
