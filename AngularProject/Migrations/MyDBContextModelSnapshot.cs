﻿// <auto-generated />
using System;
using AngularProject.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AngularProject.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AngularProject.Context.dept2", b =>
                {
                    b.Property<string>("deptid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("deptname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("deptid");

                    b.ToTable("dept2");
                });

            modelBuilder.Entity("AngularProject.Context.items2", b =>
                {
                    b.Property<string>("itemcode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("cost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("deptid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("itemname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("rate")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("itemcode");

                    b.HasIndex("deptid");

                    b.ToTable("items2");
                });

            modelBuilder.Entity("AngularProject.Context.items2", b =>
                {
                    b.HasOne("AngularProject.Context.dept2", "dept2")
                        .WithMany("items2")
                        .HasForeignKey("deptid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dept2");
                });

            modelBuilder.Entity("AngularProject.Context.dept2", b =>
                {
                    b.Navigation("items2");
                });
#pragma warning restore 612, 618
        }
    }
}
