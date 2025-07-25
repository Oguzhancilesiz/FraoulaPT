using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class userforguncellendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeeklyForms_UserPackages_UserPackageId",
                table: "UserWeeklyForms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "CoachNote",
                table: "WorkoutExercises",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "UserPackageId",
                table: "UserWeeklyForms",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeeklyForms_UserPackageId",
                table: "UserWeeklyForms",
                newName: "IX_UserWeeklyForms_AppUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "DayNote",
                table: "WorkoutDays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeeklyForms_AspNetUsers_AppUserId",
                table: "UserWeeklyForms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeeklyForms_AspNetUsers_AppUserId",
                table: "UserWeeklyForms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropColumn(
                name: "WeightKg",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "DayNote",
                table: "WorkoutDays");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "WorkoutExercises",
                newName: "CoachNote");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserWeeklyForms",
                newName: "UserPackageId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeeklyForms_AppUserId",
                table: "UserWeeklyForms",
                newName: "IX_UserWeeklyForms_UserPackageId");

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

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "WorkoutExercises",
                type: "float",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeeklyForms_UserPackages_UserPackageId",
                table: "UserWeeklyForms",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
