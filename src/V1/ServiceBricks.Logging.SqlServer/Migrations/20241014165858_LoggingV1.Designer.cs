﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceBricks.Logging.SqlServer;

#nullable disable

namespace ServiceBricks.Logging.SqlServer.Migrations
{
    [DbContext(typeof(LoggingSqlServerContext))]
    [Migration("20241014165858_LoggingV1")]
    partial class LoggingV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Logging")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceBricks.Logging.EntityFrameworkCore.LogMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Key"));

                    b.Property<string>("Application")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Server")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserStorageKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.HasIndex("Application", "Level", "CreateDate");

                    b.ToTable("LogMessage", "Logging");
                });

            modelBuilder.Entity("ServiceBricks.Logging.EntityFrameworkCore.WebRequestMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Key"));

                    b.Property<string>("Application")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RequestContentLength")
                        .HasColumnType("bigint");

                    b.Property<string>("RequestContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestCookies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("RequestHasFormContentType")
                        .HasColumnType("bit");

                    b.Property<string>("RequestHeaders")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestHost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestIPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("RequestIsHttps")
                        .HasColumnType("bit");

                    b.Property<string>("RequestMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestPathBase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestProtocol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestQuery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestQueryString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestRouteValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestScheme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ResponseContentLength")
                        .HasColumnType("bigint");

                    b.Property<string>("ResponseContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseCookies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseHeaders")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResponseStatusCode")
                        .HasColumnType("int");

                    b.Property<long?>("ResponseTotalMilliseconds")
                        .HasColumnType("bigint");

                    b.Property<string>("Server")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserStorageKey")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Key");

                    b.HasIndex("Application", "UserStorageKey", "CreateDate");

                    b.ToTable("WebRequestMessage", "Logging");
                });
#pragma warning restore 612, 618
        }
    }
}
