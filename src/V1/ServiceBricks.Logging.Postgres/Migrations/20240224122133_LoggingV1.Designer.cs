﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceBricks.Logging.Postgres;

#nullable disable

namespace ServiceBricks.Logging.Postgres.Migrations
{
    [DbContext(typeof(LoggingPostgresContext))]
    [Migration("20240224122133_LoggingV1")]
    partial class LoggingV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Logging")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ServiceBricks.Logging.EntityFrameworkCore.LogMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Key"));

                    b.Property<string>("Application")
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("Properties")
                        .HasColumnType("text");

                    b.Property<string>("Server")
                        .HasColumnType("text");

                    b.Property<string>("UserStorageKey")
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.ToTable("LogMessage", "Logging");
                });

            modelBuilder.Entity("ServiceBricks.Logging.EntityFrameworkCore.WebRequestMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Key"));

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RequestBody")
                        .HasColumnType("text");

                    b.Property<long?>("RequestContentLength")
                        .HasColumnType("bigint");

                    b.Property<string>("RequestContentType")
                        .HasColumnType("text");

                    b.Property<string>("RequestCookies")
                        .HasColumnType("text");

                    b.Property<bool?>("RequestHasFormContentType")
                        .HasColumnType("boolean");

                    b.Property<string>("RequestHeaders")
                        .HasColumnType("text");

                    b.Property<string>("RequestHost")
                        .HasColumnType("text");

                    b.Property<string>("RequestIPAddress")
                        .HasColumnType("text");

                    b.Property<bool?>("RequestIsHttps")
                        .HasColumnType("boolean");

                    b.Property<string>("RequestMethod")
                        .HasColumnType("text");

                    b.Property<string>("RequestPath")
                        .HasColumnType("text");

                    b.Property<string>("RequestPathBase")
                        .HasColumnType("text");

                    b.Property<string>("RequestProtocol")
                        .HasColumnType("text");

                    b.Property<string>("RequestQuery")
                        .HasColumnType("text");

                    b.Property<string>("RequestQueryString")
                        .HasColumnType("text");

                    b.Property<string>("RequestRouteValues")
                        .HasColumnType("text");

                    b.Property<string>("RequestScheme")
                        .HasColumnType("text");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("text");

                    b.Property<long?>("ResponseContentLength")
                        .HasColumnType("bigint");

                    b.Property<string>("ResponseContentType")
                        .HasColumnType("text");

                    b.Property<string>("ResponseCookies")
                        .HasColumnType("text");

                    b.Property<string>("ResponseHeaders")
                        .HasColumnType("text");

                    b.Property<int?>("ResponseStatusCode")
                        .HasColumnType("integer");

                    b.Property<long?>("ResponseTotalMilliseconds")
                        .HasColumnType("bigint");

                    b.HasKey("Key");

                    b.ToTable("WebRequestMessage", "Logging");
                });
#pragma warning restore 612, 618
        }
    }
}
