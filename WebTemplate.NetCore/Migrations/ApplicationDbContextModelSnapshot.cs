﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebTemplate.NetCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Core.Domain.Entities.Conexion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BaseDatos")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Puerto")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Servidor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Conexiones");
                });

            modelBuilder.Entity("Core.Domain.Entities.Salvas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ConexionId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConexionId");

                    b.ToTable("Salvas");
                });

            modelBuilder.Entity("Core.Domain.Entities.Salvas", b =>
                {
                    b.HasOne("Core.Domain.Entities.Conexion", "Conexion")
                        .WithMany("Salvas")
                        .HasForeignKey("ConexionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conexion");
                });

            modelBuilder.Entity("Core.Domain.Entities.Conexion", b =>
                {
                    b.Navigation("Salvas");
                });
#pragma warning restore 612, 618
        }
    }
}
