﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubsistemaGerencialBackend.AppDbContexts;

#nullable disable

namespace SubsistemaGerencialBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250121143504_AtualizandoTabelaClientesCpf")]
    partial class AtualizandoTabelaClientesCpf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Auditorias.Auditoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Acao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataHora")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Tabela")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("auditoria", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Boletos.Boleto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime?>("DataEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataLimitePagamento")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataVencimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NossoNumero")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<Guid>("PagamentoId")
                        .HasColumnType("char(36)");

                    b.Property<string>("SeuNumero")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<decimal?>("ValorAcrescimos")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ValorDesconto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ValorMora")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("PagamentoId")
                        .IsUnique();

                    b.ToTable("boletos", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Clientes.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool?>("AssinadoPeloPortal")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CodigoContrato")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cpf")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime?>("DataFimTryal")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataInicopagamento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("clientes", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.DeatlhesPagamentos.DetalhesPagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime?>("DataLiquidacao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FormaPagamneto")
                        .HasColumnType("int");

                    b.Property<Guid>("LicencaId")
                        .HasColumnType("char(36)");

                    b.Property<int>("StatusPagamento")
                        .HasColumnType("int");

                    b.Property<decimal?>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ValorCobrado")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("LicencaId")
                        .IsUnique();

                    b.ToTable("detalhes_pagamento", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.EnderecoClientes.EnderecoCliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cep")
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Cidade")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Complemento")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Numero")
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)");

                    b.Property<string>("Rua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Uf")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("endereco_cliente", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.EnderecoFazendas.EnderecoFazenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ComercialBairro")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ComercialCep")
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("ComercialCidade")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ComercialComplemento")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ComercialNumero")
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)");

                    b.Property<string>("ComercialRua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ComercialUf")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("FaturaBairro")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FaturaCep")
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("FaturaCidade")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("FaturaComplemento")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FaturaNumero")
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)");

                    b.Property<string>("FaturaRua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FaturaUf")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<Guid>("FazendaId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("FazendaId");

                    b.ToTable("endereco_fazenda", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Fazendas.Fazenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("char(36)");

                    b.Property<int>("CodigoFazenda")
                        .HasColumnType("int");

                    b.Property<string>("CodigoObjetoFazenda")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacaoFazenda")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("QuantidadeAnimais")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("fazenda", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Licencas.Licenca", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAtualizacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime?>("DataInico")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataVencimento")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("FaturaGerada")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Plano")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<int>("StatusLicenca")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("licenca", (string)null);
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Boletos.Boleto", b =>
                {
                    b.HasOne("SubsistemaGerencialBackend.Models.DeatlhesPagamentos.DetalhesPagamento", "DetalherPagamento")
                        .WithOne("Boleto")
                        .HasForeignKey("SubsistemaGerencialBackend.Models.Boletos.Boleto", "PagamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetalherPagamento");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.DeatlhesPagamentos.DetalhesPagamento", b =>
                {
                    b.HasOne("SubsistemaGerencialBackend.Models.Licencas.Licenca", "Licenca")
                        .WithOne("DetalherPagamento")
                        .HasForeignKey("SubsistemaGerencialBackend.Models.DeatlhesPagamentos.DetalhesPagamento", "LicencaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Licenca");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.EnderecoClientes.EnderecoCliente", b =>
                {
                    b.HasOne("SubsistemaGerencialBackend.Models.Clientes.Cliente", "Cliente")
                        .WithMany("EnderecosCliente")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.EnderecoFazendas.EnderecoFazenda", b =>
                {
                    b.HasOne("SubsistemaGerencialBackend.Models.Fazendas.Fazenda", "Fazenda")
                        .WithMany("EnderecoFazendas")
                        .HasForeignKey("FazendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fazenda");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Fazendas.Fazenda", b =>
                {
                    b.HasOne("SubsistemaGerencialBackend.Models.Clientes.Cliente", "Cliente")
                        .WithMany("Fazendas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Licencas.Licenca", b =>
                {
                    b.HasOne("SubsistemaGerencialBackend.Models.Clientes.Cliente", "Cliente")
                        .WithMany("Licenca")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Clientes.Cliente", b =>
                {
                    b.Navigation("EnderecosCliente");

                    b.Navigation("Fazendas");

                    b.Navigation("Licenca");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.DeatlhesPagamentos.DetalhesPagamento", b =>
                {
                    b.Navigation("Boleto");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Fazendas.Fazenda", b =>
                {
                    b.Navigation("EnderecoFazendas");
                });

            modelBuilder.Entity("SubsistemaGerencialBackend.Models.Licencas.Licenca", b =>
                {
                    b.Navigation("DetalherPagamento");
                });
#pragma warning restore 612, 618
        }
    }
}
