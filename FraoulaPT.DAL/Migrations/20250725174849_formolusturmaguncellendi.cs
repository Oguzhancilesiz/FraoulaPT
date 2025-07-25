using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class formolusturmaguncellendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkoutAssignments_UserPackages_UserPackageId",
                table: "UserWorkoutAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "WorkoutExerciseLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkoutAssignments_UserPackageId",
                table: "UserWorkoutAssignments");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "WeightKg",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "UserPackageId",
                table: "UserWorkoutAssignments");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "WorkoutPrograms",
                newName: "ProgramTitle");

            migrationBuilder.RenameColumn(
                name: "RepetitionCount",
                table: "WorkoutExercises",
                newName: "Repetition");

            migrationBuilder.RenameColumn(
                name: "DayNote",
                table: "WorkoutDays",
                newName: "Description");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserWeeklyFormId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "RestDurationInSeconds",
                table: "WorkoutExercises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "WorkoutExercises",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutFeedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedbackText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualReps = table.Column<int>(type: "int", nullable: true),
                    ActualWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RPE = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AutoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutFeedbacks_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutFeedbacks_WorkoutExercises_WorkoutExerciseId",
                        column: x => x.WorkoutExerciseId,
                        principalTable: "WorkoutExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_UserWeeklyFormId",
                table: "WorkoutPrograms",
                column: "UserWeeklyFormId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutFeedbacks_AppUserId",
                table: "WorkoutFeedbacks",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutFeedbacks_WorkoutExerciseId",
                table: "WorkoutFeedbacks",
                column: "WorkoutExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_UserWeeklyForms_UserWeeklyFormId",
                table: "WorkoutPrograms",
                column: "UserWeeklyFormId",
                principalTable: "UserWeeklyForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_UserWeeklyForms_UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.DropTable(
                name: "WorkoutFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "RestDurationInSeconds",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "ProgramTitle",
                table: "WorkoutPrograms",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Repetition",
                table: "WorkoutExercises",
                newName: "RepetitionCount");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "WorkoutDays",
                newName: "DayNote");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddColumn<decimal>(
                name: "WeightKg",
                table: "WorkoutExercises",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WorkoutDays",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserPackageId",
                table: "UserWorkoutAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WorkoutExerciseLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActualRepetitionCount = table.Column<int>(type: "int", nullable: true),
                    ActualSetCount = table.Column<int>(type: "int", nullable: true),
                    ActualWeight = table.Column<double>(type: "float", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AutoID = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExerciseLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExerciseLogs_WorkoutExercises_WorkoutExerciseId",
                        column: x => x.WorkoutExerciseId,
                        principalTable: "WorkoutExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutAssignments_UserPackageId",
                table: "UserWorkoutAssignments",
                column: "UserPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExerciseLogs_WorkoutExerciseId",
                table: "WorkoutExerciseLogs",
                column: "WorkoutExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkoutAssignments_UserPackages_UserPackageId",
                table: "UserWorkoutAssignments",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
