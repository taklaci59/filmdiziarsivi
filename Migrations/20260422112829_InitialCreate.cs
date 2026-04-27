using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace filmdiziarsivi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseYear = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    Views = table.Column<int>(type: "INTEGER", nullable: false),
                    TrailerUrl = table.Column<string>(type: "TEXT", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Aksiyon" },
                    { 2, "Bilim Kurgu" },
                    { 3, "Dram" },
                    { 4, "Komedi" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "Rating", "ReleaseYear", "Title", "TrailerUrl", "Views" },
                values: new object[,]
                {
                    { 1, 2, 8.6999999999999993, 1999, "Matrix", "https://www.youtube.com/embed/vKQi3bBA1y8", 1500 },
                    { 2, 2, 8.8000000000000007, 2010, "Inception", "https://www.youtube.com/embed/YoHD9XEInc0", 2000 },
                    { 3, 1, 9.0, 2008, "The Dark Knight", "https://www.youtube.com/embed/EXeTwQWrcwY", 2500 },
                    { 4, 4, 9.1999999999999993, 1975, "Hababam Sınıfı", "https://www.youtube.com/embed/rGZ4I6s3UeQ", 5000 },
                    { 5, 3, 9.3000000000000007, 1994, "Esaretin Bedeli", "https://www.youtube.com/embed/6hB3S9bIaco", 3000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
