using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expense_tracker.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmailAndUsernameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_credentials_username",
                table: "credentials");

            migrationBuilder.CreateIndex(
                name: "IX_credentials_email",
                table: "credentials",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_credentials_username",
                table: "credentials",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_credentials_email",
                table: "credentials");

            migrationBuilder.DropIndex(
                name: "IX_credentials_username",
                table: "credentials");

            migrationBuilder.CreateIndex(
                name: "IX_credentials_username",
                table: "credentials",
                column: "username");
        }
    }
}
