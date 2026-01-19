using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class LivroValidator : AbstractValidator<LivroRequest>
{
    public LivroValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MaximumLength(40).WithMessage("O título deve ter no máximo 40 caracteres");

        RuleFor(x => x.Editora)
            .NotEmpty().WithMessage("A editora é obrigatória")
            .MaximumLength(40).WithMessage("O título deve ter no máximo 40 caracteres");

        RuleFor(x => x.Edicao)
            .NotEmpty().WithMessage("A edição é obrigatória")
            .GreaterThan(0).WithMessage("Edição deve ser maior que 0");

        RuleFor(x => x.AnoPublicacao)
            .NotEmpty()
            .Matches(@"^\d{4}$").WithMessage("Ano de publicação deve ter 4 indicações numéricas");

        RuleFor(x => x.AutoresCodigo)
            .NotEmpty().WithMessage("O livro deve ter pelo menos um autor");

        RuleFor(x => x.AssuntosCodigo)
            .NotEmpty().WithMessage("O livro deve ter pelo menos um assunto");
    }
}