using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsistemaGerencialBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaClienteContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "licenca");

            migrationBuilder.DropColumn(
                name: "AssinadoPeloPortal",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "CodigoContrato",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "DataFimTryal",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "DataInicopagamento",
                table: "clientes");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoFazenda",
                table: "fazenda",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "endereco_cliente",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClienteContratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClienteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CodigoContrato = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicopagamento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AssinadoPeloPortal = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DataFimTryal = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteContratos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteContratos");

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "licenca",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CodigoFazenda",
                table: "fazenda",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "endereco_cliente",
                keyColumn: "Bairro",
                keyValue: null,
                column: "Bairro",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "endereco_cliente",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "AssinadoPeloPortal",
                table: "clientes",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoContrato",
                table: "clientes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFimTryal",
                table: "clientes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicopagamento",
                table: "clientes",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
