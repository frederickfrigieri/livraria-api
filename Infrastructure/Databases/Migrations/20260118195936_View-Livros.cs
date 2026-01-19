using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class ViewLivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW VW_Livros " +
                "as " +
                "Select " +
                "l.Cod, l.Titulo, l.Edicao, l.Editora, l.AnoPublicacao, au.Nome, au.CodAu, ass.Descricao, ass.CodAs " +
                "from Livro l " +
                "join Livro_Autor lau on lau.Livro_Cod = l.Cod " +
                "join Livro_Assunto las on las.Livro_Cod = l.Cod " +
                "join Autor au on au.CodAu = lau.Autor_CodAu " +
                "join Assunto ass on ass.CodAs = las.Assunto_CodAs ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
