using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Maps;

internal class LivroMapping : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livro");

        builder.HasKey(l => l.Codigo);

        builder.Property(l => l.Codigo).HasColumnName("Cod");

        builder.Property(l => l.Titulo)
            .IsRequired()
            .HasColumnType("varchar(40)");

        builder.Property(l => l.Editora)
            .IsRequired()
            .HasColumnType("varchar(40)");

        builder.Property(l => l.Edicao)
            .IsRequired();

        builder.Property(l => l.AnoPublicacao)
            .IsRequired()
            .HasColumnType("varchar(4)");

        builder.HasMany(l => l.Autores)
            .WithMany(a => a.Livros)
            .UsingEntity<Dictionary<string, object>>(
                "Livro_Autor",
                j => j.HasOne<Autor>().WithMany().HasForeignKey("Autor_CodAu").HasConstraintName("Livro_Autor_FKIndex1"),
                j => j.HasOne<Livro>().WithMany().HasForeignKey("Livro_Cod").HasConstraintName("Livro_Autor_FKIndex2")
            );

        builder.HasMany(l => l.Assuntos)
            .WithMany(s => s.Livros)
            .UsingEntity<Dictionary<string, object>>(
                "Livro_Assunto",
                j => j.HasOne<Assunto>().WithMany().HasForeignKey("Assunto_CodAs").HasConstraintName("Livro_Assunto_FKIndex1"),
                j => j.HasOne<Livro>().WithMany().HasForeignKey("Livro_Cod").HasConstraintName("Livro_Assunto_FKIndex2")
            );

        builder.HasMany(l => l.Precos)
            .WithOne(p => p.Livro)
            .HasForeignKey("Livro_Cod")
            .HasConstraintName("Preco_Livro_FK");

        builder.HasData(new
        {
            Codigo = 1,
            Titulo = "Clean Architecture",
            Editora = "Prentice Hall",
            Edicao = 1,
            AnoPublicacao = "2017"
        });
    }
}