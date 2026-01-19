namespace Domain.Entities;

public class Assunto(string descricao)
{
    public int Codigo { get; init; }
    public string Descricao { get; private set; } = descricao;

    public virtual ICollection<Livro> Livros { get; set; } = [];
}
