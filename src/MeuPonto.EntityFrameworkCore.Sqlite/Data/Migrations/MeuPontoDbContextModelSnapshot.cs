﻿// <auto-generated />
using System;
using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("MeuPonto.Modules.Configuracoes", b =>
                {
                    b.Property<bool>("JavascriptIsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.ToTable("Configuracoes", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Empregadores.Empregador", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Empregadores", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Perfis.Perfil", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EmpregadorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("EmpregadorId");

                    b.ToTable("Perfis", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Comprovantes.Comprovante", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Imagem")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Numero")
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PontoId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoImagemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("PontoId");

                    b.ToTable("Comprovantes", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Comprovantes.TipoImagem", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

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
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Competencia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PerfilId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.ToTable("Folhas", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Folhas.StatusFolha", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

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

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Momento", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

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
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

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
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataHora")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Estimado")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MomentoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("PausaId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("PerfilId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.ToTable("Pontos", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Trabalhadores.Trabalhador", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Trabalhadores", (string)null);
                });

            modelBuilder.Entity("MeuPonto.Modules.Perfis.Perfil", b =>
                {
                    b.HasOne("MeuPonto.Modules.Empregadores.Empregador", "Empregador")
                        .WithMany()
                        .HasForeignKey("EmpregadorId");

                    b.OwnsOne("MeuPonto.Modules.Perfis.JornadaTrabalhoSemanal", "JornadaTrabalhoSemanalPrevista", b1 =>
                        {
                            b1.Property<Guid>("PerfilId")
                                .HasColumnType("TEXT");

                            b1.HasKey("PerfilId");

                            b1.ToTable("Perfis");

                            b1.WithOwner()
                                .HasForeignKey("PerfilId");

                            b1.OwnsMany("MeuPonto.Modules.Perfis.JornadaTrabalhoDiaria", "Semana", b2 =>
                                {
                                    b2.Property<Guid>("PerfilId")
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("DiaSemana")
                                        .HasColumnType("INTEGER");

                                    b2.Property<TimeSpan?>("Tempo")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("PerfilId", "DiaSemana");

                                    b2.ToTable("Perfis_JornadaTrabalhoDiaria", (string)null);

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

                    b.Navigation("Ponto");
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Folhas.Folha", b =>
                {
                    b.HasOne("MeuPonto.Modules.Perfis.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MeuPonto.Modules.Pontos.Folhas.ApuracaoMensal", "ApuracaoMensal", b1 =>
                        {
                            b1.Property<Guid>("FolhaId")
                                .HasColumnType("TEXT");

                            b1.Property<TimeSpan?>("TempoTotalPeriodoAnterior")
                                .HasColumnType("TEXT");

                            b1.HasKey("FolhaId");

                            b1.ToTable("Folhas");

                            b1.WithOwner()
                                .HasForeignKey("FolhaId");

                            b1.OwnsMany("MeuPonto.Modules.Pontos.Folhas.ApuracaoDiaria", "Dias", b2 =>
                                {
                                    b2.Property<Guid>("FolhaId")
                                        .HasColumnType("TEXT");

                                    b2.Property<int?>("Dia")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("INTEGER");

                                    b2.Property<TimeSpan?>("DiferencaTempo")
                                        .HasColumnType("TEXT");

                                    b2.Property<bool>("Falta")
                                        .HasColumnType("INTEGER");

                                    b2.Property<bool>("Feriado")
                                        .HasColumnType("INTEGER");

                                    b2.Property<TimeSpan?>("TempoAbonado")
                                        .HasColumnType("TEXT");

                                    b2.Property<TimeSpan?>("TempoApurado")
                                        .HasColumnType("TEXT");

                                    b2.Property<TimeSpan?>("TempoPrevisto")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("FolhaId", "Dia");

                                    b2.ToTable("Folhas_ApuracaoDiaria", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("FolhaId");
                                });

                            b1.Navigation("Dias");
                        });

                    b.Navigation("ApuracaoMensal")
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Ponto", b =>
                {
                    b.HasOne("MeuPonto.Modules.Perfis.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("MeuPonto.Modules.Trabalhadores.Trabalhador", b =>
                {
                    b.OwnsOne("MeuPonto.Billing.CustomerSubscription", "CustomerSubscription", b1 =>
                        {
                            b1.Property<Guid>("TrabalhadorId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("SubscriptionPlanId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("TrabalhadorId");

                            b1.ToTable("Trabalhadores");

                            b1.WithOwner()
                                .HasForeignKey("TrabalhadorId");
                        });

                    b.Navigation("CustomerSubscription");
                });

            modelBuilder.Entity("MeuPonto.Modules.Pontos.Ponto", b =>
                {
                    b.Navigation("Comprovantes");
                });
#pragma warning restore 612, 618
        }
    }
}
