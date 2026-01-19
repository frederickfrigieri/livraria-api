using Application.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Tests
{
    public class LivroAppService
    {
        [Fact]
        public async Task Cadastrar_QuandoCadastrarLivroComDadosInvalidos_LancaExcecao()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<LivroRequest>>();
            var failure = new ValidationFailure("Titulo", "Titulo é obrigatório");
            var validationResult = new ValidationResult([failure]);
            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<LivroRequest>(), default))
                .ReturnsAsync(validationResult);

            var livroRepoMock = new Mock<ILivroRepository>();
            var autorRepoMock = new Mock<IAutorRepository>();
            var assuntoRepoMock = new Mock<IAssuntoRepository>();
            var uowMock = new Mock<IUnitOfWork>();

            var livroAppService = new Application.LivroAppService(
                livroRepoMock.Object,
                autorRepoMock.Object,
                assuntoRepoMock.Object,
                validatorMock.Object,
                uowMock.Object
            );

            var request = new LivroRequest(
                Titulo: "",
                Editora: "",
                Edicao: 0,
                AnoPublicacao: "",
                AutoresCodigo: new int[] { },
                AssuntosCodigo: new int[] { }
            );

            // Act & Assert
            await Assert.ThrowsAsync<Exceptions.ValidationException>(() => livroAppService.Cadastrar(request));
        }

        [Fact]
        public async Task Cadastrar_QuandoDadosValidos_PersisteELancaCodigo()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<LivroRequest>>();
            var validationResult = new ValidationResult();
            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<LivroRequest>(), default))
                .ReturnsAsync(validationResult);

            // Arrange
            var autor = new Autor("Autor Teste");
            var assunto = new Assunto("Assunto Teste");

            var livroRepoMock = new Mock<ILivroRepository>();
            var autorRepoMock = new Mock<IAutorRepository>();
            var assuntoRepoMock = new Mock<IAssuntoRepository>();
            var uowMock = new Mock<IUnitOfWork>();

            autorRepoMock.Setup(a => a.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync(autor);
            assuntoRepoMock.Setup(a => a.ObterPorCodigo(It.IsAny<int>())).ReturnsAsync(assunto);

            Livro? capturedLivro = null;
            livroRepoMock.Setup(r => r.Adicionar(It.IsAny<Livro>()))
                .Callback<Livro>(l => capturedLivro = l)
                .Returns(Task.CompletedTask);

            uowMock.Setup(u => u.Save()).ReturnsAsync(true);

            var livroAppService = new Application.LivroAppService(
                livroRepoMock.Object,
                autorRepoMock.Object,
                assuntoRepoMock.Object,
                validatorMock.Object,
                uowMock.Object
            );

            var request = new LivroRequest(
                Titulo: "Um Título",
                Editora: "Uma Editora",
                Edicao: 1,
                AnoPublicacao: "2020",
                AutoresCodigo: [1],
                AssuntosCodigo: [1]
            );

            // Act
            var resultado = await livroAppService.Cadastrar(request);

            // Assert
            livroRepoMock.Verify(r => r.Adicionar(It.IsAny<Livro>()), Times.Once);
            uowMock.Verify(u => u.Save(), Times.Once);

            Assert.NotNull(capturedLivro);
            Assert.Equal(capturedLivro!.Codigo, resultado);
        }
    }
}
