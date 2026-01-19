using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.Repositories;

internal class AutorRepository(AppDbContext context) : IAutorRepository
{
    private readonly AppDbContext _context = context;

    public void Atualizar(Autor autor)
    {
        _context.Autores.Update(autor);
    }

    public async Task Cadastrar(Autor autor)
    {
        await _context.Autores.AddAsync(autor);
    }

    public void Deletar(Autor autor)
    {
        _context.Autores.Remove(autor);
    }

    public async Task<Autor?> ObterPorCodigo(int codigo)
    {
        return await _context.Autores.SingleOrDefaultAsync(x => x.Codigo == codigo);
    }

    public async Task<IEnumerable<Autor>> ObterTodos()
    {
        return await _context.Autores
            .AsNoTracking()
            .ToListAsync();
    }
}