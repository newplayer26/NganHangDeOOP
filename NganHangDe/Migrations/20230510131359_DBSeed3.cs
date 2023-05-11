using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NganHangDe.Migrations
{
    /// <inheritdoc />
    public partial class DBSeed3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "TEst 2");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Info", "Name", "ParentCategoryId" },
                values: new object[] { 3, "Test", "TEst 3", 1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Name", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Question 1", "Question Text 1" },
                    { 2, 1, "Question 2", "Question Text 2" },
                    { 3, 1, "Question 3", "Question Text 3" },
                    { 4, 1, "Question 4", "Question Text 4" },
                    { 5, 2, "Question 5", "Question Text 5" },
                    { 6, 2, "Question 6", "Question Text 6" },
                    { 7, 2, "Question 7", "Question Text 7" },
                    { 8, 2, "Question 8", "Question Text 8" },
                    { 9, 2, "Question 9", "Question Text 9" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Grade", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 4, 1.0, 1, "Answer Text 1 - 0" },
                    { 5, 0.0, 1, "Answer Text 1 - 1" },
                    { 6, 0.0, 1, "Answer Text 1 - 2" },
                    { 7, 0.0, 1, "Answer Text 1 - 3" },
                    { 8, 1.0, 2, "Answer Text 2 - 0" },
                    { 9, 0.0, 2, "Answer Text 2 - 1" },
                    { 10, 0.0, 2, "Answer Text 2 - 2" },
                    { 11, 0.0, 2, "Answer Text 2 - 3" },
                    { 12, 1.0, 3, "Answer Text 3 - 0" },
                    { 13, 0.0, 3, "Answer Text 3 - 1" },
                    { 14, 0.0, 3, "Answer Text 3 - 2" },
                    { 15, 0.0, 3, "Answer Text 3 - 3" },
                    { 16, 1.0, 4, "Answer Text 4 - 0" },
                    { 17, 0.0, 4, "Answer Text 4 - 1" },
                    { 18, 0.0, 4, "Answer Text 4 - 2" },
                    { 19, 0.0, 4, "Answer Text 4 - 3" },
                    { 20, 1.0, 5, "Answer Text 5 - 0" },
                    { 21, 0.0, 5, "Answer Text 5 - 1" },
                    { 22, 0.0, 5, "Answer Text 5 - 2" },
                    { 23, 0.0, 5, "Answer Text 5 - 3" },
                    { 24, 1.0, 6, "Answer Text 6 - 0" },
                    { 25, 0.0, 6, "Answer Text 6 - 1" },
                    { 26, 0.0, 6, "Answer Text 6 - 2" },
                    { 27, 0.0, 6, "Answer Text 6 - 3" },
                    { 28, 1.0, 7, "Answer Text 7 - 0" },
                    { 29, 0.0, 7, "Answer Text 7 - 1" },
                    { 30, 0.0, 7, "Answer Text 7 - 2" },
                    { 31, 0.0, 7, "Answer Text 7 - 3" },
                    { 32, 1.0, 8, "Answer Text 8 - 0" },
                    { 33, 0.0, 8, "Answer Text 8 - 1" },
                    { 34, 0.0, 8, "Answer Text 8 - 2" },
                    { 35, 0.0, 8, "Answer Text 8 - 3" },
                    { 36, 1.0, 9, "Answer Text 9 - 0" },
                    { 37, 0.0, 9, "Answer Text 9 - 1" },
                    { 38, 0.0, 9, "Answer Text 9 - 2" },
                    { 39, 0.0, 9, "Answer Text 9 - 3" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Name", "Text" },
                values: new object[] { 10, 3, "Question 10", "Question Text 10" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Grade", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 40, 1.0, 10, "Answer Text 10 - 0" },
                    { 41, 0.0, 10, "Answer Text 10 - 1" },
                    { 42, 0.0, 10, "Answer Text 10 - 2" },
                    { 43, 0.0, 10, "Answer Text 10 - 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "TEst 1");
        }
    }
}
