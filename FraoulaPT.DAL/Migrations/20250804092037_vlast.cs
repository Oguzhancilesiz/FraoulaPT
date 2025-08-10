using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class vlast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_UserPackages_UserPackageId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPackages_Packages_PackageId",
                table: "UserPackages");

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "UserQuestions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageId",
                table: "UserPackages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserPackageId",
                table: "ChatMessages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_UserPackages_UserPackageId",
                table: "ChatMessages",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPackages_Packages_PackageId",
                table: "UserPackages",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_UserPackages_UserPackageId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPackages_Packages_PackageId",
                table: "UserPackages");

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "UserQuestions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageId",
                table: "UserPackages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserPackageId",
                table: "ChatMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_UserPackages_UserPackageId",
                table: "ChatMessages",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPackages_Packages_PackageId",
                table: "UserPackages",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
