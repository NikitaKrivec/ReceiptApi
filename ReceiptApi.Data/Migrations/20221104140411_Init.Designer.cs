﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReceiptApi.Data;

#nullable disable

namespace ReceiptApi.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221104140411_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ReceiptApi.Core.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReceiptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ReceiptApi.Core.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("ReceiptApi.Core.Models.Item", b =>
                {
                    b.HasOne("ReceiptApi.Core.Models.Receipt", null)
                        .WithMany("ReceiptItems")
                        .HasForeignKey("ReceiptId");
                });

            modelBuilder.Entity("ReceiptApi.Core.Models.Receipt", b =>
                {
                    b.Navigation("ReceiptItems");
                });
#pragma warning restore 612, 618
        }
    }
}
