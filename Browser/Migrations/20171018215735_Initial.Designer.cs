﻿// <auto-generated />
using Browser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Browser.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20171018215735_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Browser.Favorites.FavoritesLocation", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.Property<Guid?>("UrlID");

                    b.HasKey("ID");

                    b.HasIndex("UrlID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Browser.History.HistoryLocation", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Title");

                    b.Property<Guid?>("UrlID");

                    b.HasKey("ID");

                    b.HasIndex("UrlID");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Browser.Requests.Url", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Addon");

                    b.Property<string>("Host");

                    b.Property<string>("Scheme");

                    b.Property<string>("Unidentified");

                    b.HasKey("ID");

                    b.ToTable("Url");
                });

            modelBuilder.Entity("Browser.Favorites.FavoritesLocation", b =>
                {
                    b.HasOne("Browser.Requests.Url", "Url")
                        .WithMany()
                        .HasForeignKey("UrlID");
                });

            modelBuilder.Entity("Browser.History.HistoryLocation", b =>
                {
                    b.HasOne("Browser.Requests.Url", "Url")
                        .WithMany()
                        .HasForeignKey("UrlID");
                });
#pragma warning restore 612, 618
        }
    }
}
