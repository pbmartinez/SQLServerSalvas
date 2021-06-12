using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTemplate.NetCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conexiones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Servidor = table.Column<string>(type: "TEXT", nullable: false),
                    Puerto = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseDatos = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conexiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salvas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConexionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salvas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salvas_Conexiones_ConexionId",
                        column: x => x.ConexionId,
                        principalTable: "Conexiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salvas_ConexionId",
                table: "Salvas",
                column: "ConexionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salvas");

            migrationBuilder.DropTable(
                name: "Conexiones");
        }
    }
}
