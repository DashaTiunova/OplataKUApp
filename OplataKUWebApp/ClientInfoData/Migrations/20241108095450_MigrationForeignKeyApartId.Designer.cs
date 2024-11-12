﻿// <auto-generated />
using ClientInfoData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClientInfoData.Migrations
{
    [DbContext(typeof(ClientInfoContext))]
    [Migration("20241108095450_MigrationForeignKeyApartId")]
    partial class MigrationForeignKeyApartId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ClientInfoData.ClientInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApartId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Midname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ApartId");

                    b.ToTable("ClientInfos");
                });

            modelBuilder.Entity("ClientInfoData.PayInfo", b =>
                {
                    b.Property<int>("ApartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apartnumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Housenumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ApartId");

                    b.ToTable("PayInfos");
                });

            modelBuilder.Entity("ClientInfoData.ClientInfo", b =>
                {
                    b.HasOne("ClientInfoData.PayInfo", "PayInfo")
                        .WithMany()
                        .HasForeignKey("ApartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PayInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
