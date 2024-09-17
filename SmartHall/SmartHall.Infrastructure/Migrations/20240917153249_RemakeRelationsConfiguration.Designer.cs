﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHall.Infrastructure.Persistense;

#nullable disable

namespace SmartHall.Infrastructure.Migrations
{
    [DbContext(typeof(SmartHallDbContext))]
    [Migration("20240917153249_RemakeRelationsConfiguration")]
    partial class RemakeRelationsConfiguration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartHall.Domain.HallAggregate.Hall", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("BaseCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Halls", (string)null);
                });

            modelBuilder.Entity("SmartHall.Domain.HallAggregate.Hall", b =>
                {
                    b.OwnsMany("SmartHall.Domain.HallAggregate.Entities.HallEquipment.HallEquipment", "AvailableEquipment", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("HallId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Cost")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("Id", "HallId");

                            b1.HasIndex("HallId");

                            b1.ToTable("HallEquipment", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("HallId");
                        });

                    b.OwnsMany("SmartHall.Domain.HallAggregate.Entities.Reservation.Reservation", "Reservations", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("HallId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Period")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Period");

                            b1.HasKey("Id", "HallId");

                            b1.HasIndex("HallId");

                            b1.ToTable("Reservations", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("HallId");
                        });

                    b.Navigation("AvailableEquipment");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
