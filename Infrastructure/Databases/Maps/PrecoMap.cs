using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Maps;

internal class PrecoMap : IEntityTypeConfiguration<Preco>
{
    public void Configure(EntityTypeBuilder<Preco> builder)
    {
        builder.ToTable("Preco");

        builder.HasKey(x => x.Codigo);

        builder.Property(x => x.Codigo)
            .HasColumnName("Cod");

        builder.Property(x => x.Valor)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.HasOne(p => p.FormaCompra)
            .WithMany()
            .HasForeignKey("FormaCompra_Cod")
            .HasConstraintName("Preco_FormaCompra_FK")
            .IsRequired();

        builder.HasOne(p => p.Livro)
            .WithMany(l => l.Precos)
            .HasForeignKey("Livro_Cod")
            .HasConstraintName("Preco_Livro_FK")
            .IsRequired();
    }
}
