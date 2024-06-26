﻿// <auto-generated />
using BlazorIntus2.Shared.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorIntus2.Shared.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240328101705_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+OrderWindow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("WindowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("WindowId");

                    b.ToTable("OrderWindow", (string)null);
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+SubElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.Property<int>("WindowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WindowId");

                    b.ToTable("SubElement", (string)null);
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+Window", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityOfWindows")
                        .HasColumnType("int");

                    b.Property<int>("TotalSubElements")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Window", (string)null);
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+OrderWindow", b =>
                {
                    b.HasOne("BlazorIntus2.Shared.DataModel.Data+Order", "Order")
                        .WithMany("OrderWindows")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlazorIntus2.Shared.DataModel.Data+Window", "Window")
                        .WithMany()
                        .HasForeignKey("WindowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Window");
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+SubElement", b =>
                {
                    b.HasOne("BlazorIntus2.Shared.DataModel.Data+Window", "Window")
                        .WithMany("SubElements")
                        .HasForeignKey("WindowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Window");
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+Order", b =>
                {
                    b.Navigation("OrderWindows");
                });

            modelBuilder.Entity("BlazorIntus2.Shared.DataModel.Data+Window", b =>
                {
                    b.Navigation("SubElements");
                });
#pragma warning restore 612, 618
        }
    }
}
