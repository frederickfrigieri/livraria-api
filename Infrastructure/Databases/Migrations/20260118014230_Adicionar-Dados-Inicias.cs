using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDadosInicias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assunto",
                columns: new[] { "CodAs", "Descricao" },
                values: new object[,]
                {
                    { 1, "Engenharia" },
                    { 2, "Arquitetura" },
                    { 3, "Design Patterns" }
                });

            migrationBuilder.InsertData(
                table: "Autor",
                columns: new[] { "CodAu", "Nome" },
                values: new object[,]
                {
                    { 1, "Robert C. Martin" },
                    { 2, "Martin Fowler" },
                    { 3, "Eric Evans" }
                });

            migrationBuilder.InsertData(
                table: "Livro",
                columns: new[] { "Cod", "AnoPublicacao", "Edicao", "Editora", "Titulo" },
                values: new object[] { 1, "2017", 1, "Prentice Hall", "Clean Architecture" });

            migrationBuilder.InsertData(
                table: "Livro_Assunto",
                columns: new[] { "Assunto_CodAs", "Livro_Cod" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Livro_Autor",
                columns: new[] { "Autor_CodAu", "Livro_Cod" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assunto",
                keyColumn: "CodAs",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assunto",
                keyColumn: "CodAs",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Autor",
                keyColumn: "CodAu",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Livro_Assunto",
                keyColumns: new[] { "Assunto_CodAs", "Livro_Cod" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Livro_Autor",
                keyColumns: new[] { "Autor_CodAu", "Livro_Cod" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Livro_Autor",
                keyColumns: new[] { "Autor_CodAu", "Livro_Cod" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Assunto",
                keyColumn: "CodAs",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autor",
                keyColumn: "CodAu",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autor",
                keyColumn: "CodAu",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Livro",
                keyColumn: "Cod",
                keyValue: 1);
        }
    }
}
