using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class FormaCompraPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forma_Compra",
                columns: table => new
                {
                    Cod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forma_Compra", x => x.Cod);
                });

            migrationBuilder.CreateTable(
                name: "Preco",
                columns: table => new
                {
                    Cod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FormaCompra_Cod = table.Column<int>(type: "int", nullable: false),
                    Livro_Cod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preco", x => x.Cod);
                    table.ForeignKey(
                        name: "Preco_FormaCompra_FK",
                        column: x => x.FormaCompra_Cod,
                        principalTable: "Forma_Compra",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Preco_Livro_FK",
                        column: x => x.Livro_Cod,
                        principalTable: "Livro",
                        principalColumn: "Cod",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Forma_Compra",
                columns: new[] { "Cod", "Descricao" },
                values: new object[,]
                {
                    { 1, "Balcao" },
                    { 2, "Self-Service" },
                    { 3, "Internet" },
                    { 4, "Evento" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preco_FormaCompra_Cod",
                table: "Preco",
                column: "FormaCompra_Cod");

            migrationBuilder.CreateIndex(
                name: "IX_Preco_Livro_Cod",
                table: "Preco",
                column: "Livro_Cod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preco");

            migrationBuilder.DropTable(
                name: "Forma_Compra");
        }
    }
}
