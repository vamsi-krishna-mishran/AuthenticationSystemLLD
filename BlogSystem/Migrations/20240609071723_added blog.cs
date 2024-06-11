using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class addedblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "blogURI",
                table: "blog",
                newName: "blogThumbnailImg");

            migrationBuilder.AddColumn<string>(
                name: "blogDocument",
                table: "blog",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "blogHeading",
                table: "blog",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blogDocument",
                table: "blog");

            migrationBuilder.DropColumn(
                name: "blogHeading",
                table: "blog");

            migrationBuilder.RenameColumn(
                name: "blogThumbnailImg",
                table: "blog",
                newName: "blogURI");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    blogId = table.Column<int>(type: "INTEGER", nullable: true),
                    rating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rating_blog_blogId",
                        column: x => x.blogId,
                        principalTable: "blog",
                        principalColumn: "blogId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_blogId",
                table: "Rating",
                column: "blogId");
        }
    }
}
