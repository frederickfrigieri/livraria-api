using Domain.Exceptions;

namespace Domain.Entities;

public class Livro
{
    public int Codigo { get; init; }
    public string Titulo { get; private set; }
    public string Editora { get; private set; }
    public int Edicao { get; private set; }
    public string AnoPublicacao { get; private set; }

    // Propriedades de navegação privadas para o mundo externo, mas acessíveis ao EF
    private readonly List<Autor> _autores = new();
    public virtual IReadOnlyCollection<Autor> Autores => _autores.AsReadOnly();

    private readonly List<Assunto> _assuntos = new();
    public virtual IReadOnlyCollection<Assunto> Assuntos => _assuntos.AsReadOnly();

    private readonly List<Preco> _precos = new();
    public virtual IReadOnlyCollection<Preco> Precos => _precos.AsReadOnly();

    // Construtor para o EF Core
    protected Livro() { }

    public Livro(string titulo, string editora, int edicao, string anoPublicacao, IEnumerable<Autor> autores, IEnumerable<Assunto> assuntos)
    {
        Titulo = titulo;
        Editora = editora;
        AnoPublicacao = anoPublicacao;
        Edicao = edicao;

        if (string.IsNullOrEmpty(Titulo))
            throw new DomainException(CodeError.LivroTituloNaoPodeSerVazio, $"A propriedade {nameof(Titulo)} não pode ser vázio.");

        if (string.IsNullOrEmpty(Editora))
            throw new DomainException(CodeError.LivroEdicaoNaoPodeSerVazio, $"A propriedade {nameof(Editora)} não pode ser vázio.");

        if (edicao <= 0)
            throw new DomainException(CodeError.LivroEdicaoNaoPodeSerVazio, $"A propriedade {nameof(Edicao)} não pode ser menor que 0.");

        if (anoPublicacao.Length != 4)
            throw new DomainException(CodeError.LivroEdicaoNaoPodeSerVazio, $"A propriedade {nameof(AnoPublicacao)}deve ser um ano com 4 dígitos.");

        foreach (var autor in autores)
            AdicionarAutor(autor);

        foreach (var assunto in assuntos)
            AdicionarAssunto(assunto);
    }

    public void AdicionarAssunto(Assunto assunto)
    {
        if (assunto == null)
            throw new DomainException(CodeError.AssuntoObrigatorio, $"O assunto é obrigatorio'.");

        if (Assuntos.Any(x => x.Descricao == assunto.Descricao))
            throw new DomainException(CodeError.AssuntoJaExiste, $"O assunto {assunto.Descricao} já existe.");

        _assuntos.Add(assunto);
    }

    public void AdicionarAutor(Autor autor)
    {
        if (autor == null)
            throw new DomainException(CodeError.AutorObrigatorio, $"O autor é obrigatório.");

        if (Autores.Any(x => x.Nome == autor.Nome))
            throw new DomainException(CodeError.AutorJaExiste, $"O autor {autor.Nome} já existe.");

        _autores.Add(autor);
    }

    public void AdicionarPreco(FormaCompra formaCompra, decimal preco)
    {
        var existe = _precos.Where(x => x.FormaCompra.Codigo == formaCompra.Codigo).FirstOrDefault();

        if (existe != null)
        {
            existe.Atualizar(preco);
        }
        else
        {
            _precos.Add(new Preco(preco, formaCompra, this));
        }
    }
}
