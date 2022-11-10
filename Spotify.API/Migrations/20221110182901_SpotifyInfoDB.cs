using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.API.Migrations
{
    public partial class SpotifyInfoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    PlaylistId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canciones_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Canciones para escuchar en el coche.", "lista coche" });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "lista de canciones de piano para concentrarse.", "Concentración" });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Lista de 50 canciones más escuchadas.", "Más escuchadas" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 1, "Queen.", "I want it all", 1 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 2, "Estopa.", "Vino tinto", 1 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 3, "choppin.", "piano1", 2 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 4, "choppin.", "piano2", 2 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 5, "Sebastián Yatra.", "Tacones rojos", 3 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 6, "Rosalia.", "La fama", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Canciones_PlaylistId",
                table: "Canciones",
                column: "PlaylistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canciones");

            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
