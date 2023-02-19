using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPonto.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Pis = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Empresa_Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Empresa_Nome = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Empresa_InscricaoEstadual = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Empresa_Endereco = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PontoComprovanteImagemTipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoComprovanteImagemTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerfilId = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontos_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PontoComprovantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PontoId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImagemTipoId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoComprovantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontoComprovantes_PontoComprovanteImagemTipos_ImagemTipoId",
                        column: x => x.ImagemTipoId,
                        principalTable: "PontoComprovanteImagemTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontoComprovantes_Pontos_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Pontos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PontoComprovanteImagemTipos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 0, "Original" });

            migrationBuilder.InsertData(
                table: "PontoComprovanteImagemTipos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Tratada" });

            migrationBuilder.CreateIndex(
                name: "IX_PontoComprovantes_ImagemTipoId",
                table: "PontoComprovantes",
                column: "ImagemTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoComprovantes_PontoId",
                table: "PontoComprovantes",
                column: "PontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_PerfilId",
                table: "Pontos",
                column: "PerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontoComprovantes");

            migrationBuilder.DropTable(
                name: "PontoComprovanteImagemTipos");

            migrationBuilder.DropTable(
                name: "Pontos");

            migrationBuilder.DropTable(
                name: "Perfis");
        }
    }
}
