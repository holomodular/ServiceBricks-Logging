﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceBricks.Logging.Sqlite;

#nullable disable

namespace ServiceBricks.Logging.Sqlite.Migrations
{
    [DbContext(typeof(LoggingSqliteContext))]
    partial class LoggingSqliteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Logging")
                .HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ServiceBricks.Logging.EntityFrameworkCore.LogMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Application")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("CreateDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Properties")
                        .HasColumnType("TEXT");

                    b.Property<string>("Server")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserStorageKey")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("LogMessage", "Logging");
                });

            modelBuilder.Entity("ServiceBricks.Logging.EntityFrameworkCore.WebRequestMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("CreateDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("RequestBody")
                        .HasColumnType("TEXT");

                    b.Property<long?>("RequestContentLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequestContentType")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestCookies")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("RequestHasFormContentType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequestHeaders")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestHost")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestIPAddress")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("RequestIsHttps")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequestMethod")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestPathBase")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestProtocol")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestQuery")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestQueryString")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestRouteValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestScheme")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("TEXT");

                    b.Property<long?>("ResponseContentLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ResponseContentType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResponseCookies")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResponseHeaders")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ResponseStatusCode")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("ResponseTotalMilliseconds")
                        .HasColumnType("INTEGER");

                    b.HasKey("Key");

                    b.ToTable("WebRequestMessage", "Logging");
                });
#pragma warning restore 612, 618
        }
    }
}
