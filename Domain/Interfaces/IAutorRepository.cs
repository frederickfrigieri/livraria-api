using Domain.Entities;

namespace Domain.Interfaces;

public interface IAutorRepository
{
    Task<Autor?> ObterPorCodigo(int codigo);
    Task<IEnumerable<Autor>> ObterTodos();
    Task Cadastrar(Autor autor);
    void Atualizar(Autor autor);
    void Deletar(Autor autor);
}
