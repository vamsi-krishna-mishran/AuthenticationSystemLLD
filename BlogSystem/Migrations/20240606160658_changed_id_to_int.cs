using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class changed_id_to_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    doorNo = table.Column<string>(type: "TEXT", nullable: false),
                    street = table.Column<string>(type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    state = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    zipCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: false),
                    age = table.Column<int>(type: "INTEGER", nullable: false),
                    userType = table.Column<int>(type: "INTEGER", nullable: false),
                    addressid = table.Column<int>(type: "INTEGER", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_address_addressid",
                        column: x => x.addressid,
                        principalTable: "address",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    blogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    publishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    adminIdid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.blogId);
                    table.ForeignKey(
                        name: "FK_blog_users_adminIdid",
                        column: x => x.adminIdid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rating = table.Column<int>(type: "INTEGER", nullable: false),
                    blogId = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "IX_blog_adminIdid",
                table: "blog",
                column: "adminIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_blogId",
                table: "Rating",
                column: "blogId");

            migrationBuilder.CreateIndex(
                name: "IX_users_addressid",
                table: "users",
                column: "addressid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "blog");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
