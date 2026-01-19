using Domain.Entities;
using Domain.ViewModel;

namespace Domain.Interfaces;

public interface ILivroRepository
{
    Task<IEnumerable<Livro>> ObterTodosComRelacionamentos();
    Task<IEnumerable<LivrosDetalhesViewModel>> ObterDadosView();
    Task<Livro?> ObterPorId(int codigo);
    Task Adicionar(Livro livro);
    void Atualizar(Livro livro);
    void Deletar(Livro livro);
}
