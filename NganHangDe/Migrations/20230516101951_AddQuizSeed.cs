using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NganHangDe.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
