using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class egzersizekategorieklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExerciseCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AutoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseCategory_CategoryId",
                table: "Exercises",
                column: "CategoryId",
                principalTable: "ExerciseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseCategory_CategoryId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "ExerciseCategory");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Exercises");
        }
    }
}
