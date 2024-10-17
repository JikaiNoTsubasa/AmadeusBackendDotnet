﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ama_back_api.Database;

#nullable disable

namespace ama_back_api.Migrations
{
    [DbContext(typeof(AmaDBContext))]
    [Migration("20241017081720_rework")]
    partial class rework
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AmaCategoryAmaEntity", b =>
                {
                    b.Property<long>("CategoriesId")
                        .HasColumnType("bigint");

                    b.Property<long>("EntitiesId")
                        .HasColumnType("bigint");

                    b.HasKey("CategoriesId", "EntitiesId");

                    b.HasIndex("EntitiesId");

                    b.ToTable("AmaCategoryAmaEntity");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Entities");

                    b.HasDiscriminator().HasValue("AmaEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AvatarPath")
                        .HasColumnType("longtext");

                    b.Property<string>("DisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("Login")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaProject", b =>
                {
                    b.HasBaseType("ama_back_api.DBModels.AmaEntity");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<long>("UnitId")
                        .HasColumnType("bigint");

                    b.HasIndex("UnitId");

                    b.HasDiscriminator().HasValue("AmaProject");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaTask", b =>
                {
                    b.HasBaseType("ama_back_api.DBModels.AmaEntity");

                    b.Property<long>("ParentTaskId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.HasIndex("ParentTaskId");

                    b.HasIndex("ProjectId");

                    b.HasDiscriminator().HasValue("AmaTask");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaUnit", b =>
                {
                    b.HasBaseType("ama_back_api.DBModels.AmaEntity");

                    b.HasDiscriminator().HasValue("AmaUnit");
                });

            modelBuilder.Entity("AmaCategoryAmaEntity", b =>
                {
                    b.HasOne("ama_back_api.DBModels.AmaCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ama_back_api.DBModels.AmaEntity", null)
                        .WithMany()
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaEntity", b =>
                {
                    b.HasOne("ama_back_api.DBModels.AmaStatus", "Status")
                        .WithMany("Entities")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaProject", b =>
                {
                    b.HasOne("ama_back_api.DBModels.AmaUnit", "Unit")
                        .WithMany("Projects")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaTask", b =>
                {
                    b.HasOne("ama_back_api.DBModels.AmaTask", "ParentTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("ParentTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ama_back_api.DBModels.AmaProject", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentTask");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaStatus", b =>
                {
                    b.Navigation("Entities");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaProject", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaTask", b =>
                {
                    b.Navigation("SubTasks");
                });

            modelBuilder.Entity("ama_back_api.DBModels.AmaUnit", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
