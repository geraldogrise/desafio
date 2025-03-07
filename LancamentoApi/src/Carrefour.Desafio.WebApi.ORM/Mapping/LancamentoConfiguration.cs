using Carrefour.Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carrefour.Desafio.WebApi.ORM.Mapping
{
    public class LancamentoConfiguration : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.ToTable("TBL_LANCAMENTO");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasColumnName("LAN_ID")
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            builder.Property(l => l.DataLancamento)
                .HasColumnName("DT_LANCAMENTO")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(l => l.Tipo)
                .HasColumnName("TP_LANCAMENTO")
                .IsRequired()
                .HasMaxLength(10)
                .HasConversion<string>();

            builder.Property(l => l.ValorLancamento)
                .HasColumnName("VLR_LANCAMENTO")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.Descricao)
                .HasColumnName("DES_LANCAMENTO")
                .HasMaxLength(255);

            builder.Property(l => l.Categoria)
                .HasColumnName("CAT_LANCAMENTO")
                .HasMaxLength(100);

            builder.Property(l => l.CreatedAt)
                .HasColumnName("CREATED_AT")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(l => l.UpdatedAt)
                .HasColumnName("UPDATED_AT")
                .HasColumnType("datetime");
        }
    }
}
