﻿// <auto-generated />
using System;
using GasStationRatingSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GasStationRatingSystem.DAL.Migrations
{
    [DbContext(typeof(GasStationRatingSystemContext))]
    [Migration("20210721034647_db_21072021_0")]
    partial class db_21072021_0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GasStationRatingSystem.Tables.GasStation", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LauncherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AddedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("GasStations", "Guide");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.GasStationContact", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ContactType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GasStationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AddedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("GasStationId");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("GasStationContacts", "Guide");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeOfReset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetPasswordDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UseDefaultPassword")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("AddedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users", "People");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.UserType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AddedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.VisitInfo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VisitNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VisitTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AddedBy");

                    b.HasIndex("DeletedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("StationId");

                    b.HasIndex("UserId");

                    b.ToTable("VisitInfo", "Visit");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.GasStation", b =>
                {
                    b.HasOne("GasStationRatingSystem.Tables.User", "CreatedUser")
                        .WithMany("GasStationCreated")
                        .HasForeignKey("AddedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "DeletedUser")
                        .WithMany("GasStationDeleted")
                        .HasForeignKey("DeletedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "ModifiedUser")
                        .WithMany("GasStationModified")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedUser");

                    b.Navigation("DeletedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.GasStationContact", b =>
                {
                    b.HasOne("GasStationRatingSystem.Tables.User", "CreatedUser")
                        .WithMany("GasStationContactCreated")
                        .HasForeignKey("AddedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "DeletedUser")
                        .WithMany("GasStationContactDeleted")
                        .HasForeignKey("DeletedBy");

                    b.HasOne("GasStationRatingSystem.Tables.GasStation", "GasStation")
                        .WithMany("GasStationContacts")
                        .HasForeignKey("GasStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GasStationRatingSystem.Tables.User", "ModifiedUser")
                        .WithMany("GasStationContactModified")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedUser");

                    b.Navigation("DeletedUser");

                    b.Navigation("GasStation");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.User", b =>
                {
                    b.HasOne("GasStationRatingSystem.Tables.User", "CreatedUser")
                        .WithMany("UserCreated")
                        .HasForeignKey("AddedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "DeletedUser")
                        .WithMany("UserDeleted")
                        .HasForeignKey("DeletedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "ModifiedUser")
                        .WithMany("UserModified")
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("GasStationRatingSystem.Tables.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("DeletedUser");

                    b.Navigation("ModifiedUser");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.UserType", b =>
                {
                    b.HasOne("GasStationRatingSystem.Tables.User", "CreatedUser")
                        .WithMany("UserTypeCreated")
                        .HasForeignKey("AddedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "DeletedUser")
                        .WithMany("UserTypeDeleted")
                        .HasForeignKey("DeletedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "ModifiedUser")
                        .WithMany("UserTypeModified")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedUser");

                    b.Navigation("DeletedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.VisitInfo", b =>
                {
                    b.HasOne("GasStationRatingSystem.Tables.User", "CreatedUser")
                        .WithMany("VisitInfoCreated")
                        .HasForeignKey("AddedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "DeletedUser")
                        .WithMany("VisitInfoDeleted")
                        .HasForeignKey("DeletedBy");

                    b.HasOne("GasStationRatingSystem.Tables.User", "ModifiedUser")
                        .WithMany("VisitInfoModified")
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("GasStationRatingSystem.Tables.GasStation", "GasStation")
                        .WithMany("VisitInfo")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GasStationRatingSystem.Tables.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("DeletedUser");

                    b.Navigation("GasStation");

                    b.Navigation("ModifiedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.GasStation", b =>
                {
                    b.Navigation("GasStationContacts");

                    b.Navigation("VisitInfo");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.User", b =>
                {
                    b.Navigation("GasStationContactCreated");

                    b.Navigation("GasStationContactDeleted");

                    b.Navigation("GasStationContactModified");

                    b.Navigation("GasStationCreated");

                    b.Navigation("GasStationDeleted");

                    b.Navigation("GasStationModified");

                    b.Navigation("UserCreated");

                    b.Navigation("UserDeleted");

                    b.Navigation("UserModified");

                    b.Navigation("UserTypeCreated");

                    b.Navigation("UserTypeDeleted");

                    b.Navigation("UserTypeModified");

                    b.Navigation("VisitInfoCreated");

                    b.Navigation("VisitInfoDeleted");

                    b.Navigation("VisitInfoModified");
                });

            modelBuilder.Entity("GasStationRatingSystem.Tables.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}