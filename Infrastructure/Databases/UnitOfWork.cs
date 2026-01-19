using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases;

internal class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task<bool> Save()
    {
        try
        {
            return await dbContext.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                    throw new RepositoryException("Este registro não pode ser excluído porque está vinculado a outros dados no sistema.", ex);

                if (sqlEx.Number == 2627)
                    throw new RepositoryException("Já existe um registro com estas informações.", ex);

                if (sqlEx.Number == 208)
                    throw new RepositoryException("Erro interno: Estrutura de dados não encontrada.", ex);
            }

            throw new RepositoryException("Erro ao persistir dados no banco de dados.", ex);
        }
    }
}
