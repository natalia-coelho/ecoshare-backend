using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecoshare_backend.Migrations
{
    public partial class AddFormacaoToPerfilPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_AspNetUsers_UsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_UsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Pessoas");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Pessoas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Formacao",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_UsuarioId",
                table: "Pessoas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_AspNetUsers_UsuarioId",
                table: "Pessoas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_AspNetUsers_UsuarioId",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_UsuarioId",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Formacao",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Enderecos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Pessoas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Pessoas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_UsuarioId1",
                table: "Pessoas",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_AspNetUsers_UsuarioId1",
                table: "Pessoas",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
