using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsistemaGerencialBackend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoLogsAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "licenca",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "licenca",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "fazenda",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "fazenda",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "endereco_fazenda",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "endereco_fazenda",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "endereco_cliente",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "endereco_cliente",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "detalhes_pagamento",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "detalhes_pagamento",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "clientes",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "clientes",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "boletos",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "boletos",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.CreateTable(
                name: "auditoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Usuario = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Acao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tabela = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    Descricao = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditoria", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auditoria");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "licenca");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "licenca");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "fazenda");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "fazenda");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "endereco_fazenda");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "endereco_fazenda");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "endereco_cliente");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "endereco_cliente");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "detalhes_pagamento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "detalhes_pagamento");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "boletos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "boletos");
        }
    }
}
