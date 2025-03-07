using Carrefour.Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrefour.Desafio.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("TBL_USER");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("USER_ID").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).HasColumnName("TXT_USERNAME").IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).HasColumnName("TXT_PASSWORD").IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).HasColumnName("TXT_EMAIL").IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasColumnName("TXT_PHONE").HasMaxLength(20);

        builder.Property(u => u.Status).HasColumnName("TXT_STATUS")
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role).HasColumnName("TXT_ROLE")
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Phone).HasColumnName("TXT_PHONE").HasMaxLength(20);

        // Mapeamento das colunas de data
        builder.Property(u => u.CreatedAt)
            .HasColumnName("CREATED_AT")
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("UPDATED_AT")
            .HasColumnType("timestamp without time zone");

    }
}
