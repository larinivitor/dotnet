using Microsoft.EntityFrameworkCore.Migrations;

namespace Crescer.Spotify.Infra.Migrations
{
    public partial class LoginESenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Usuario",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuario",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuario");
        }
    }
}
