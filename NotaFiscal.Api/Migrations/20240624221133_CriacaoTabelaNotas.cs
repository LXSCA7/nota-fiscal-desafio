using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotaFiscal.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelaNotas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                columns: table => new
                {
                    NotaFiscalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroNf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataNf = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CnpjEmissorNf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CnpjDestinatarioNf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.NotaFiscalId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasFiscais");
        }
    }
}
