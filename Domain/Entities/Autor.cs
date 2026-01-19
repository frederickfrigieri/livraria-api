namespace Domain.Entities;

public class Autor(string nome)
{
    public int Codigo { get; init; }
    public string Nome { get; private set; } = nome;

    public virtual ICollection<Livro> Livros { get; set; } = [];
}
