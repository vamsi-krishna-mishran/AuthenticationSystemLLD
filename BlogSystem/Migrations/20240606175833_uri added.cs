using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class uriadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "blogURI",
                table: "blog",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blogURI",
                table: "blog");
        }
    }
}
