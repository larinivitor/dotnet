using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crescer.Spotify.Infra.Migrations
{
    public partial class InclusaoAvaliacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nota = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true),
                    MusicaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Musica_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "Musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_Id",
                table: "Avaliacao",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_MusicaId",
                table: "Avaliacao",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_UsuarioId",
                table: "Avaliacao",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");
        }
    }
}
