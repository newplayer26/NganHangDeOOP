using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NganHangDe.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Categories",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                   Info = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                   ParentCategoryId = table.Column<int>(type: "int", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Categories", x => x.Id);
                   table.ForeignKey(
                       name: "FK_Categories_Categories_ParentCategoryId",
                       column: x => x.ParentCategoryId,
                       principalTable: "Categories",
                       principalColumn: "Id");
               });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => new { x.QuizId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuestionId",
                table: "QuizQuestion",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Info", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Test", "TEst 1", null },
                    { 2, "Test", "TEst 2", null }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "Description", "Name", "TimeLimit" },
                values: new object[,]
                {
                    { 1, "Quiz1", "Quiz1", new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, "Quiz2", "Quiz2", new TimeSpan(0, 0, 0, 0, 0) },
                    { 3, "Quiz3", "Quiz3", new TimeSpan(0, 0, 0, 0, 0) },
                    { 4, "Quiz4", "Quiz4", new TimeSpan(0, 0, 0, 0, 0) },
                    { 5, "Quiz5", "Quiz5", new TimeSpan(0, 0, 0, 0, 0) },
                    { 6, "Quiz6", "Quiz6", new TimeSpan(0, 0, 0, 0, 0) },
                    { 7, "Quiz7", "Quiz7", new TimeSpan(0, 0, 0, 0, 0) },
                    { 8, "Quiz8", "Quiz8", new TimeSpan(0, 0, 0, 0, 0) },
                    { 9, "Quiz9", "Quiz9", new TimeSpan(0, 0, 0, 0, 0) },
                    { 10, "Quiz10", "Quiz10", new TimeSpan(0, 0, 0, 0, 0) }
                });

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
    }
}
