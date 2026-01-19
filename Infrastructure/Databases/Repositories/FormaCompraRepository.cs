using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases.Repositories;

internal class FormaCompraRepository(AppDbContext appDbContext) : IFormaCompraRepository
{
    public async Task<IEnumerable<FormaCompra>> Obter()
    {
        return await appDbContext.FormasCompras.ToListAsync();
    }
}
