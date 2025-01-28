using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsistemaGerencialBackend.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabelaFazenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FazendaId",
                table: "ClienteContratos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteContratos_FazendaId",
                table: "ClienteContratos",
                column: "FazendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteContratos_fazenda_FazendaId",
                table: "ClienteContratos",
                column: "FazendaId",
                principalTable: "fazenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienteContratos_fazenda_FazendaId",
                table: "ClienteContratos");

            migrationBuilder.DropIndex(
                name: "IX_ClienteContratos_FazendaId",
                table: "ClienteContratos");

            migrationBuilder.DropColumn(
                name: "FazendaId",
                table: "ClienteContratos");
        }
    }
}
