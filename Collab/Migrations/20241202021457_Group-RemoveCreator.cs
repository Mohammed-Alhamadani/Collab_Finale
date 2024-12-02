using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collab.Migrations
{
    /// <inheritdoc />
    public partial class GroupRemoveCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatorUserID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatorUserID",
                table: "Groups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorUserID",
                table: "Groups",
                column: "CreatorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatorUserID",
                table: "Groups",
                column: "CreatorUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
