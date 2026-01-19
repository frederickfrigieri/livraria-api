using Domain.Entities;

namespace Domain.Interfaces;

public interface IFormaCompraRepository
{
    Task<IEnumerable<FormaCompra>> Obter();
}
