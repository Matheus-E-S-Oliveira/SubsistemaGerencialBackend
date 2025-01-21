using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.EnderecoClientes;

namespace SubsistemaGerencialBackend.Configuration.EnderecosClienteConfiguration
{
    public class EnderecoClienteConfiguration : IEntityTypeConfiguration<EnderecoCliente>
    {
        public void Configure(EntityTypeBuilder<EnderecoCliente> builder)
        {
            builder.ToTable("endereco_cliente");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.Uf)
                .HasMaxLength(2)
                .IsRequired(false);

            builder.Property(c => c.Cidade)
                .HasMaxLength(100)
                .IsRequired(false);
        
            builder.Property(c => c.Cep)
                .HasMaxLength(9)
                .IsRequired(false);

            builder.Property(c => c.Rua)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.Bairro)
                .HasMaxLength(100);

            builder.Property(c => c.Numero)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(c => c.Complemento)
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
