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

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryGroupId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("CategoryExpenses");
                });

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

            modelBuilder.Entity("ManagementFinanceApp.Entities.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attachment");

                    b.Property<int>("CategoryExpenseId");

                    b.Property<int>("CategorySavingId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<double>("HowMuch");

                    b.Property<bool>("StandingOrder");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryExpenseId");

                    b.HasIndex("CategorySavingId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Frequency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("Frequencies");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attachment");

                    b.Property<int>("CategoryIncomeId");

                    b.Property<int>("CategorySavingId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<double>("HowMuch");

                    b.Property<bool>("StandingOrder");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryIncomeId");

                    b.HasIndex("CategorySavingId");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsAddedToQueue");

                    b.Property<bool>("IsDone");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PlanType");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Restriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryExpenseId");

                    b.Property<DateTime>("Date");

                    b.Property<double>("MaxMonth");

                    b.Property<double>("MaxYear");

                    b.Property<int>("RestrictionYear");

                    b.HasKey("Id");

                    b.HasIndex("CategoryExpenseId");

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Saving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategorySavingId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<double>("HowMuch");

                    b.Property<int>("SavingType");

                    b.HasKey("Id");

                    b.HasIndex("CategorySavingId");

                    b.ToTable("Savings");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.SavingState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<double>("State");

                    b.HasKey("Id");

                    b.ToTable("SavingStates");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Frequency");

                    b.Property<int>("FrequencyId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SavingId");

                    b.Property<int>("TypeStandingOrder");

                    b.HasKey("Id");

                    b.HasIndex("FrequencyId");

                    b.HasIndex("SavingId");

                    b.ToTable("StandingOrders");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrderHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FrequencyId");

                    b.Property<int>("SavingId");

                    b.Property<int>("StandingOrderId");

                    b.Property<int>("TypeStandingOrder");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FrequencyId");

                    b.HasIndex("SavingId");

                    b.HasIndex("StandingOrderId");

                    b.HasIndex("UserId");

                    b.ToTable("StandingOrderHistories");
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

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryExpense", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("CategoryExpense")
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryIncome", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("CategoryIncomes")
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

            modelBuilder.Entity("ManagementFinanceApp.Entities.Expense", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryExpense", "CategoryExpense")
                        .WithMany()
                        .HasForeignKey("CategoryExpenseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Income", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryIncome", "CategoryIncome")
                        .WithMany()
                        .HasForeignKey("CategoryIncomeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Restriction", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryExpense", "CategoryExpense")
                        .WithMany()
                        .HasForeignKey("CategoryExpenseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Saving", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrder", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.Frequency", "Frequencies")
                        .WithMany()
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.Saving", "Savings")
                        .WithMany()
                        .HasForeignKey("SavingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrderHistory", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.Frequency", "Frequency")
                        .WithMany()
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.Saving", "Savings")
                        .WithMany()
                        .HasForeignKey("SavingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.StandingOrder", "StandingOrder")
                        .WithMany()
                        .HasForeignKey("StandingOrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ManagementFinanceApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
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
