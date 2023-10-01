using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeuPonto.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracoes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    JavascriptIsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Empregadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empregadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Momento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Momento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pausa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pausa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusFolha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusFolha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoImagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoImagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trabalhadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerSubscription_SubscriptionPlanId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalhadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmpregadorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfis_Empregadores_EmpregadorId",
                        column: x => x.EmpregadorId,
                        principalTable: "Empregadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Folhas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Competencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ApuracaoMensal_TempoTotalPeriodoAnterior = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Perfis_JornadaTrabalhoDiaria",
                columns: table => new
                {
                    DiaSemana = table.Column<int>(type: "INTEGER", nullable: false),
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tempo = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis_JornadaTrabalhoDiaria", x => new { x.PerfilId, x.DiaSemana });
                    table.ForeignKey(
                        name: "FK_Perfis_JornadaTrabalhoDiaria_Perfis_PerfilId",
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
                    MomentoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PausaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Estimado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Folhas_ApuracaoDiaria",
                columns: table => new
                {
                    Dia = table.Column<int>(type: "INTEGER", nullable: false),
                    FolhaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TempoPrevisto = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TempoApurado = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    DiferencaTempo = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    TempoAbonado = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Feriado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Falta = table.Column<bool>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folhas_ApuracaoDiaria", x => new { x.FolhaId, x.Dia });
                    table.ForeignKey(
                        name: "FK_Folhas_ApuracaoDiaria_Folhas_FolhaId",
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
                    Imagem = table.Column<byte[]>(type: "BLOB", nullable: false),
                    TipoImagemId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Momento",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Saída" },
                    { 3, "Errado" }
                });

            migrationBuilder.InsertData(
                table: "Pausa",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Almoço" },
                    { 2, "Café/Lanche" },
                    { 3, "Banheiro" },
                    { 4, "Conversa/Reunião" },
                    { 5, "Telefonema" },
                    { 6, "Genérica" }
                });

            migrationBuilder.InsertData(
                table: "StatusFolha",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 0, "Aberta" },
                    { 1, "Fechada" }
                });

            migrationBuilder.InsertData(
                table: "TipoImagem",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Original" },
                    { 2, "Tratada" }
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
                name: "IX_Perfis_EmpregadorId",
                table: "Perfis",
                column: "EmpregadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_PerfilId",
                table: "Pontos",
                column: "PerfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comprovantes");

            migrationBuilder.DropTable(
                name: "Configuracoes");

            migrationBuilder.DropTable(
                name: "Folhas_ApuracaoDiaria");

            migrationBuilder.DropTable(
                name: "Momento");

            migrationBuilder.DropTable(
                name: "Pausa");

            migrationBuilder.DropTable(
                name: "Perfis_JornadaTrabalhoDiaria");

            migrationBuilder.DropTable(
                name: "StatusFolha");

            migrationBuilder.DropTable(
                name: "TipoImagem");

            migrationBuilder.DropTable(
                name: "Trabalhadores");

            migrationBuilder.DropTable(
                name: "Pontos");

            migrationBuilder.DropTable(
                name: "Folhas");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropTable(
                name: "Empregadores");
        }
    }
}
