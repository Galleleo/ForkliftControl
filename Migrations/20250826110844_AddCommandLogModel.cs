using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForkliftControl.Migrations
{
    /// <inheritdoc />
    public partial class AddCommandLogModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommandLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Command = table.Column<string>(type: "TEXT", nullable: false),
                    ForkliftId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandLogs_Forklifts_ForkliftId",
                        column: x => x.ForkliftId,
                        principalTable: "Forklifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandLogs_ForkliftId",
                table: "CommandLogs",
                column: "ForkliftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandLogs");
        }
    }
}
