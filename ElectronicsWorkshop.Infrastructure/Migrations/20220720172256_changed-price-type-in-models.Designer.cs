﻿// <auto-generated />
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElectronicsWorkshop.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220720172256_changed-price-type-in-models")]
    partial class changedpricetypeinmodels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CompositeDeviceConnector", b =>
                {
                    b.Property<int>("CompositeDevicesId")
                        .HasColumnType("int");

                    b.Property<int>("ConnectorsId")
                        .HasColumnType("int");

                    b.HasKey("CompositeDevicesId", "ConnectorsId");

                    b.HasIndex("ConnectorsId");

                    b.ToTable("CompositeDeviceConnector");
                });

            modelBuilder.Entity("ElectronicsWorkshop.Core.Domain.Models.BaseDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BaseDevices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Laptop Motherboard",
                            Price = 20000m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 2,
                            Name = "HD Monitor PCB",
                            Price = 5000m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 3,
                            Name = "Smartphone PCB",
                            Price = 10000m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 4,
                            Name = "Arduino",
                            Price = 10m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 5,
                            Name = "Raspberry Pi",
                            Price = 100m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 6,
                            Name = "Oscilloscope PCB",
                            Price = 25000m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 7,
                            Name = "VNA PCB",
                            Price = 50000m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 8,
                            Name = "RF Amplifier PCB",
                            Price = 1500m,
                            Quantity = 100
                        });
                });

            modelBuilder.Entity("ElectronicsWorkshop.Core.Domain.Models.CompositeDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BasisId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasisId");

                    b.ToTable("CompositeDevices");
                });

            modelBuilder.Entity("ElectronicsWorkshop.Core.Domain.Models.Connector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Connectors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "FFC",
                            Price = 11m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 2,
                            Name = "LCEDI",
                            Price = 9m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 3,
                            Name = "USB",
                            Price = 2m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 4,
                            Name = "USB type C",
                            Price = 2m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 5,
                            Name = "General Pin",
                            Price = 1m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 6,
                            Name = "DC Power",
                            Price = 9m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 7,
                            Name = "BNC",
                            Price = 6m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 8,
                            Name = "SMA",
                            Price = 15m,
                            Quantity = 100
                        });
                });

            modelBuilder.Entity("CompositeDeviceConnector", b =>
                {
                    b.HasOne("ElectronicsWorkshop.Core.Domain.Models.CompositeDevice", null)
                        .WithMany()
                        .HasForeignKey("CompositeDevicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicsWorkshop.Core.Domain.Models.Connector", null)
                        .WithMany()
                        .HasForeignKey("ConnectorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ElectronicsWorkshop.Core.Domain.Models.CompositeDevice", b =>
                {
                    b.HasOne("ElectronicsWorkshop.Core.Domain.Models.BaseDevice", "Basis")
                        .WithMany("CompositeDevices")
                        .HasForeignKey("BasisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basis");
                });

            modelBuilder.Entity("ElectronicsWorkshop.Core.Domain.Models.BaseDevice", b =>
                {
                    b.Navigation("CompositeDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
