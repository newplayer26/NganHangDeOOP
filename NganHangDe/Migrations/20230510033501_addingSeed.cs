using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NganHangDe.Migrations
{
    /// <inheritdoc />
    public partial class addingSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Info", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Test", "TEst", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
