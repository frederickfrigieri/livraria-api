namespace Domain.Entities;

public class Preco
{
    public Preco(decimal valor, FormaCompra formaCompra, Livro livro)
    {
        Valor = valor;
        FormaCompra = formaCompra;
        Livro = livro;
    }

    protected Preco() { }

    public int Codigo { get; init; }
    public decimal Valor { get; private set; }
    public FormaCompra FormaCompra { get; private set; }
    public Livro Livro { get; private set; }

    public void Atualizar(decimal preco)
    {
        Valor = preco;
    }
}
