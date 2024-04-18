using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                columns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                values: new object[] { 3, 5 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { 1, "email@email.com", "password123!@#" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuizId",
                table: "UserAnswers",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Quizzes_QuizId",
                table: "UserAnswers",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Quizzes_QuizId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuizId",
                table: "UserAnswers");

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                columns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                values: new object[] { 1, 5 });
        }
    }
}
