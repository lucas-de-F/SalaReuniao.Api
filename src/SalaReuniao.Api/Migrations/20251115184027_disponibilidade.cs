using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class disponibilidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisponibilidadeEntity_Salas_SalaDeReuniaoId",
                table: "DisponibilidadeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisponibilidadeEntity",
                table: "DisponibilidadeEntity");

            migrationBuilder.RenameTable(
                name: "DisponibilidadeEntity",
                newName: "Disponibilidades");

            migrationBuilder.RenameIndex(
                name: "IX_DisponibilidadeEntity_SalaDeReuniaoId",
                table: "Disponibilidades",
                newName: "IX_Disponibilidades_SalaDeReuniaoId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Inicio",
                table: "Reunioes",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Fim",
                table: "Reunioes",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Data",
                table: "Reunioes",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Inicio",
                table: "Disponibilidades",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Fim",
                table: "Disponibilidades",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidades_Salas_SalaDeReuniaoId",
                table: "Disponibilidades",
                column: "SalaDeReuniaoId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidades_Salas_SalaDeReuniaoId",
                table: "Disponibilidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades");

            migrationBuilder.RenameTable(
                name: "Disponibilidades",
                newName: "DisponibilidadeEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidades_SalaDeReuniaoId",
                table: "DisponibilidadeEntity",
                newName: "IX_DisponibilidadeEntity_SalaDeReuniaoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Inicio",
                table: "Reunioes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fim",
                table: "Reunioes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Reunioes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Inicio",
                table: "DisponibilidadeEntity",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Fim",
                table: "DisponibilidadeEntity",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisponibilidadeEntity",
                table: "DisponibilidadeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisponibilidadeEntity_Salas_SalaDeReuniaoId",
                table: "DisponibilidadeEntity",
                column: "SalaDeReuniaoId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
