﻿// <auto-generated />
using System;
using ManagementFinanceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagementFinanceApp.Migrations
{
    [DbContext(typeof(ManagementFinanceAppDbContext))]
    [Migration("20221204171317_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("CategoryExpenses");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CategoryGroups");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("CategoryIncomes");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategorySaving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanPay")
                        .HasColumnType("boolean");

                    b.Property<int>("CategoryGroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("Debt")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("CategorySavings");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Attachment")
                        .HasColumnType("text");

                    b.Property<int>("CategoryExpenseId")
                        .HasColumnType("integer");

                    b.Property<int>("CategorySavingId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("HowMuch")
                        .HasColumnType("double precision");

                    b.Property<bool>("StandingOrder")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryExpenseId");

                    b.HasIndex("CategorySavingId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Frequency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Frequencies");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Attachment")
                        .HasColumnType("text");

                    b.Property<int>("CategoryIncomeId")
                        .HasColumnType("integer");

                    b.Property<int>("CategorySavingId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("HowMuch")
                        .HasColumnType("double precision");

                    b.Property<bool>("StandingOrder")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryIncomeId");

                    b.HasIndex("CategorySavingId");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Investment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("InvestmentScheduleId")
                        .HasColumnType("integer");

                    b.Property<int>("IsActive")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PeriodInvestment")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UnitInvestment")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InvestmentScheduleId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.InvestmentSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("AddedScheduleFromUser")
                        .HasColumnType("boolean");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Capitalization")
                        .HasColumnType("integer");

                    b.Property<string>("ConditionEarlyTerminationInvestment")
                        .HasColumnType("text");

                    b.Property<bool>("InterestRateInAllPerdiodInvestment")
                        .HasColumnType("boolean");

                    b.Property<double>("InterestRateOnScaleOfYear")
                        .HasColumnType("double precision");

                    b.Property<double>("MaxAmount")
                        .HasColumnType("double precision");

                    b.Property<double>("MinAmount")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PeriodDeposit")
                        .HasColumnType("integer");

                    b.Property<bool>("PossibilityEarlyTerminationInvestment")
                        .HasColumnType("boolean");

                    b.Property<bool>("RequiredPersonalAccountInCurrentBank")
                        .HasColumnType("boolean");

                    b.Property<string>("RestInformation")
                        .HasColumnType("text");

                    b.Property<int>("UnitDeposit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("InvestmentSchedules");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAddedToQueue")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlanType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Restriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryExpenseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("MaxMonth")
                        .HasColumnType("double precision");

                    b.Property<double>("MaxYear")
                        .HasColumnType("double precision");

                    b.Property<int>("RestrictionYear")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryExpenseId");

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Saving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategorySavingId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("HowMuch")
                        .HasColumnType("double precision");

                    b.Property<int>("SavingType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategorySavingId");

                    b.ToTable("Savings");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.SavingState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("State")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("SavingStates");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Frequency")
                        .HasColumnType("integer");

                    b.Property<int>("FrequencyId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SavingId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeStandingOrder")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FrequencyId");

                    b.HasIndex("SavingId");

                    b.ToTable("StandingOrders");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrderHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FrequencyId")
                        .HasColumnType("integer");

                    b.Property<int>("SavingId")
                        .HasColumnType("integer");

                    b.Property<int>("StandingOrderId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeStandingOrder")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategorySavingId")
                        .HasColumnType("integer");

                    b.Property<int>("CategorySavingIdFrom")
                        .HasColumnType("integer");

                    b.Property<int>("CategorySavingIdTo")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("HowMuch")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CategorySavingId");

                    b.ToTable("TransferHistories");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("Nick")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryExpense", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("CategoryExpense")
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryGroup");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryIncome", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("CategoryIncomes")
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryGroup");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategorySaving", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryGroup", "CategoryGroup")
                        .WithMany("CategorySavings")
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryGroup");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Expense", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryExpense", "CategoryExpense")
                        .WithMany()
                        .HasForeignKey("CategoryExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryExpense");

                    b.Navigation("CategorySaving");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Income", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryIncome", "CategoryIncome")
                        .WithMany()
                        .HasForeignKey("CategoryIncomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryIncome");

                    b.Navigation("CategorySaving");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Investment", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.InvestmentSchedule", "InvestmentSchedule")
                        .WithMany("Investments")
                        .HasForeignKey("InvestmentScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvestmentSchedule");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Restriction", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategoryExpense", "CategoryExpense")
                        .WithMany()
                        .HasForeignKey("CategoryExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryExpense");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.Saving", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategorySaving");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrder", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.Frequency", "Frequencies")
                        .WithMany()
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.Saving", "Savings")
                        .WithMany()
                        .HasForeignKey("SavingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Frequencies");

                    b.Navigation("Savings");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.StandingOrderHistory", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.Frequency", "Frequency")
                        .WithMany()
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.Saving", "Savings")
                        .WithMany()
                        .HasForeignKey("SavingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.StandingOrder", "StandingOrder")
                        .WithMany()
                        .HasForeignKey("StandingOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementFinanceApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Frequency");

                    b.Navigation("Savings");

                    b.Navigation("StandingOrder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.TransferHistory", b =>
                {
                    b.HasOne("ManagementFinanceApp.Entities.CategorySaving", "CategorySaving")
                        .WithMany()
                        .HasForeignKey("CategorySavingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategorySaving");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.CategoryGroup", b =>
                {
                    b.Navigation("CategoryExpense");

                    b.Navigation("CategoryIncomes");

                    b.Navigation("CategorySavings");
                });

            modelBuilder.Entity("ManagementFinanceApp.Entities.InvestmentSchedule", b =>
                {
                    b.Navigation("Investments");
                });
#pragma warning restore 612, 618
        }
    }
}
