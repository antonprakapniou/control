﻿// <auto-generated />
using System;
using Control.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Control.DAL.Models.Measuring", b =>
                {
                    b.Property<Guid>("MeasuringId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("Code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("MeasuringId")
                        .HasName("MeasuringId");

                    b.ToTable("Measurings", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Nomination", b =>
                {
                    b.Property<Guid>("NominationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("NominationId")
                        .HasName("NominationId");

                    b.ToTable("Nominations", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Operation", b =>
                {
                    b.Property<Guid>("OperationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("OperationId")
                        .HasName("OperationId");

                    b.ToTable("Operations", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Owner", b =>
                {
                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("Email");

                    b.Property<string>("Master")
                        .HasColumnType("text")
                        .HasColumnName("Master");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("Phone");

                    b.Property<string>("Production")
                        .HasColumnType("text")
                        .HasColumnName("Production");

                    b.Property<string>("Shop")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Shop");

                    b.HasKey("OwnerId")
                        .HasName("OwnerId");

                    b.ToTable("Owners", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Period", b =>
                {
                    b.Property<Guid>("PeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("Month")
                        .HasColumnType("integer")
                        .HasColumnName("Month");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("PeriodId")
                        .HasName("PeriodId");

                    b.ToTable("Periods", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Position", b =>
                {
                    b.Property<Guid>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Addition")
                        .HasColumnType("text")
                        .HasColumnName("Addition");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Created");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("Included")
                        .HasColumnType("text")
                        .HasColumnName("Included");

                    b.Property<Guid?>("MeasuringId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<DateTime>("Next")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Next");

                    b.Property<Guid?>("NominationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OperationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PeriodId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Previous")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Previous");

                    b.Property<Guid?>("StatusId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UnitsId")
                        .HasColumnType("uuid");

                    b.HasKey("PositionId")
                        .HasName("PositionId");

                    b.HasIndex("MeasuringId");

                    b.HasIndex("NominationId");

                    b.HasIndex("OperationId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UnitsId");

                    b.ToTable("Positions", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Status", b =>
                {
                    b.Property<Guid>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("StatusId")
                        .HasName("StatusId");

                    b.ToTable("Statuses", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Units", b =>
                {
                    b.Property<Guid>("UnitsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("UnitsId")
                        .HasName("UnitsId");

                    b.ToTable("Units", (string)null);
                });

            modelBuilder.Entity("Control.DAL.Models.Position", b =>
                {
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

                    b.HasOne("Control.DAL.Models.Status", "Status")
                        .WithMany("Positions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Control.DAL.Models.Units", "Units")
                        .WithMany("Positions")
                        .HasForeignKey("UnitsId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Measuring");

                    b.Navigation("Nomination");

                    b.Navigation("Operation");

                    b.Navigation("Owner");

                    b.Navigation("Period");

                    b.Navigation("Status");

                    b.Navigation("Units");
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

            modelBuilder.Entity("Control.DAL.Models.Status", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Control.DAL.Models.Units", b =>
                {
                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
