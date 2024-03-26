﻿// <auto-generated />
using System;
using Lab3.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lab3.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240320145354_ChangeRating")]
    partial class ChangeRating
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lab3.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AppointmentID"));

                    b.Property<int?>("ClientID")
                        .HasColumnType("integer");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProcedureID")
                        .HasColumnType("integer");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AppointmentID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ProcedureID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Lab3.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Lab3.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientID"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ClientID");

                    b.HasIndex("UserID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Lab3.Models.Master", b =>
                {
                    b.Property<int>("MasterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MasterID"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("AgeInCategory")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("MasterID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("Lab3.Models.Procedure", b =>
                {
                    b.Property<int>("ProcedureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProcedureID"));

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<int?>("MasterID")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int?>("ProcedureTypeID")
                        .HasColumnType("integer");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("ProcedureID");

                    b.HasIndex("MasterID");

                    b.HasIndex("ProcedureTypeID");

                    b.ToTable("Procedures");
                });

            modelBuilder.Entity("Lab3.Models.ProcedureType", b =>
                {
                    b.Property<int>("ProcedureTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProcedureTypeID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProcedureTypeID");

                    b.ToTable("ProcedureTypes");
                });

            modelBuilder.Entity("Lab3.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lab3.Models.Appointment", b =>
                {
                    b.HasOne("Lab3.Models.Client", "Client")
                        .WithMany("Appointments")
                        .HasForeignKey("ClientID");

                    b.HasOne("Lab3.Models.Procedure", "Procedure")
                        .WithMany("Appointments")
                        .HasForeignKey("ProcedureID");

                    b.Navigation("Client");

                    b.Navigation("Procedure");
                });

            modelBuilder.Entity("Lab3.Models.Client", b =>
                {
                    b.HasOne("Lab3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lab3.Models.Master", b =>
                {
                    b.HasOne("Lab3.Models.Category", "Category")
                        .WithMany("Masters")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lab3.Models.Procedure", b =>
                {
                    b.HasOne("Lab3.Models.Master", "Master")
                        .WithMany("Procedures")
                        .HasForeignKey("MasterID");

                    b.HasOne("Lab3.Models.ProcedureType", "ProcedureType")
                        .WithMany("Procedures")
                        .HasForeignKey("ProcedureTypeID");

                    b.Navigation("Master");

                    b.Navigation("ProcedureType");
                });

            modelBuilder.Entity("Lab3.Models.Category", b =>
                {
                    b.Navigation("Masters");
                });

            modelBuilder.Entity("Lab3.Models.Client", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Lab3.Models.Master", b =>
                {
                    b.Navigation("Procedures");
                });

            modelBuilder.Entity("Lab3.Models.Procedure", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Lab3.Models.ProcedureType", b =>
                {
                    b.Navigation("Procedures");
                });
#pragma warning restore 612, 618
        }
    }
}
