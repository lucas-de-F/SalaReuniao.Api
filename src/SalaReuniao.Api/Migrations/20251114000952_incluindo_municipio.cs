using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaReuniao.Api.Migrations
{
    /// <inheritdoc />
    public partial class incluindo_municipio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Salas""
                ALTER COLUMN ""Numero"" DROP DEFAULT;

                ALTER TABLE ""Salas""
                ALTER COLUMN ""Numero"" TYPE integer
                USING ""Numero""::integer;

                ALTER TABLE ""Salas""
                ALTER COLUMN ""Numero"" SET DEFAULT 0;
            ");
                
            migrationBuilder.AddColumn<string>(
                name: "Endereco_Municipio",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco_Municipio",
                table: "Salas");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Salas""
                ALTER COLUMN ""Numero"" DROP DEFAULT;

                ALTER TABLE ""Salas""
                ALTER COLUMN ""Numero"" TYPE text;

                ALTER TABLE ""Salas""
                ALTER COLUMN ""Numero"" SET DEFAULT '0';
            ");

        }
    }
}
