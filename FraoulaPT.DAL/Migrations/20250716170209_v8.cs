using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_WorkoutPrograms_WorkoutProgramId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Media_ExerciseMediaId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_CoachId",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_ExerciseMediaId",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_Media_WorkoutProgramId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "ExerciseMediaId",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "ExerciseName",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "WorkoutProgramId",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "CoachId",
                table: "WorkoutPrograms",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPrograms_CoachId",
                table: "WorkoutPrograms",
                newName: "IX_WorkoutPrograms_AppUserId");

            migrationBuilder.RenameColumn(
                name: "RepCount",
                table: "WorkoutExercises",
                newName: "RepetitionCount");

            migrationBuilder.AddColumn<string>(
                name: "CoachNote",
                table: "WorkoutPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "WorkoutPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WorkoutPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "WorkoutPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserWeeklyFormId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CoachNote",
                table: "WorkoutExercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "WorkoutExercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "WorkoutExercises",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AutoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExerciseLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ActualSetCount = table.Column<int>(type: "int", nullable: true),
                    ActualRepetitionCount = table.Column<int>(type: "int", nullable: true),
                    ActualWeight = table.Column<double>(type: "float", nullable: true),
                    UserNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AutoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExerciseLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExerciseLog_WorkoutExercises_WorkoutExerciseId",
                        column: x => x.WorkoutExerciseId,
                        principalTable: "WorkoutExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExerciseLog_WorkoutExerciseId",
                table: "WorkoutExerciseLog",
                column: "WorkoutExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercise_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercise_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "WorkoutExerciseLog");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "CoachNote",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "CoachNote",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "WorkoutPrograms",
                newName: "CoachId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutPrograms_AppUserId",
                table: "WorkoutPrograms",
                newName: "IX_WorkoutPrograms_CoachId");

            migrationBuilder.RenameColumn(
                name: "RepetitionCount",
                table: "WorkoutExercises",
                newName: "RepCount");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkoutPrograms",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkoutPrograms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationSeconds",
                table: "WorkoutExercises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseMediaId",
                table: "WorkoutExercises",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExerciseName",
                table: "WorkoutExercises",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "WorkoutExercises",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutProgramId",
                table: "Media",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseMediaId",
                table: "WorkoutExercises",
                column: "ExerciseMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_WorkoutProgramId",
                table: "Media",
                column: "WorkoutProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_WorkoutPrograms_WorkoutProgramId",
                table: "Media",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Media_ExerciseMediaId",
                table: "WorkoutExercises",
                column: "ExerciseMediaId",
                principalTable: "Media",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_CoachId",
                table: "WorkoutPrograms",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
