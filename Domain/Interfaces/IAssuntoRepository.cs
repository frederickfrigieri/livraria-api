using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssuntoRepository
{
    Task<Assunto?> ObterPorCodigo(int codigo);
    Task<IEnumerable<Assunto>> ObterTodos();
    Task Cadastrar(Assunto assunto);
    void Atualizar(Assunto assunto);
    void Deletar(Assunto assunto);
}
