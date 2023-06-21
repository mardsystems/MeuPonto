﻿// <auto-generated />
using System;
using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeuPonto.Data.Migrations
{
    [DbContext(typeof(MeuPontoDbContext))]
    partial class MeuPontoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MeuPonto.Modules.ConfiguracaoPorUsuario", b =>
                {
                    b.Property<bool>("JavascriptIsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Configuracoes", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Perfis.Empregadores.Empregador", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("InscricaoEstadual")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Empregadores", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Perfis.Perfil", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("EmpregadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Matricula")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("EmpregadorId");

                    b.ToTable("Perfis", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Comprovantes.Comprovante", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("Imagem")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Numero")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<Guid?>("PontoId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoImagemId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PontoId");

                    b.HasIndex("TipoImagemId");

                    b.ToTable("Comprovantes", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Comprovantes.TipoImagem", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TipoImagem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Original"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Tratada"
                        });
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Folhas.Folha", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Competencia")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("PerfilId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.HasIndex("StatusId");

                    b.ToTable("Folhas", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Folhas.Status", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Nome = "Aberta"
                        },
                        new
                        {
                            Id = 1,
                            Nome = "Fechada"
                        });
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Momento", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Momento");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Entrada"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Saída"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Errado"
                        });
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Pausa", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Pausa");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Almoço"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Café/Lanche"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Banheiro"
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Conversa/Reunião"
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Telefonema"
                        },
                        new
                        {
                            Id = 6,
                            Nome = "Genérica"
                        });
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Ponto", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DataHora")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<bool>("Estimado")
                        .HasColumnType("bit");

                    b.Property<int>("MomentoId")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("PausaId")
                        .HasColumnType("int");

                    b.Property<Guid?>("PerfilId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("MomentoId");

                    b.HasIndex("PausaId");

                    b.HasIndex("PerfilId");

                    b.ToTable("Pontos", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Trabalhador", b =>
                {
                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Pis")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Trabalhadores", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Perfis.Perfil", b =>
                {
                    b.HasOne("MeuPonto.Modules.Perfis.Empregadores.Empregador", "Empregador")
                        .WithMany()
                        .HasForeignKey("EmpregadorId");

                    b.OwnsOne("MeuPonto.Modules.Perfis.JornadaTrabalhoSemanal", "JornadaTrabalhoSemanalPrevista", b1 =>
                        {
                            b1.Property<Guid>("PerfilId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("PerfilId");

                            b1.ToTable("Perfis");

                            b1.WithOwner()
                                .HasForeignKey("PerfilId");

                            b1.OwnsMany("MeuPonto.Modules.Perfis.JornadaTrabalhoDiaria", "Semana", b2 =>
                                {
                                    b2.Property<Guid>("PerfilId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("DiaSemana")
                                        .HasColumnType("int");

                                    b2.Property<TimeSpan?>("Tempo")
                                        .IsRequired()
                                        .HasColumnType("time");

                                    b2.HasKey("PerfilId", "DiaSemana");

                                    b2.ToTable("JornadaTrabalhoDiaria");

                                    b2.WithOwner()
                                        .HasForeignKey("PerfilId");
                                });

                            b1.Navigation("Semana");
                        });

                    b.Navigation("Empregador");

                    b.Navigation("JornadaTrabalhoSemanalPrevista")
                        .IsRequired();
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Comprovantes.Comprovante", b =>
                {
                    b.HasOne("MeuPonto.Modules.Pontos.Ponto", "Ponto")
                        .WithMany("Comprovantes")
                        .HasForeignKey("PontoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeuPonto.Modules.Pontos.Comprovantes.TipoImagem", "TipoImagem")
                        .WithMany()
                        .HasForeignKey("TipoImagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ponto");

                    b.Navigation("TipoImagem");
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Folhas.Folha", b =>
                {
                    b.HasOne("MeuPonto.Modules.Perfis.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeuPonto.Modules.Pontos.Folhas.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MeuPonto.Modules.Pontos.Folhas.ApuracaoMensal", "ApuracaoMensal", b1 =>
                        {
                            b1.Property<Guid>("FolhaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long?>("DiferencaTempoTotal")
                                .HasColumnType("bigint");

                            b1.Property<long?>("TempoTotalApurado")
                                .HasColumnType("bigint");

                            b1.Property<long?>("TempoTotalPeriodoAnterior")
                                .HasColumnType("bigint");

                            b1.Property<long?>("TempoTotalPrevisto")
                                .HasColumnType("bigint");

                            b1.HasKey("FolhaId");

                            b1.ToTable("Folhas");

                            b1.WithOwner()
                                .HasForeignKey("FolhaId");

                            b1.OwnsMany("MeuPonto.Modules.Pontos.Folhas.ApuracaoDiaria", "Dias", b2 =>
                                {
                                    b2.Property<Guid>("FolhaId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int?>("Dia")
                                        .HasColumnType("int");

                                    b2.Property<long?>("DiferencaTempo")
                                        .HasColumnType("bigint");

                                    b2.Property<bool>("Falta")
                                        .HasColumnType("bit");

                                    b2.Property<bool>("Feriado")
                                        .HasColumnType("bit");

                                    b2.Property<long?>("TempoAbonado")
                                        .HasColumnType("bigint");

                                    b2.Property<long?>("TempoApurado")
                                        .HasColumnType("bigint");

                                    b2.Property<long>("TempoPrevisto")
                                        .HasColumnType("bigint");

                                    b2.HasKey("FolhaId", "Dia");

                                    b2.ToTable("ApuracaoDiaria");

                                    b2.WithOwner()
                                        .HasForeignKey("FolhaId");
                                });

                            b1.Navigation("Dias");
                        });

                    b.Navigation("ApuracaoMensal")
                        .IsRequired();

                    b.Navigation("Perfil");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Ponto", b =>
                {
                    b.HasOne("MeuPonto.Modules.Pontos.Momento", "Momento")
                        .WithMany()
                        .HasForeignKey("MomentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeuPonto.Modules.Pontos.Pausa", "Pausa")
                        .WithMany()
                        .HasForeignKey("PausaId");

                    b.HasOne("MeuPonto.Modules.Perfis.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Momento");

                    b.Navigation("Pausa");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Ponto", b =>
                {
                    b.Navigation("Comprovantes");
                });
#pragma warning restore 612, 618
        }
    }
}
