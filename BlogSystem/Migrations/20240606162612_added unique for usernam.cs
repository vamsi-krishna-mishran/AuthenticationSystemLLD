using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class addeduniqueforusernam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_userName",
                table: "users",
                column: "userName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_userName",
                table: "users");
        }
    }
}
