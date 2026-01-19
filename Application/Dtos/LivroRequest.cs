namespace Application.Dtos;

public record LivroRequest(
    string Titulo,
    string Editora,
    int Edicao,
    string AnoPublicacao,
    int[] AutoresCodigo,
    int[] AssuntosCodigo);
