namespace Domain.Entities;

public class FormaCompra(string descricao)
{
    public int Codigo { get; init; }
    public string Descricao { get; private set; } = descricao;
}
