using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraoulaPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CalorieCalculationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "WeightKg",
                table: "UserProfiles",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "HeightCm",
                table: "UserProfiles",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityLevel",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BMR",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BodyFatPercentage",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BoneMass",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyCalorieGoal",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyCarbGoal",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyFatGoal",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyProteinGoal",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MetabolicAge",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MuscleMassPercentage",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TDEE",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TargetWeight",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VisceralFat",
                table: "UserProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WaterPercentage",
                table: "UserProfiles",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityLevel",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "BMR",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "BodyFatPercentage",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "BoneMass",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DailyCalorieGoal",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DailyCarbGoal",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DailyFatGoal",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DailyProteinGoal",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MetabolicAge",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MuscleMassPercentage",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "TDEE",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "TargetWeight",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "VisceralFat",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "WaterPercentage",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<double>(
                name: "WeightKg",
                table: "UserProfiles",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HeightCm",
                table: "UserProfiles",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
