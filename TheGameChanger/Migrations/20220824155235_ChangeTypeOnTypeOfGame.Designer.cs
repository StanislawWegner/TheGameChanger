﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheGameChanger.Entities;

#nullable disable

namespace TheGameChanger.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20220824155235_ChangeTypeOnTypeOfGame")]
    partial class ChangeTypeOnTypeOfGame
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TheGameChanger.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("TypeOfGameId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TypeOfGameId")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TheGameChanger.Entities.TypeOfGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("TheGameChanger.Entities.Game", b =>
                {
                    b.HasOne("TheGameChanger.Entities.TypeOfGame", "TypeOfGame")
                        .WithOne("Game")
                        .HasForeignKey("TheGameChanger.Entities.Game", "TypeOfGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeOfGame");
                });

            modelBuilder.Entity("TheGameChanger.Entities.TypeOfGame", b =>
                {
                    b.Navigation("Game")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
