﻿// <auto-generated />
using System;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240903073123_AddProductsTable")]
    partial class AddProductsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BulkyBook.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DisplayOrder")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Comedy"
                        });
                });

            modelBuilder.Entity("BulkyBook.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Billy Spark",
                            Description = "Praesent vita sodales libro some text here",
                            ISBN = "SWD99999001",
                            ListPrice = 99.0,
                            Price = 90.0,
                            Price100 = 80.0,
                            Price50 = 85.0,
                            Title = "Fortune of Time"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Liu Cixin",
                            Description = "A gripping sci-fi novel that explores the complexity of space and time.",
                            ISBN = "SWD99999002",
                            ListPrice = 120.0,
                            Price = 110.0,
                            Price100 = 100.0,
                            Price50 = 105.0,
                            Title = "The Dark Forest"
                        },
                        new
                        {
                            Id = 3,
                            Author = "John Doe",
                            Description = "A thrilling adventure set in the depths of the ocean.",
                            ISBN = "SWD99999003",
                            ListPrice = 95.0,
                            Price = 85.0,
                            Price100 = 75.0,
                            Price50 = 80.0,
                            Title = "The Silent Sea"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Wu Cheng'en",
                            Description = "An ancient Chinese tale of bravery, wisdom, and perseverance.",
                            ISBN = "SWD99999004",
                            ListPrice = 150.0,
                            Price = 140.0,
                            Price100 = 130.0,
                            Price50 = 135.0,
                            Title = "Journey to the West"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Jane Smith",
                            Description = "A deep dive into the science of the human mind and potential.",
                            ISBN = "SWD99999005",
                            ListPrice = 85.0,
                            Price = 80.0,
                            Price100 = 70.0,
                            Price50 = 75.0,
                            Title = "Mind Over Matter"
                        },
                        new
                        {
                            Id = 6,
                            Author = "Carl Sagan",
                            Description = "An exploration of the universe and its profound mysteries.",
                            ISBN = "SWD99999006",
                            ListPrice = 110.0,
                            Price = 100.0,
                            Price100 = 90.0,
                            Price50 = 95.0,
                            Title = "Secrets of the Cosmos"
                        },
                        new
                        {
                            Id = 7,
                            Author = "Sun Tzu",
                            Description = "An ancient Chinese military treatise on strategy and tactics.",
                            ISBN = "SWD99999007",
                            ListPrice = 75.0,
                            Price = 70.0,
                            Price100 = 60.0,
                            Price50 = 65.0,
                            Title = "The Art of War"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
