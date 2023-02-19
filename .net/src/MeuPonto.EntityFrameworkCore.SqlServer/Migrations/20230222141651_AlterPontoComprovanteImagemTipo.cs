using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPonto.Migrations
{
    public partial class AlterPontoComprovanteImagemTipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PontoComprovanteImagemTipos",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.UpdateData(
                table: "PontoComprovanteImagemTipos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nome",
                value: "Original");

            migrationBuilder.InsertData(
                table: "PontoComprovanteImagemTipos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Tratada" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PontoComprovanteImagemTipos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "PontoComprovanteImagemTipos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nome",
                value: "Tratada");

            migrationBuilder.InsertData(
                table: "PontoComprovanteImagemTipos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 0, "Original" });
        }
    }
}
