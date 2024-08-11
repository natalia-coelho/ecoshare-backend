using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecoshare_backend.Migrations
{
    public partial class HerdandoUsuarioPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pessoas",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_AspNetUsers_UsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_UsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Pessoas");
        }
    }
}
