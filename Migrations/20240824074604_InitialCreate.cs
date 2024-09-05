using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMaxDebtProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MUSTERI_TANIM_TABLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    UNVAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUSTERI_TANIM_TABLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MUSTERI_FATURA_TABLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    MUSTERI_ID = table.Column<int>(type: "int", nullable: false),
                    FATURA_TARIHI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FATURA_TUTARI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ODEME_TARIHI = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUSTERI_FATURA_TABLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MUSTERI_FATURA_TABLE_MUSTERI_TANIM_TABLE_MUSTERI_ID",
                        column: x => x.MUSTERI_ID,
                        principalTable: "MUSTERI_TANIM_TABLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_FATURA_TABLE_MUSTERI_ID",
                table: "MUSTERI_FATURA_TABLE",
                column: "MUSTERI_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MUSTERI_FATURA_TABLE");

            migrationBuilder.DropTable(
                name: "MUSTERI_TANIM_TABLE");
        }
    }
}
