﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace shiftLogger.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230605133943_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3");

            modelBuilder.Entity("Models.Logger", b =>
                {
                    b.Property<int>("loggerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Atividade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Fim")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("TEXT");

                    b.HasKey("loggerID");

                    b.HasIndex("Inicio");

                    b.ToTable("LOGGER", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}