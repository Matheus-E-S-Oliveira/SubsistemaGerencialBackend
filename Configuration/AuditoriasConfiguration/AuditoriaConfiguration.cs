using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubsistemaGerencialBackend.Models.Auditorias;

namespace SubsistemaGerencialBackend.Configuration.AuditoriasConfiguration
{
    public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable("auditoria");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x =>x.Usuario)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Acao)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Tabela)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.DataHora)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(250)
                .IsRequired();

        }
    }
}
