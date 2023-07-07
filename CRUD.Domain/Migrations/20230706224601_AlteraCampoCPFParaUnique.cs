using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AlteraCampoCPFParaUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Pessoa",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoa",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoa");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
