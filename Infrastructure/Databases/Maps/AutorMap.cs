using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Maps;

internal class AutorMap : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autor");

        builder.HasKey(a => a.Codigo);

        builder.Property(x => x.Codigo).HasColumnName("CodAu");

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasColumnType("varchar(40)");

        builder.HasData(
            new { Codigo = 1, Nome = "Robert C. Martin" },
            new { Codigo = 2, Nome = "Martin Fowler" },
            new { Codigo = 3, Nome = "Eric Evans" }
        );
    }
}
