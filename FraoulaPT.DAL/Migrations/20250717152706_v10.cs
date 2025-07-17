using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMedia_ChatMessages_ChatMessageId",
                table: "ChatMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_UserWeeklyForms_UserWeeklyFormId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMedia",
                table: "ChatMedia");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Medias");

            migrationBuilder.RenameTable(
                name: "ChatMedia",
                newName: "ChatMedias");

            migrationBuilder.RenameIndex(
                name: "IX_Media_UserWeeklyFormId",
                table: "Medias",
                newName: "IX_Medias_UserWeeklyFormId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMedia_ChatMessageId",
                table: "ChatMedias",
                newName: "IX_ChatMedias_ChatMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medias",
                table: "Medias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMedias",
                table: "ChatMedias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMedias_ChatMessages_ChatMessageId",
                table: "ChatMedias",
                column: "ChatMessageId",
                principalTable: "ChatMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_UserWeeklyForms_UserWeeklyFormId",
                table: "Medias",
                column: "UserWeeklyFormId",
                principalTable: "UserWeeklyForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMedias_ChatMessages_ChatMessageId",
                table: "ChatMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_UserWeeklyForms_UserWeeklyFormId",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medias",
                table: "Medias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMedias",
                table: "ChatMedias");

            migrationBuilder.RenameTable(
                name: "Medias",
                newName: "Media");

            migrationBuilder.RenameTable(
                name: "ChatMedias",
                newName: "ChatMedia");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_UserWeeklyFormId",
                table: "Media",
                newName: "IX_Media_UserWeeklyFormId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMedias_ChatMessageId",
                table: "ChatMedia",
                newName: "IX_ChatMedia_ChatMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMedia",
                table: "ChatMedia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMedia_ChatMessages_ChatMessageId",
                table: "ChatMedia",
                column: "ChatMessageId",
                principalTable: "ChatMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_UserWeeklyForms_UserWeeklyFormId",
                table: "Media",
                column: "UserWeeklyFormId",
                principalTable: "UserWeeklyForms",
                principalColumn: "Id");
        }
    }
}
