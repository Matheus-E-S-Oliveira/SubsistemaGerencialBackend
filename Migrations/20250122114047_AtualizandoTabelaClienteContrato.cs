using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsistemaGerencialBackend.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabelaClienteContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoObjetoFazenda",
                table: "fazenda");

            migrationBuilder.AddColumn<string>(
                name: "CodigoObjetoFazenda",
                table: "ClienteContratos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoObjetoFazenda",
                table: "ClienteContratos");

            migrationBuilder.AddColumn<string>(
                name: "CodigoObjetoFazenda",
                table: "fazenda",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
