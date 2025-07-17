using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExerciseLog_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutExerciseLog");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercise_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExerciseLog",
                table: "WorkoutExerciseLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "WorkoutExerciseLog",
                newName: "WorkoutExerciseLogs");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExerciseLog_WorkoutExerciseId",
                table: "WorkoutExerciseLogs",
                newName: "IX_WorkoutExerciseLogs_WorkoutExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExerciseLogs",
                table: "WorkoutExerciseLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExerciseLogs_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutExerciseLogs",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExerciseLogs_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutExerciseLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExerciseLogs",
                table: "WorkoutExerciseLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "WorkoutExerciseLogs",
                newName: "WorkoutExerciseLog");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExerciseLogs_WorkoutExerciseId",
                table: "WorkoutExerciseLog",
                newName: "IX_WorkoutExerciseLog_WorkoutExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExerciseLog",
                table: "WorkoutExerciseLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExerciseLog_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutExerciseLog",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercise_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
