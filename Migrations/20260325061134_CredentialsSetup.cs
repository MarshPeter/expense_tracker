using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expense_tracker.Migrations
{
    /// <inheritdoc />
    public partial class CredentialsSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    hashed_password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credentials", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_credentials_username",
                table: "credentials",
                column: "username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credentials");
        }
    }
}
