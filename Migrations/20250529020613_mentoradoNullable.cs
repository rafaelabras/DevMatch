using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevMatch.Migrations
{
    /// <inheritdoc />
    public partial class mentoradoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Sessions",
                newName: "DataStart");

            migrationBuilder.AlterColumn<string>(
                name: "MentoradoId",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEnd",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEnd",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "DataStart",
                table: "Sessions",
                newName: "Data");

            migrationBuilder.AlterColumn<string>(
                name: "MentoradoId",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
