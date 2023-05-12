﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Models;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20230510010620_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webapi.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("DepartmentStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("DepartmentId")
                        .HasName("PK__Departme__B2079BEDDC916E7A");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("EmployeeId")
                        .HasName("PK__Employee__7AD04F1179BD2F56");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Information", b =>
                {
                    b.Property<int>("InformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("InformationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InformationId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("InformationAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("InformationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool>("InformationStatus")
                        .HasColumnType("bit");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    b.HasKey("InformationId")
                        .HasName("PK__Informat__C93C35D00999FA45");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("MaterialId");

                    b.ToTable("Information");
                });

            modelBuilder.Entity("webapi.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaterialID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"));

                    b.Property<int>("MaterialAmount")
                        .HasColumnType("int");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("MaterialStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("MaterialTotal")
                        .HasColumnType("int");

                    b.HasKey("MaterialId")
                        .HasName("PK__Material__C506131762499DF3");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Employee", b =>
                {
                    b.HasOne("webapi.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .IsRequired()
                        .HasConstraintName("FK__Employee__Depart__3A81B327");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("webapi.Models.Information", b =>
                {
                    b.HasOne("webapi.Models.Employee", "Employee")
                        .WithMany("Information")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK__Informati__Emplo__4222D4EF");

                    b.HasOne("webapi.Models.Material", "Material")
                        .WithMany("Information")
                        .HasForeignKey("MaterialId")
                        .IsRequired()
                        .HasConstraintName("FK__Informati__Mater__412EB0B6");

                    b.Navigation("Employee");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("webapi.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("webapi.Models.Employee", b =>
                {
                    b.Navigation("Information");
                });

            modelBuilder.Entity("webapi.Models.Material", b =>
                {
                    b.Navigation("Information");
                });
#pragma warning restore 612, 618
        }
    }
}
