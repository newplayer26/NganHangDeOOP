using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NganHangDe.Migrations
{
    /// <inheritdoc />
    public partial class seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TEst 1");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Info", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Test", "TEst 1", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TEst");
        }
    }
}
