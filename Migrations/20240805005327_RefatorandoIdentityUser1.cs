using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecoshare_backend.Migrations
{
    public partial class RefatorandoIdentityUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_AspNetUsers_UsuarioId1",
                table: "Fornecedores");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_UsuarioId1",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Fornecedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Fornecedores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_UsuarioId1",
                table: "Fornecedores",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_AspNetUsers_UsuarioId1",
                table: "Fornecedores",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
