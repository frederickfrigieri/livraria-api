using Domain.Entities;
using Domain.Interfaces;

namespace Application;

public record CadastrarAssuntoRequest(string Descricao);

public interface IAssuntoAppService
{
    Task<int> Cadastrar(CadastrarAssuntoRequest request);
    Task<IEnumerable<Assunto>> ObterTodos();
}

public class AssuntoAppService(IAssuntoRepository assuntoRepository, IUnitOfWork unitOfWork) : IAssuntoAppService
{
    public async Task<int> Cadastrar(CadastrarAssuntoRequest request)
    {
        var novoAssunto = new Assunto(request.Descricao);
        
        await assuntoRepository.Cadastrar(novoAssunto);
        await unitOfWork.Save();

        return novoAssunto.Codigo;
    }

    public async Task<IEnumerable<Assunto>> ObterTodos()
    {
        return await assuntoRepository.ObterTodos();
    }
}
