using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.WebApp.Compartilhado.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBDespesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBDespesa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormaPagamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBDespesa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaDespesa",
                columns: table => new
                {
                    CategoriasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespesasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaDespesa", x => new { x.CategoriasId, x.DespesasId });
                    table.ForeignKey(
                        name: "FK_CategoriaDespesa_TBCategoria_CategoriasId",
                        column: x => x.CategoriasId,
                        principalTable: "TBCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaDespesa_TBDespesa_DespesasId",
                        column: x => x.DespesasId,
                        principalTable: "TBDespesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaDespesa_DespesasId",
                table: "CategoriaDespesa",
                column: "DespesasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaDespesa");

            migrationBuilder.DropTable(
                name: "TBDespesa");
        }
    }
}
