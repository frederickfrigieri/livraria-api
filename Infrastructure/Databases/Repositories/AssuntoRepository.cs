using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.Repositories;

internal class AssuntoRepository(AppDbContext context) : IAssuntoRepository
{
    private readonly AppDbContext _context = context;

    public void Atualizar(Assunto assunto)
    {
        _context.Assuntos.Update(assunto);
    }

    public async Task Cadastrar(Assunto assunto)
    {
        _context.Assuntos.Add(assunto);
    }

    public void Deletar(Assunto assunto)
    {
        _context.Assuntos.Remove(assunto);
    }

    public Task<Assunto?> ObterPorCodigo(int codigo)
    {
        return _context.Assuntos.SingleOrDefaultAsync(x => x.Codigo == codigo);
    }

    public async Task<IEnumerable<Assunto>> ObterTodos()
    {
        return await _context.Assuntos.AsNoTracking().ToListAsync();
    }
}
