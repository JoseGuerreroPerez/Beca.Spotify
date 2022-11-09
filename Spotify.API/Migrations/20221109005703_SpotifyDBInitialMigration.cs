using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.API.Migrations
{
    public partial class SpotifyDBInitialMigration : Migration
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
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
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
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
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
                values: new object[] { 1, "The one with that big park.", "New York City" });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "The one with the cathedral that was never really finished.", "Antwerp" });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "The one with that big tower.", "Paris" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 1, "The most visited urban park in the United States.", "Central Park", 1 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 2, "A 102-story skyscraper located in Midtown Manhattan.", "Empire State Building", 1 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 3, "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.", "Cathedral", 2 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 4, "The the finest example of railway architecture in Belgium.", "Antwerp Central Station", 2 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 5, "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.", "Eiffel Tower", 3 });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "Description", "Name", "PlaylistId" },
                values: new object[] { 6, "The world's largest museum.", "The Louvre", 3 });

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
