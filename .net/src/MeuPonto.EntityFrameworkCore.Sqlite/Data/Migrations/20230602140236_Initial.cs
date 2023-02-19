using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPonto.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracoes",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    JavascriptIsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Cnpj = table.Column<string>(type: "TEXT", maxLength: 14, nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", maxLength: 36, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trabalhadores",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Pis = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Matricula = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    EmpresaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfis_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Folhas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Competencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ApuracaoMensal_TempoTotalPrevisto = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    ApuracaoMensal_TempoTotalApurado = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    ApuracaoMensal_DiferencaTempoTotal = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    ApuracaoMensal_TempoTotalPeriodoAnterior = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folhas_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JornadaTrabalhoDiaria",
                columns: table => new
                {
                    DiaSemana = table.Column<int>(type: "INTEGER", nullable: false),
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tempo = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JornadaTrabalhoDiaria", x => new { x.PerfilId, x.DiaSemana });
                    table.ForeignKey(
                        name: "FK_JornadaTrabalhoDiaria_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Momento = table.Column<int>(type: "INTEGER", nullable: false),
                    Pausa = table.Column<string>(type: "TEXT", nullable: true),
                    Estimado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
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
                name: "ApuracaoDiaria",
                columns: table => new
                {
                    Dia = table.Column<int>(type: "INTEGER", nullable: false),
                    FolhaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TempoPrevisto = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TempoApurado = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    DiferencaTempo = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    TempoAbonado = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Feriado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Falta = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApuracaoDiaria", x => new { x.FolhaId, x.Dia });
                    table.ForeignKey(
                        name: "FK_ApuracaoDiaria_Folhas_FolhaId",
                        column: x => x.FolhaId,
                        principalTable: "Folhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comprovantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PontoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Numero = table.Column<string>(type: "TEXT", maxLength: 16, nullable: true),
                    Imagem = table.Column<byte[]>(type: "BLOB", nullable: false),
                    TipoImagem = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comprovantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comprovantes_Pontos_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Pontos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comprovantes_PontoId",
                table: "Comprovantes",
                column: "PontoId");

            migrationBuilder.CreateIndex(
                name: "IX_Folhas_PerfilId",
                table: "Folhas",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_EmpresaId",
                table: "Perfis",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_PerfilId",
                table: "Pontos",
                column: "PerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApuracaoDiaria");

            migrationBuilder.DropTable(
                name: "Comprovantes");

            migrationBuilder.DropTable(
                name: "Configuracoes");

            migrationBuilder.DropTable(
                name: "JornadaTrabalhoDiaria");

            migrationBuilder.DropTable(
                name: "Trabalhadores");

            migrationBuilder.DropTable(
                name: "Folhas");

            migrationBuilder.DropTable(
                name: "Pontos");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
