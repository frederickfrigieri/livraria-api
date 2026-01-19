using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.Repositories;

internal class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Livro?> ObterPorId(int codigo)
    {
        return _context.Livros
            .Include(x => x.Autores)
            .Include(x => x.Assuntos)
            .SingleOrDefaultAsync(l => l.Codigo == codigo);
    }

    public async Task<IEnumerable<Livro>> ObterTodosComRelacionamentos()
    {
        return await _context.Livros
            .Include(x => x.Autores)
            .Include(x => x.Assuntos)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Adicionar(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
    }

    public void Atualizar(Livro livro)
    {
        _context.Livros.Update(livro);
    }

    public void Deletar(Livro livro)
    {
        _context.Livros.Remove(livro);
    }

    public async Task<IEnumerable<LivrosDetalhesViewModel>> ObterDadosView()
    {
        return await _context.VwLivrosDetalhes.ToListAsync();
    }
}