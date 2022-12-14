// <auto-generated />
using System;
using DemoApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoApp.Server.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20220907105757_Initial DB")]
    partial class InitialDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DemoApp.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1001,
                            Email = "liyojose@gmail.com",
                            Firstname = "Liyo",
                            LastUpdated = new DateTime(2022, 9, 7, 16, 27, 57, 465, DateTimeKind.Local).AddTicks(7521),
                            Lastname = "Jose"
                        },
                        new
                        {
                            Id = 1002,
                            Email = "roger@gmail.com",
                            Firstname = "Roger",
                            LastUpdated = new DateTime(2022, 9, 7, 16, 27, 57, 465, DateTimeKind.Local).AddTicks(7533),
                            Lastname = "Wan"
                        },
                        new
                        {
                            Id = 1003,
                            Email = "pedro@gmail.com",
                            Firstname = "Pedro",
                            LastUpdated = new DateTime(2022, 9, 7, 16, 27, 57, 465, DateTimeKind.Local).AddTicks(7534),
                            Lastname = "hugo"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
