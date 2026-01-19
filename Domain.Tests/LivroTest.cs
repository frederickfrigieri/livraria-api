using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Tests
{
    public class LivroTest
    {

        private List<Autor> _autores = new()
            {
                new("autor1"),
                new("autor2"),
            };

        private List<Assunto> _assuntos = new()
            {
                new("assunto1"),
                new("assunto2"),
            };

        [Fact]
        public void Livro_CriandoLivroComTituloVazio_LancaExcecao()
        {
            // Act
            var ex = Assert.Throws<DomainException>(() => new Livro("", "editora", 1, "2014", _autores, _assuntos));

            //Assert
            Assert.Equal("A propriedade Titulo não pode ser vázio.", ex.Message);
        }

        [Fact]
        public void AdicionarAutor_AdicionandoAutorDuplicado_LancaExcecao()
        {
            // Arrange
            var livro = new Livro("titulo", "editora", 1, "2014", _autores, _assuntos);
            var nomeAutor = "autor2";

            // Act
            var ex = Assert.Throws<DomainException>(() => livro.AdicionarAutor(new Autor(nomeAutor)));

            // Assert
            Assert.Equal($"O autor {nomeAutor} já existe.", ex.Message);
        }

        [Fact]
        public void Livro_CriandoLivroComDadosValidos_CriaObjetoCorretamente()
        {
            // Arrange
            var titulo = "Titulo Valido";
            var editora = "Editora Valida";
            var edicao = 2;
            var ano = "2021";
            var autores = _autores;
            var assuntos = _assuntos;

            // Act
            var livro = new Livro(titulo, editora, edicao, ano, autores, assuntos);

            // Assert
            Assert.Equal(titulo, livro.Titulo);
            Assert.Equal(editora, livro.Editora);
            Assert.Equal(edicao, livro.Edicao);
            Assert.Equal(ano, livro.AnoPublicacao);
            Assert.Equal(2, livro.Autores.Count);
            Assert.Contains(livro.Autores, a => a.Nome == "autor1");
            Assert.Contains(livro.Assuntos, s => s.Descricao == "assunto1");
        }


    }
}
