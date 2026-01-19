using Domain.Entities;
using Domain.Interfaces;

namespace Application;

public interface IFormaCompraAppService
{
    Task<IEnumerable<FormaCompra>> Obter();
}

public class FormaCompraAppService(IFormaCompraRepository formaCompraRepository) : IFormaCompraAppService
{
    public async Task<IEnumerable<FormaCompra>> Obter()
    {
        return await formaCompraRepository.Obter();
    }
}
