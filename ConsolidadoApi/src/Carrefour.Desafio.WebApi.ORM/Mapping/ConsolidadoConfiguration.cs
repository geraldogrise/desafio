using Carrefour.Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carrefour.Desafio.WebApi.ORM.Mapping
{
    public class ConsolidadoConfiguration : IEntityTypeConfiguration<Consolidado>
    {
        public void Configure(EntityTypeBuilder<Consolidado> builder)
        {
            builder.ToTable("TBL_CONSOLIDADO");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CON_ID")
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.DataConsolidado)
                .HasColumnName("DT_CONSOLIDADO")
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.ValorDebito)
                .HasColumnName("VLR_DEB")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(c => c.ValorCredito)
                .HasColumnName("VLR_CRE")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(c => c.SaldoFinal)
                .HasColumnName("VLR_SLD_FINAL")
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CREATED_AT")
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("UPDATED_AT")
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}
