using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBricks.Logging.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class LoggingV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogMessage",
                columns: table => new
                {
                    Key = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Application = table.Column<string>(type: "TEXT", nullable: true),
                    Server = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    UserStorageKey = table.Column<string>(type: "TEXT", nullable: true),
                    Path = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Exception = table.Column<string>(type: "TEXT", nullable: true),
                    Properties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogMessage", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "WebRequestMessage",
                columns: table => new
                {
                    Key = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateDate = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Application = table.Column<string>(type: "TEXT", nullable: true),
                    Server = table.Column<string>(type: "TEXT", nullable: true),
                    UserStorageKey = table.Column<string>(type: "TEXT", nullable: true),
                    RequestIPAddress = table.Column<string>(type: "TEXT", nullable: true),
                    RequestProtocol = table.Column<string>(type: "TEXT", nullable: true),
                    RequestScheme = table.Column<string>(type: "TEXT", nullable: true),
                    RequestMethod = table.Column<string>(type: "TEXT", nullable: true),
                    RequestBody = table.Column<string>(type: "TEXT", nullable: true),
                    RequestPath = table.Column<string>(type: "TEXT", nullable: true),
                    RequestPathBase = table.Column<string>(type: "TEXT", nullable: true),
                    RequestQueryString = table.Column<string>(type: "TEXT", nullable: true),
                    RequestQuery = table.Column<string>(type: "TEXT", nullable: true),
                    RequestRouteValues = table.Column<string>(type: "TEXT", nullable: true),
                    RequestHost = table.Column<string>(type: "TEXT", nullable: true),
                    RequestHasFormContentType = table.Column<bool>(type: "INTEGER", nullable: true),
                    RequestCookies = table.Column<string>(type: "TEXT", nullable: true),
                    RequestContentType = table.Column<string>(type: "TEXT", nullable: true),
                    RequestContentLength = table.Column<long>(type: "INTEGER", nullable: true),
                    RequestHeaders = table.Column<string>(type: "TEXT", nullable: true),
                    RequestIsHttps = table.Column<bool>(type: "INTEGER", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "INTEGER", nullable: true),
                    ResponseHeaders = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseCookies = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseContentType = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseContentLength = table.Column<long>(type: "INTEGER", nullable: true),
                    ResponseTotalMilliseconds = table.Column<long>(type: "INTEGER", nullable: true),
                    ResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    Exception = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebRequestMessage", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogMessage_Application_Level_CreateDate",
                table: "LogMessage",
                columns: new[] { "Application", "Level", "CreateDate" });

            migrationBuilder.CreateIndex(
                name: "IX_WebRequestMessage_Application_UserStorageKey_CreateDate",
                table: "WebRequestMessage",
                columns: new[] { "Application", "UserStorageKey", "CreateDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogMessage");

            migrationBuilder.DropTable(
                name: "WebRequestMessage");
        }
    }
}
