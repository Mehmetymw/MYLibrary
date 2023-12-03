using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYLibraryService.Migrations
{
    /// <inheritdoc />
    public partial class addBorrower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Borrower",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Borrower",
                table: "Books");
        }
    }
}
