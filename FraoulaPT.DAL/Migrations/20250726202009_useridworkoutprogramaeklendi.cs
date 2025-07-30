using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class useridworkoutprogramaeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_UserWeeklyFormId",
                table: "WorkoutPrograms",
                column: "UserWeeklyFormId",
                unique: true);

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
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPrograms_UserWeeklyFormId",
                table: "WorkoutPrograms");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "WorkoutPrograms",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_UserWeeklyFormId",
                table: "WorkoutPrograms",
                column: "UserWeeklyFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPrograms_AspNetUsers_AppUserId",
                table: "WorkoutPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
