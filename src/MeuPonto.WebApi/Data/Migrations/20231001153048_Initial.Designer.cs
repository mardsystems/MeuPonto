﻿// <auto-generated />
using System;
using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeuPonto.Data.Migrations
{
    [DbContext(typeof(MeuPontoDbContext))]
    [Migration("20231001153048_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeuPonto.Models.Comprovante", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("Imagem")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("PontoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("TipoImagemId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PontoId");

                    b.HasIndex("TipoImagemId");

                    b.ToTable("Comprovantes", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.Configuracoes", b =>
                {
                    b.Property<bool>("JavascriptIsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Configuracoes", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.Empregador", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Empregadores", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.Folha", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Competencia")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ContratoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.HasIndex("StatusId");

                    b.ToTable("Folhas", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.Momento", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
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

            modelBuilder.Entity("MeuPonto.Models.Pausa", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
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

            modelBuilder.Entity("MeuPonto.Models.Contrato", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("EmpregadorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("EmpregadorId");

                    b.ToTable("Contratos", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.Ponto", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

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

                    b.Property<int?>("ContratoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("MomentoId");

                    b.HasIndex("PausaId");

                    b.HasIndex("ContratoId");

                    b.ToTable("Pontos", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.StatusFolha", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("StatusFolha");

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

            modelBuilder.Entity("MeuPonto.Models.TipoImagem", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
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

            modelBuilder.Entity("MeuPonto.Models.Trabalhador", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Trabalhadores", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Models.Comprovante", b =>
                {
                    b.HasOne("MeuPonto.Models.Ponto", "Ponto")
                        .WithMany("Comprovantes")
                        .HasForeignKey("PontoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MeuPonto.Models.TipoImagem", "TipoImagem")
                        .WithMany()
                        .HasForeignKey("TipoImagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ponto");

                    b.Navigation("TipoImagem");
                });

            modelBuilder.Entity("MeuPonto.Models.Folha", b =>
                {
                    b.HasOne("MeuPonto.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MeuPonto.Models.StatusFolha", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MeuPonto.Models.ApuracaoMensal", "ApuracaoMensal", b1 =>
                        {
                            b1.Property<Guid>("FolhaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long?>("TempoTotalPeriodoAnterior")
                                .HasColumnType("bigint");

                            b1.HasKey("FolhaId");

                            b1.ToTable("Folhas");

                            b1.WithOwner()
                                .HasForeignKey("FolhaId");

                            b1.OwnsMany("MeuPonto.Models.ApuracaoDiaria", "Dias", b2 =>
                                {
                                    b2.Property<Guid>("FolhaId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int?>("Dia")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int?>("Dia"));

                                    b2.Property<long?>("DiferencaTempo")
                                        .HasColumnType("bigint");

                                    b2.Property<bool>("Falta")
                                        .HasColumnType("bit");

                                    b2.Property<bool>("Feriado")
                                        .HasColumnType("bit");

                                    b2.Property<string>("Observacao")
                                        .HasMaxLength(255)
                                        .HasColumnType("nvarchar(255)");

                                    b2.Property<long?>("TempoAbonado")
                                        .HasColumnType("bigint");

                                    b2.Property<long?>("TempoApurado")
                                        .HasColumnType("bigint");

                                    b2.Property<long>("TempoPrevisto")
                                        .HasColumnType("bigint");

                                    b2.HasKey("FolhaId", "Dia");

                                    b2.ToTable("Folhas_ApuracaoDiaria", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("FolhaId");
                                });

                            b1.Navigation("Dias");
                        });

                    b.Navigation("ApuracaoMensal")
                        .IsRequired();

                    b.Navigation("Contrato");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("MeuPonto.Models.Contrato", b =>
                {
                    b.HasOne("MeuPonto.Models.Empregador", "Empregador")
                        .WithMany()
                        .HasForeignKey("EmpregadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("MeuPonto.Models.JornadaTrabalhoSemanal", "JornadaTrabalhoSemanalPrevista", b1 =>
                        {
                            b1.Property<int>("ContratoId")
                                .HasColumnType("int");

                            b1.HasKey("ContratoId");

                            b1.ToTable("Contratos");

                            b1.WithOwner()
                                .HasForeignKey("ContratoId");

                            b1.OwnsMany("MeuPonto.Models.JornadaTrabalhoDiaria", "Semana", b2 =>
                                {
                                    b2.Property<int>("ContratoId")
                                        .HasColumnType("int");

                                    b2.Property<int>("DiaSemana")
                                        .HasColumnType("int");

                                    b2.Property<TimeSpan?>("Tempo")
                                        .IsRequired()
                                        .HasColumnType("time");

                                    b2.HasKey("ContratoId", "DiaSemana");

                                    b2.ToTable("Contratos_JornadaTrabalhoDiaria", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ContratoId");
                                });

                            b1.Navigation("Semana");
                        });

                    b.Navigation("Empregador");

                    b.Navigation("JornadaTrabalhoSemanalPrevista")
                        .IsRequired();
                });

            modelBuilder.Entity("MeuPonto.Models.Ponto", b =>
                {
                    b.HasOne("MeuPonto.Models.Momento", "Momento")
                        .WithMany()
                        .HasForeignKey("MomentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeuPonto.Models.Pausa", "Pausa")
                        .WithMany()
                        .HasForeignKey("PausaId");

                    b.HasOne("MeuPonto.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Momento");

                    b.Navigation("Pausa");

                    b.Navigation("Contrato");
                });

            modelBuilder.Entity("MeuPonto.Models.Trabalhador", b =>
                {
                    b.OwnsOne("MeuPonto.Models.CustomerSubscription", "CustomerSubscription", b1 =>
                        {
                            b1.Property<int>("TrabalhadorId")
                                .HasColumnType("int");

                            b1.Property<int>("SubscriptionPlanId")
                                .HasColumnType("int");

                            b1.HasKey("TrabalhadorId");

                            b1.ToTable("Trabalhadores");

                            b1.WithOwner()
                                .HasForeignKey("TrabalhadorId");
                        });

                    b.Navigation("CustomerSubscription");
                });

            modelBuilder.Entity("MeuPonto.Models.Ponto", b =>
                {
                    b.Navigation("Comprovantes");
                });
#pragma warning restore 612, 618
        }
    }
}
