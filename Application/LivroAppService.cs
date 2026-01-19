using Application.Dtos;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;


namespace Application;

public interface ILivroAppService
{
    Task<int> Cadastrar(LivroRequest request);
    Task<Livro?> ObterPorCodigo(int codigo);
    Task<IEnumerable<Livro>> ObterTodos();
    Task<IEnumerable<RelatorioAutorViewModel>> ObterRelatorio();
    Task Apagar(int codigo);
}

public class LivroAppService : ILivroAppService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IAutorRepository _autorRepository;
    private readonly IAssuntoRepository _assuntoRepository;
    private readonly IValidator<LivroRequest> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public LivroAppService(
        ILivroRepository livroRepository,
        IAutorRepository autorRepository,
        IAssuntoRepository assuntoRepository,
        IValidator<LivroRequest> validator,
        IUnitOfWork unitOfWork)
    {
        _livroRepository = livroRepository;
        _autorRepository = autorRepository;
        _assuntoRepository = assuntoRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task Apagar(int codigo)
    {
        var livro = await _livroRepository.ObterPorId(codigo) 
            ?? throw new Exceptions.ApplicationException(CodeError.LivroNaoEncontrado, "Livro não encontrado");
        
        _livroRepository.Deletar(livro);

        await _unitOfWork.Save();
    }

    public async Task<int> Cadastrar(LivroRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new Exceptions.ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

        var autores = new List<Autor>();

        foreach (var codigo in request.AutoresCodigo)
        {
            var autorEntity = await _autorRepository.ObterPorCodigo(codigo)
                ?? throw new Exceptions.ApplicationException(CodeError.AutorNaoEncontrado, $"Autor codigo: {codigo} não foi encontrado");

            autores.Add(autorEntity);
        }

        var assuntos = new List<Assunto>();

        foreach (var codigo in request.AssuntosCodigo)
        {
            var assuntoEntity = await _assuntoRepository.ObterPorCodigo(codigo)
                ?? throw new Exceptions.ApplicationException(CodeError.AutorNaoEncontrado, $"Assunto codigo: {codigo} não foi encontrado");

            assuntos.Add(assuntoEntity);
        }

        var novoLivro = new Livro(request.Titulo, request.Editora, request.Edicao, request.AnoPublicacao, autores, assuntos);

        // 5. Persistência via Unit of Work
        await _livroRepository.Adicionar(novoLivro);

        await _unitOfWork.Save();

        return novoLivro.Codigo;
    }

    public async Task<Livro?> ObterPorCodigo(int codigo)
    {
        return await _livroRepository.ObterPorId(codigo);
    }

    public async Task<IEnumerable<RelatorioAutorViewModel>> ObterRelatorio()
    {
        var dados = await _livroRepository.ObterDadosView();

        var relatorio = dados
            .GroupBy(d => new { d.CodAu, d.Nome })
            .Select(grupo => new RelatorioAutorViewModel
            {
                Codigo = grupo.Key.CodAu,
                Autor = grupo.Key.Nome,
                Livros = grupo.Select(l => new RelatorioLivrosViewModel
                {
                    Codigo = l.Cod,
                    Titulo = l.Titulo,
                    AnoPublicacao = l.AnoPublicacao.ToString(),
                    Editora = l.Editora,
                    Edicao = l.Edicao.ToString(),
                    Assuntos = l.Descricao
                }).ToList()
            })
            .OrderBy(a => a.Autor)
            .ToList();

        return relatorio;
    }

    public async Task<IEnumerable<Livro>> ObterTodos()
    {
        return await _livroRepository.ObterTodosComRelacionamentos();
    }
}

public class RelatorioAutorViewModel
{
    public string Autor { get; set; }
    public int Codigo { get; set; }
    public IEnumerable<RelatorioLivrosViewModel> Livros { get; set; }
}

public class RelatorioLivrosViewModel
{
    public int Codigo { get; set; }
    public string Titulo { get; set; }
    public string AnoPublicacao { get; set; }
    public string Editora { get; set; }
    public string Edicao { get; set; }
    public string Assuntos { get; set; }
}
