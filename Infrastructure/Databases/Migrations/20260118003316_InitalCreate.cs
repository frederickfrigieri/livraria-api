using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assunto",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CodAs", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CodAu", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Editora = table.Column<string>(type: "varchar(40)", nullable: false),
                    Edicao = table.Column<int>(type: "int", nullable: false),
                    AnoPublicacao = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cod", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Assunto",
                columns: table => new
                {
                    Assunto_CodAs = table.Column<int>(type: "int", nullable: false),
                    Livro_Cod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Assunto", x => new { x.Assunto_CodAs, x.Livro_Cod });
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Assunto_Assunto_CodAs",
                        column: x => x.Assunto_CodAs,
                        principalTable: "Assunto",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Livro_Livro_Cod",
                        column: x => x.Livro_Cod,
                        principalTable: "Livro",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Autor",
                columns: table => new
                {
                    Autor_CodAu = table.Column<int>(type: "int", nullable: false),
                    Livro_Cod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Autor", x => new { x.Autor_CodAu, x.Livro_Cod });
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Autor_Autor_CodAu",
                        column: x => x.Autor_CodAu,
                        principalTable: "Autor",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Livro_Livro_Cod",
                        column: x => x.Livro_Cod,
                        principalTable: "Livro",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Assunto_Livro_Cod",
                table: "Livro_Assunto",
                column: "Livro_Cod");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_Livro_Cod",
                table: "Livro_Autor",
                column: "Livro_Cod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro_Assunto");

            migrationBuilder.DropTable(
                name: "Livro_Autor");

            migrationBuilder.DropTable(
                name: "Assunto");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Livro");
        }
    }
}
