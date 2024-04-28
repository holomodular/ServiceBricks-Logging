using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServiceBricks.Logging.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class LoggingV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Logging");

            migrationBuilder.CreateTable(
                name: "LogMessage",
                schema: "Logging",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Application = table.Column<string>(type: "text", nullable: true),
                    Server = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    UserStorageKey = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogMessage", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "WebRequestMessage",
                schema: "Logging",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RequestIPAddress = table.Column<string>(type: "text", nullable: true),
                    RequestProtocol = table.Column<string>(type: "text", nullable: true),
                    RequestScheme = table.Column<string>(type: "text", nullable: true),
                    RequestMethod = table.Column<string>(type: "text", nullable: true),
                    RequestBody = table.Column<string>(type: "text", nullable: true),
                    RequestPath = table.Column<string>(type: "text", nullable: true),
                    RequestPathBase = table.Column<string>(type: "text", nullable: true),
                    RequestQueryString = table.Column<string>(type: "text", nullable: true),
                    RequestQuery = table.Column<string>(type: "text", nullable: true),
                    RequestRouteValues = table.Column<string>(type: "text", nullable: true),
                    RequestHost = table.Column<string>(type: "text", nullable: true),
                    RequestHasFormContentType = table.Column<bool>(type: "boolean", nullable: true),
                    RequestCookies = table.Column<string>(type: "text", nullable: true),
                    RequestContentType = table.Column<string>(type: "text", nullable: true),
                    RequestContentLength = table.Column<long>(type: "bigint", nullable: true),
                    RequestHeaders = table.Column<string>(type: "text", nullable: true),
                    RequestIsHttps = table.Column<bool>(type: "boolean", nullable: true),
                    RequestUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "integer", nullable: true),
                    ResponseHeaders = table.Column<string>(type: "text", nullable: true),
                    ResponseCookies = table.Column<string>(type: "text", nullable: true),
                    ResponseContentType = table.Column<string>(type: "text", nullable: true),
                    ResponseContentLength = table.Column<long>(type: "bigint", nullable: true),
                    ResponseTotalMilliseconds = table.Column<long>(type: "bigint", nullable: true),
                    ResponseBody = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebRequestMessage", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogMessage",
                schema: "Logging");

            migrationBuilder.DropTable(
                name: "WebRequestMessage",
                schema: "Logging");
        }
    }
}
