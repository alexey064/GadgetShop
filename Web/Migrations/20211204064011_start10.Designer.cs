﻿// <auto-generated />
using System;
using Diplom.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Diplom.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20211204064011_start10")]
    partial class start10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Diplom.Models.Model.Accessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductID")
                        .IsUnique();

                    b.ToTable("Accessories");
                });

            modelBuilder.Entity("Diplom.Models.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PostId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Diplom.Models.Model.Notebook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short?>("BatteryCapacity")
                        .HasColumnType("smallint");

                    b.Property<string>("Camera")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HDDSize")
                        .HasColumnType("int");

                    b.Property<int>("OSId")
                        .HasColumnType("int");

                    b.Property<string>("Optional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Outputs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcessorId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<byte>("RAMCount")
                        .HasColumnType("tinyint");

                    b.Property<int>("SSDSize")
                        .HasColumnType("int");

                    b.Property<string>("ScreenResolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ScreenSize")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<int?>("ScreenTypeId")
                        .HasColumnType("int");

                    b.Property<int>("VideocardID")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<string>("WirelessCommunication")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OSId");

                    b.HasIndex("ProcessorId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("ScreenTypeId");

                    b.HasIndex("VideocardID");

                    b.ToTable("Notebooks");
                });

            modelBuilder.Entity("Diplom.Models.Model.ProdMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int?>("Id1Id")
                        .HasColumnType("int");

                    b.Property<int?>("MoveId")
                        .HasColumnType("int");

                    b.Property<int>("MovementTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id1Id");

                    b.HasIndex("MoveId");

                    b.HasIndex("MovementTypeId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TypeId");

                    b.ToTable("ProdMovements");
                });

            modelBuilder.Entity("Diplom.Models.Model.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<DateTime>("DiscountDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("ColorId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Diplom.Models.Model.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("Diplom.Models.Model.PurchaseHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("ProdMovementId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SellerId");

                    b.HasIndex("StatusId");

                    b.ToTable("PurchaseHistories");
                });

            modelBuilder.Entity("Diplom.Models.Model.Smartphone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatteryCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Camera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChargingTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Communication")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemoryCount")
                        .HasColumnType("int");

                    b.Property<bool>("NFC")
                        .HasColumnType("bit");

                    b.Property<int>("OSId")
                        .HasColumnType("int");

                    b.Property<string>("Optional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcessorId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<byte>("RAMCount")
                        .HasColumnType("tinyint");

                    b.Property<string>("ScreenResolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ScreenSize")
                        .HasColumnType("float");

                    b.Property<int>("ScreenTypeId")
                        .HasColumnType("int");

                    b.Property<short>("SimCount")
                        .HasColumnType("smallint");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChargingTypeId");

                    b.HasIndex("OSId");

                    b.HasIndex("ProcessorId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("ScreenTypeId");

                    b.ToTable("Smartphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.WireHeadphone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChargingTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("WireLenght")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ChargingTypeId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("WireHeadphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.WirelessHeadphone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short?>("Battery")
                        .HasColumnType("smallint");

                    b.Property<double>("BluetoothVersion")
                        .HasColumnType("float");

                    b.Property<short?>("CaseBattery")
                        .HasColumnType("smallint");

                    b.Property<int>("ChargingTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Radius")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChargingTypeId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("WirelessHeadphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.ChargingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChargingTypes");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.MovementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovementTypes");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.OS", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("OS");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Processor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.ScreenType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenTypes");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Videocard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Videocards");
                });

            modelBuilder.Entity("Diplom.Models.Model.Accessory", b =>
                {
                    b.HasOne("Diplom.Models.Model.Product", "product")
                        .WithOne("Accessory")
                        .HasForeignKey("Diplom.Models.Model.Accessory", "ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("Diplom.Models.Model.Client", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.Department", "Department")
                        .WithMany("Clients")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Type", "Post")
                        .WithMany("Clients")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Diplom.Models.Model.Notebook", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.OS", "OS")
                        .WithMany()
                        .HasForeignKey("OSId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Processor", "Processor")
                        .WithMany("Notebooks")
                        .HasForeignKey("ProcessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.Product", "product")
                        .WithOne("Notebook")
                        .HasForeignKey("Diplom.Models.Model.Notebook", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.ScreenType", "ScreenType")
                        .WithMany("Notebooks")
                        .HasForeignKey("ScreenTypeId");

                    b.HasOne("Diplom.Models.Model.simple.Videocard", "Videocard")
                        .WithMany("Notebooks")
                        .HasForeignKey("VideocardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OS");

                    b.Navigation("Processor");

                    b.Navigation("product");

                    b.Navigation("ScreenType");

                    b.Navigation("Videocard");
                });

            modelBuilder.Entity("Diplom.Models.Model.ProdMovement", b =>
                {
                    b.HasOne("Diplom.Models.Model.PurchaseHistory", "Id1")
                        .WithMany("ProdMovement")
                        .HasForeignKey("Id1Id");

                    b.HasOne("Diplom.Models.Model.Provider", "Provider")
                        .WithMany("ProdMovement")
                        .HasForeignKey("MoveId");

                    b.HasOne("Diplom.Models.Model.simple.MovementType", "MovementType")
                        .WithMany("ProdMovements")
                        .HasForeignKey("MovementTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.Product", "Product")
                        .WithMany("ProdMovements")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Type", null)
                        .WithMany("ProdMovements")
                        .HasForeignKey("TypeId");

                    b.Navigation("Id1");

                    b.Navigation("MovementType");

                    b.Navigation("Product");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Diplom.Models.Model.Product", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Color", "Color")
                        .WithMany("Products")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Department", "Department")
                        .WithMany("Products")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Type", "Type")
                        .WithMany("Products")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Color");

                    b.Navigation("Department");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Diplom.Models.Model.Provider", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.Department", "Department")
                        .WithMany("Providers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Diplom.Models.Model.PurchaseHistory", b =>
                {
                    b.HasOne("Diplom.Models.Model.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Diplom.Models.Model.simple.Department", "department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.Client", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.HasOne("Diplom.Models.Model.simple.Type", "Status")
                        .WithMany("PurchaseHistories")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("department");

                    b.Navigation("Seller");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Diplom.Models.Model.Smartphone", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.ChargingType", "ChargingType")
                        .WithMany("Smartphone")
                        .HasForeignKey("ChargingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.OS", "OS")
                        .WithMany()
                        .HasForeignKey("OSId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.Processor", "Processor")
                        .WithMany("Smartphones")
                        .HasForeignKey("ProcessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.Product", "product")
                        .WithOne("Smartphone")
                        .HasForeignKey("Diplom.Models.Model.Smartphone", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.simple.ScreenType", "ScreenType")
                        .WithMany("Smartphones")
                        .HasForeignKey("ScreenTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargingType");

                    b.Navigation("OS");

                    b.Navigation("Processor");

                    b.Navigation("product");

                    b.Navigation("ScreenType");
                });

            modelBuilder.Entity("Diplom.Models.Model.WireHeadphone", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.ChargingType", "ChargingType")
                        .WithMany("WireHeadphone")
                        .HasForeignKey("ChargingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.Product", "Product")
                        .WithOne("WireHeadphones")
                        .HasForeignKey("Diplom.Models.Model.WireHeadphone", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargingType");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Diplom.Models.Model.WirelessHeadphone", b =>
                {
                    b.HasOne("Diplom.Models.Model.simple.ChargingType", "Charging")
                        .WithMany("WirelessHeadphones")
                        .HasForeignKey("ChargingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diplom.Models.Model.Product", "Product")
                        .WithOne("WirelessHeadphones")
                        .HasForeignKey("Diplom.Models.Model.WirelessHeadphone", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Charging");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Diplom.Models.Model.Product", b =>
                {
                    b.Navigation("Accessory");

                    b.Navigation("Notebook");

                    b.Navigation("ProdMovements");

                    b.Navigation("Smartphone");

                    b.Navigation("WireHeadphones");

                    b.Navigation("WirelessHeadphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.Provider", b =>
                {
                    b.Navigation("ProdMovement");
                });

            modelBuilder.Entity("Diplom.Models.Model.PurchaseHistory", b =>
                {
                    b.Navigation("ProdMovement");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.ChargingType", b =>
                {
                    b.Navigation("Smartphone");

                    b.Navigation("WireHeadphone");

                    b.Navigation("WirelessHeadphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Color", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Department", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Products");

                    b.Navigation("Providers");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.MovementType", b =>
                {
                    b.Navigation("ProdMovements");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Processor", b =>
                {
                    b.Navigation("Notebooks");

                    b.Navigation("Smartphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.ScreenType", b =>
                {
                    b.Navigation("Notebooks");

                    b.Navigation("Smartphones");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Type", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("ProdMovements");

                    b.Navigation("Products");

                    b.Navigation("PurchaseHistories");
                });

            modelBuilder.Entity("Diplom.Models.Model.simple.Videocard", b =>
                {
                    b.Navigation("Notebooks");
                });
#pragma warning restore 612, 618
        }
    }
}
