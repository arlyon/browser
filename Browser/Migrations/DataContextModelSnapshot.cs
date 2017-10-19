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
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("UrlAddon");

                    b.Property<string>("UrlHost");

                    b.Property<string>("UrlScheme");

                    b.Property<string>("UrlUnidentified");

                    b.HasKey("ID");

                    b.HasIndex("UrlScheme", "UrlHost", "UrlAddon", "UrlUnidentified");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Browser.History.HistoryLocation", b =>
                {
                    b.Property<DateTime>("Date");

                    b.Property<string>("Title");

                    b.HasKey("Date");

                    b.ToTable("History");
                });

            modelBuilder.Entity("Browser.Requests.Url", b =>
                {
                    b.Property<string>("Scheme");

                    b.Property<string>("Host");

                    b.Property<string>("Addon");

                    b.Property<string>("Unidentified");

                    b.HasKey("Scheme", "Host", "Addon", "Unidentified");

                    b.HasAlternateKey("Host");

                    b.ToTable("Url");
                });

            modelBuilder.Entity("Browser.Favorites.FavoritesLocation", b =>
                {
                    b.HasOne("Browser.Requests.Url", "Url")
                        .WithMany()
                        .HasForeignKey("UrlScheme", "UrlHost", "UrlAddon", "UrlUnidentified");
                });
#pragma warning restore 612, 618
        }
    }
}
