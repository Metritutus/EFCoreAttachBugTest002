﻿// <auto-generated />
using System;
using EFCoreAttachBugTest002;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreAttachBugTest002.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("EFCoreAttachBugTest002.Models.Example", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("HolderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IntegerValue")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MysteryTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HolderId");

                    b.HasIndex("MysteryTypeId");

                    b.ToTable("Example");
                });

            modelBuilder.Entity("EFCoreAttachBugTest002.Models.Holder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Holder");
                });

            modelBuilder.Entity("EFCoreAttachBugTest002.Models.MysteryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MysteryType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mystery1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mystery2"
                        });
                });

            modelBuilder.Entity("EFCoreAttachBugTest002.Models.Example", b =>
                {
                    b.HasOne("EFCoreAttachBugTest002.Models.Holder", null)
                        .WithMany("Examples")
                        .HasForeignKey("HolderId");

                    b.HasOne("EFCoreAttachBugTest002.Models.MysteryType", "MysteryType")
                        .WithMany("Examples")
                        .HasForeignKey("MysteryTypeId");

                    b.Navigation("MysteryType");
                });

            modelBuilder.Entity("EFCoreAttachBugTest002.Models.Holder", b =>
                {
                    b.Navigation("Examples");
                });

            modelBuilder.Entity("EFCoreAttachBugTest002.Models.MysteryType", b =>
                {
                    b.Navigation("Examples");
                });
#pragma warning restore 612, 618
        }
    }
}