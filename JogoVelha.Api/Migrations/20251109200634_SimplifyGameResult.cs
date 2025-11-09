using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JogoVelha.Api.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyGameResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameResults");

            migrationBuilder.DropColumn(
                name: "PlayerOName",
                table: "GameResults");

            migrationBuilder.DropColumn(
                name: "PlayerXName",
                table: "GameResults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GameResults");

            migrationBuilder.DropColumn(
                name: "WinnerName",
                table: "GameResults");

            migrationBuilder.RenameColumn(
                name: "FinishedAt",
                table: "GameResults",
                newName: "DataHora");

            migrationBuilder.AddColumn<string>(
                name: "Vencedor",
                table: "GameResults",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vencedor",
                table: "GameResults");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "GameResults",
                newName: "FinishedAt");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "GameResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PlayerOName",
                table: "GameResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerXName",
                table: "GameResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GameResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WinnerName",
                table: "GameResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
