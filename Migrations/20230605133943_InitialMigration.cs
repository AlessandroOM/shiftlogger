using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shiftLogger.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOGGER",
                columns: table => new
                {
                    loggerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Atividade = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGGER", x => x.loggerID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LOGGER_Inicio",
                table: "LOGGER",
                column: "Inicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGGER");
        }
    }
}
