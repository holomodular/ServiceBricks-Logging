using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBricks.Logging.SqlServer.Migrations
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Application = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserStorageKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RequestIPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestProtocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestScheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestPathBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestQueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestQuery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestRouteValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestHost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestHasFormContentType = table.Column<bool>(type: "bit", nullable: true),
                    RequestCookies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestContentLength = table.Column<long>(type: "bigint", nullable: true),
                    RequestHeaders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestIsHttps = table.Column<bool>(type: "bit", nullable: true),
                    RequestUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: true),
                    ResponseHeaders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseCookies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseContentLength = table.Column<long>(type: "bigint", nullable: true),
                    ResponseTotalMilliseconds = table.Column<long>(type: "bigint", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
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