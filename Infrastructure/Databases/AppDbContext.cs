using Domain.Entities;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Assunto> Assuntos { get; set; }
    public DbSet<FormaCompra> FormasCompras { get; set; }
    public DbSet<LivrosDetalhesViewModel> VwLivrosDetalhes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<LivrosDetalhesViewModel>(eb =>
        {
            eb.HasNoKey();      
            eb.ToView("VW_Livros");
        });

        modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Livro_Autor", b =>
        {
            b.HasData(
                new { Livro_Cod = 1, Autor_CodAu = 1 },
                new { Livro_Cod = 1, Autor_CodAu = 2 }
            );
        });

        modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Livro_Assunto", b =>
        {
            b.HasData(
                new { Livro_Cod = 1, Assunto_CodAs = 1 }
            );
        });

        base.OnModelCreating(modelBuilder);
    }
}