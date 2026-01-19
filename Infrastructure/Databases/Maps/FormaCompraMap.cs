using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Maps;

internal class FormaCompraMap : IEntityTypeConfiguration<FormaCompra>
{
    public void Configure(EntityTypeBuilder<FormaCompra> builder)
    {
        builder.ToTable("Forma_Compra");

        builder.HasKey(x => x.Codigo);

        builder.Property(x => x.Codigo)
            .HasColumnName("Cod");

        builder.Property(x => x.Descricao)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.HasData(
            new { Codigo = 1, Descricao = "Balcao" },
            new { Codigo = 2, Descricao = "Self-Service" },
            new { Codigo = 3, Descricao = "Internet" },
            new { Codigo = 4, Descricao = "Evento" }
        );
    }
}
