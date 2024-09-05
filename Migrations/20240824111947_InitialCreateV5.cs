using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMaxDebtProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MUSTERI_FATURA_TABLE_MUSTERI_TANIM_TABLE_MUSTERI_ID",
                table: "MUSTERI_FATURA_TABLE");

            migrationBuilder.DropIndex(
                name: "IX_MUSTERI_FATURA_TABLE_MUSTERI_ID",
                table: "MUSTERI_FATURA_TABLE");

            migrationBuilder.AlterColumn<string>(
                name: "UNVAN",
                table: "MUSTERI_TANIM_TABLE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "MUSTERI_TANIM_TABLE",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "MUSTERI_FATURA_TABLE",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UNVAN",
                table: "MUSTERI_TANIM_TABLE",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "MUSTERI_TANIM_TABLE",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "MUSTERI_FATURA_TABLE",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_MUSTERI_FATURA_TABLE_MUSTERI_ID",
                table: "MUSTERI_FATURA_TABLE",
                column: "MUSTERI_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MUSTERI_FATURA_TABLE_MUSTERI_TANIM_TABLE_MUSTERI_ID",
                table: "MUSTERI_FATURA_TABLE",
                column: "MUSTERI_ID",
                principalTable: "MUSTERI_TANIM_TABLE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
