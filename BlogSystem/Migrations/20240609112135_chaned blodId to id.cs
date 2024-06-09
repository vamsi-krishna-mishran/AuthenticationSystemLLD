using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class chanedblodIdtoid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_blog_blogId",
                table: "rating");

            migrationBuilder.RenameColumn(
                name: "blogId",
                table: "rating",
                newName: "blogid");

            migrationBuilder.RenameIndex(
                name: "IX_rating_blogId",
                table: "rating",
                newName: "IX_rating_blogid");

            migrationBuilder.RenameColumn(
                name: "blogId",
                table: "blog",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_blog_blogid",
                table: "rating",
                column: "blogid",
                principalTable: "blog",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_blog_blogid",
                table: "rating");

            migrationBuilder.RenameColumn(
                name: "blogid",
                table: "rating",
                newName: "blogId");

            migrationBuilder.RenameIndex(
                name: "IX_rating_blogid",
                table: "rating",
                newName: "IX_rating_blogId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "blog",
                newName: "blogId");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_blog_blogId",
                table: "rating",
                column: "blogId",
                principalTable: "blog",
                principalColumn: "blogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
