using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge_MOTTU.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelasUsuarioBikeEPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RM554694");

            migrationBuilder.CreateTable(
                name: "BIKE",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MODELO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PLACA = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    ANO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DISPONIVEL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIKE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PENDING",
                schema: "RM554694",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    STATUS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    USUARIO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIKE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DATA_FIM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PENDING", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PENDING_BIKE_BIKE_ID",
                        column: x => x.BIKE_ID,
                        principalSchema: "RM554694",
                        principalTable: "BIKE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PENDING_USUARIOS_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalSchema: "RM554694",
                        principalTable: "USUARIOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PENDING_BIKE_ID",
                schema: "RM554694",
                table: "PENDING",
                column: "BIKE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PENDING_USUARIO_ID",
                schema: "RM554694",
                table: "PENDING",
                column: "USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PENDING",
                schema: "RM554694");

            migrationBuilder.DropTable(
                name: "BIKE",
                schema: "RM554694");

            migrationBuilder.DropTable(
                name: "USUARIOS",
                schema: "RM554694");
        }
    }
}
