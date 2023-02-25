﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20230327105315_login-vertification")]
    partial class loginvertification
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("CategorySequence");

            modelBuilder.HasSequence("ProductSequence");

            modelBuilder.Entity("Core.Security.Entities.EmailVertificator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailVertificators");
                });

            modelBuilder.Entity("Core.Security.Entities.OperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationClaim");
                });

            modelBuilder.Entity("Core.Security.Entities.OtpVertificator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OtpVertificators");
                });

            modelBuilder.Entity("Core.Security.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RevokedReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Core.Security.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("VertificationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Core.Security.Entities.UserOperationClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OperationClaimId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperationClaimId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOperationClaim");
                });

            modelBuilder.Entity("Domain.Entities.AbstractEntities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [CategorySequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.AbstractEntities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [ProductSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<int?>("Calories")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Explanation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasStock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ClosedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("PaidPrice")
                        .HasColumnType("float");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderedProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderedProducts");
                });

            modelBuilder.Entity("Domain.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Domain.Entities.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("TableNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.HasBaseType("Core.Security.Entities.User");

                    b.HasDiscriminator().HasValue("Owner");
                });

            modelBuilder.Entity("Domain.Entities.DrinkCategory", b =>
                {
                    b.HasBaseType("Domain.Entities.AbstractEntities.Category");

                    b.HasIndex("MenuId");

                    b.ToTable("DrinkCategories");
                });

            modelBuilder.Entity("Domain.Entities.FoodCategory", b =>
                {
                    b.HasBaseType("Domain.Entities.AbstractEntities.Category");

                    b.HasIndex("MenuId");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("Domain.Entities.Drink", b =>
                {
                    b.HasBaseType("Domain.Entities.AbstractEntities.Product");

                    b.Property<int>("DrinkCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("Volume")
                        .HasColumnType("int");

                    b.HasIndex("DrinkCategoryId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("Domain.Entities.Food", b =>
                {
                    b.HasBaseType("Domain.Entities.AbstractEntities.Product");

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Core.Security.Entities.EmailVertificator", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Security.Entities.OtpVertificator", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Security.Entities.RefreshToken", b =>
                {
                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Security.Entities.UserOperationClaim", b =>
                {
                    b.HasOne("Core.Security.Entities.OperationClaim", "OperationClaim")
                        .WithMany()
                        .HasForeignKey("OperationClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Security.Entities.User", "User")
                        .WithMany("UserOperationClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperationClaim");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.HasOne("Domain.Entities.Restaurant", "Resturant")
                        .WithOne("Menu")
                        .HasForeignKey("Domain.Entities.Menu", "RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.Table", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Domain.Entities.OrderedProduct", b =>
                {
                    b.HasOne("Domain.Entities.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("Domain.Entities.AbstractEntities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Restaurant", b =>
                {
                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithMany("Restaurants")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Table", b =>
                {
                    b.HasOne("Domain.Entities.Order", "CurrentOrder")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Table", "OrderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Domain.Entities.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentOrder");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Domain.Entities.DrinkCategory", b =>
                {
                    b.HasOne("Domain.Entities.Menu", "Menu")
                        .WithMany("Drinks")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Domain.Entities.FoodCategory", b =>
                {
                    b.HasOne("Domain.Entities.Menu", "Menu")
                        .WithMany("Foods")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Domain.Entities.Drink", b =>
                {
                    b.HasOne("Domain.Entities.DrinkCategory", "DrinkCategory")
                        .WithMany("Drinks")
                        .HasForeignKey("DrinkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrinkCategory");
                });

            modelBuilder.Entity("Domain.Entities.Food", b =>
                {
                    b.HasOne("Domain.Entities.FoodCategory", "FoodCategory")
                        .WithMany("Foods")
                        .HasForeignKey("FoodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodCategory");
                });

            modelBuilder.Entity("Core.Security.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UserOperationClaims");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Navigation("Drinks");

                    b.Navigation("Foods");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Restaurant", b =>
                {
                    b.Navigation("Menu")
                        .IsRequired();

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Domain.Entities.Table", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Navigation("Restaurants");
                });

            modelBuilder.Entity("Domain.Entities.DrinkCategory", b =>
                {
                    b.Navigation("Drinks");
                });

            modelBuilder.Entity("Domain.Entities.FoodCategory", b =>
                {
                    b.Navigation("Foods");
                });
#pragma warning restore 612, 618
        }
    }
}
