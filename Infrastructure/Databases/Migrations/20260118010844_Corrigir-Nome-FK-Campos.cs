using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirNomeFKCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Assunto_Assunto_Assunto_CodAs",
                table: "Livro_Assunto");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Assunto_Livro_Livro_Cod",
                table: "Livro_Assunto");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_Autor_Autor_CodAu",
                table: "Livro_Autor");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_Livro_Livro_Cod",
                table: "Livro_Autor");

            migrationBuilder.DropPrimaryKey(
                name: "Cod",
                table: "Livro");

            migrationBuilder.DropPrimaryKey(
                name: "CodAu",
                table: "Autor");

            migrationBuilder.DropPrimaryKey(
                name: "CodAs",
                table: "Assunto");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Livro",
                newName: "Cod");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Autor",
                newName: "CodAu");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Assunto",
                newName: "CodAs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livro",
                table: "Livro",
                column: "Cod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autor",
                table: "Autor",
                column: "CodAu");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assunto",
                table: "Assunto",
                column: "CodAs");

            migrationBuilder.AddForeignKey(
                name: "Livro_Assunto_FKIndex1",
                table: "Livro_Assunto",
                column: "Assunto_CodAs",
                principalTable: "Assunto",
                principalColumn: "CodAs",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Livro_Assunto_FKIndex2",
                table: "Livro_Assunto",
                column: "Livro_Cod",
                principalTable: "Livro",
                principalColumn: "Cod",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Livro_Autor_FKIndex1",
                table: "Livro_Autor",
                column: "Autor_CodAu",
                principalTable: "Autor",
                principalColumn: "CodAu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Livro_Autor_FKIndex2",
                table: "Livro_Autor",
                column: "Livro_Cod",
                principalTable: "Livro",
                principalColumn: "Cod",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Livro_Assunto_FKIndex1",
                table: "Livro_Assunto");

            migrationBuilder.DropForeignKey(
                name: "Livro_Assunto_FKIndex2",
                table: "Livro_Assunto");

            migrationBuilder.DropForeignKey(
                name: "Livro_Autor_FKIndex1",
                table: "Livro_Autor");

            migrationBuilder.DropForeignKey(
                name: "Livro_Autor_FKIndex2",
                table: "Livro_Autor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Livro",
                table: "Livro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Autor",
                table: "Autor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assunto",
                table: "Assunto");

            migrationBuilder.RenameColumn(
                name: "Cod",
                table: "Livro",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "CodAu",
                table: "Autor",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "CodAs",
                table: "Assunto",
                newName: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "Cod",
                table: "Livro",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "CodAu",
                table: "Autor",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "CodAs",
                table: "Assunto",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Assunto_Assunto_Assunto_CodAs",
                table: "Livro_Assunto",
                column: "Assunto_CodAs",
                principalTable: "Assunto",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Assunto_Livro_Livro_Cod",
                table: "Livro_Assunto",
                column: "Livro_Cod",
                principalTable: "Livro",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_Autor_Autor_CodAu",
                table: "Livro_Autor",
                column: "Autor_CodAu",
                principalTable: "Autor",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_Livro_Livro_Cod",
                table: "Livro_Autor",
                column: "Livro_Cod",
                principalTable: "Livro",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
