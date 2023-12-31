﻿// <auto-generated />
using CustomerAPP.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomerAPP.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerAPP.Shared.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            CompanyName = "Microsoft"
                        },
                        new
                        {
                            CompanyId = 2,
                            CompanyName = "Apple"
                        },
                        new
                        {
                            CompanyId = 3,
                            CompanyName = "IBM"
                        });
                });

            modelBuilder.Entity("CustomerAPP.Shared.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            Email = "ahoppikins@gmail.com",
                            FirstName = "Antony",
                            LastName = "Hoppikins"
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 2,
                            Email = "jwayne@gmail.com",
                            FirstName = "John",
                            LastName = "Wayne"
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 3,
                            Email = "msunnys@gmail.com",
                            FirstName = "Marcus",
                            LastName = "Sunny"
                        },
                        new
                        {
                            Id = 4,
                            CompanyId = 2,
                            Email = "paustin@gmail.com",
                            FirstName = "Peter",
                            LastName = "Austin"
                        },
                        new
                        {
                            Id = 5,
                            CompanyId = 3,
                            Email = "gcapilano@gmail.com",
                            FirstName = "Gregory",
                            LastName = "Capilano"
                        });
                });

            modelBuilder.Entity("CustomerAPP.Shared.Customer", b =>
                {
                    b.HasOne("CustomerAPP.Shared.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
