using Domain.Entities;
using Domain.Interfaces;

namespace Application;

public record CadastrarAutorRequest(string Nome);

public interface IAutorAppService
{
    Task Cadastrar(CadastrarAutorRequest request);
    Task<IEnumerable<Autor>> ObterTodos();
}

public class AutorAppService(IAutorRepository autorRepository, IUnitOfWork unitOfWork) : IAutorAppService
{
    public async Task Cadastrar(CadastrarAutorRequest request)
    {
        await autorRepository.Cadastrar(autor: new Domain.Entities.Autor(request.Nome));

        await unitOfWork.Save();
    }

    public async Task<IEnumerable<Autor>> ObterTodos()
    {
        return await autorRepository.ObterTodos();
    }
}