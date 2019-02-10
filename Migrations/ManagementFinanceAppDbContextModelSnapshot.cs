﻿// <auto-generated />
using System;
using ManagementFinanceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementFinanceApp.Migrations
{
    [DbContext(typeof(ManagementFinanceAppDbContext))]
    partial class ManagementFinanceAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryType");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CategoryGroups");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryGroupId");

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("CategoryIncomes");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategorySaving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanPay");

                    b.Property<int>("CategoryGroupId");

                    b.Property<bool>("Debt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("CategorySavings");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attachment");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<double>("HowMuch");

                    b.Property<bool>("StandingOrder");

                    b.HasKey("Id");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.TransferHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategorySavingId");

                    b.Property<int>("CategorySavingIdFrom");

                    b.Property<int>("CategorySavingIdTo");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<double>("HowMuch");

                    b.HasKey("Id");

                    b.HasIndex("CategorySavingId");

                    b.ToTable("TransferHistories");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(40);

                    b.Property<string>("Nick")
                        .HasMaxLength(20);

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryIncome", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany()
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategorySaving", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("CategorySavings")
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.TransferHistory", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
