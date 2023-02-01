﻿// <auto-generated />
using System;
using Control.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230131181236_RenameCodeInOwner")]
    partial class RenameCodeInOwner
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Control.DAL.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Measuring", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Measurings", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Nomination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Nominations", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Operation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Operations", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<string>("FullProduction")
                        .HasColumnType("TEXT")
                        .HasColumnName("FullProduction");

                    b.Property<string>("FullShop")
                        .HasColumnType("TEXT")
                        .HasColumnName("FullShop");

                    b.Property<string>("Master")
                        .HasColumnType("TEXT")
                        .HasColumnName("Master");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT")
                        .HasColumnName("Phone");

                    b.Property<string>("ShopCode")
                        .HasColumnType("TEXT")
                        .HasColumnName("ShopCode");

                    b.Property<string>("ShortProduction")
                        .HasColumnType("TEXT")
                        .HasColumnName("ShortProduction");

                    b.Property<string>("ShortShop")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("ShortShop");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Owners", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Period", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Month");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Periods", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Addition")
                        .HasColumnType("TEXT")
                        .HasColumnName("Addition");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("Created");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Type");

                    b.Property<string>("FactoryNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Number");

                    b.Property<string>("Included")
                        .HasColumnType("TEXT")
                        .HasColumnName("Included");

                    b.Property<Guid?>("MeasuringId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NextDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("NextDate");

                    b.Property<Guid?>("NominationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OperationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PeriodId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Picture")
                        .HasColumnType("TEXT")
                        .HasColumnName("Picture");

                    b.Property<DateTime>("PreviousDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("PreviousDate");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Status");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MeasuringId");

                    b.HasIndex("NominationId");

                    b.HasIndex("OperationId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PeriodId");

                    b.ToTable("Positions", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Position", b =>
                {
                    b.HasOne("Control.DAL.Models.Category", "Category")
                        .WithMany("Positions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Control.DAL.Models.Measuring", "Measuring")
                        .WithMany("Positions")
                        .HasForeignKey("MeasuringId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Control.DAL.Models.Nomination", "Nomination")
                        .WithMany("Positions")
                        .HasForeignKey("NominationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Control.DAL.Models.Operation", "Operation")
                        .WithMany("Positions")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Control.DAL.Models.Owner", "Owner")
                        .WithMany("Positions")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Control.DAL.Models.Period", "Period")
                        .WithMany("Positions")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("Measuring");

                    b.Navigation("Nomination");

                    b.Navigation("Operation");

                    b.Navigation("Owner");

                    b.Navigation("Period");
                });

            modelBuilder.Entity("Control.DAL.Models.Category", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Control.DAL.Models.Measuring", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Control.DAL.Models.Nomination", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Control.DAL.Models.Operation", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Control.DAL.Models.Owner", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Control.DAL.Models.Period", b =>
                {
                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
