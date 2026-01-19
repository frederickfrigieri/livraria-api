using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Maps;

internal class AssuntoMap : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder.ToTable("Assunto");

        builder.HasKey(s => s.Codigo);
        
        builder.Property(s => s.Codigo).HasColumnName("CodAs");

        builder.Property(s => s.Descricao)
            .IsRequired()
            .HasColumnType("varchar(20)");

        builder.HasData(
            new { Codigo = 1, Descricao = "Engenharia de Software" },
            new { Codigo = 2, Descricao = "Arquitetura de Sistemas" },
            new { Codigo = 3, Descricao = "Design Patterns" }
        );
    }
}