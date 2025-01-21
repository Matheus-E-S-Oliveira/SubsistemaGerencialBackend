using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsistemaGerencialBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionamnetoClinteFromClienteContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClienteContratos_ClienteId",
                table: "ClienteContratos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteContratos_clientes_ClienteId",
                table: "ClienteContratos",
                column: "ClienteId",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteContratos_clientes_ClienteId",
                table: "ClienteContratos");

            migrationBuilder.DropIndex(
                name: "IX_ClienteContratos_ClienteId",
                table: "ClienteContratos");
        }
    }
}
