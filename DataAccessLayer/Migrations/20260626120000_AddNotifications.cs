using System;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(EduNestDbContext))]
    [Migration("20260626120000_AddNotifications")]
    public partial class AddNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    notificationid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    referencekey = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    bookingid = table.Column<int>(type: "integer", nullable: true),
                    lessonid = table.Column<int>(type: "integer", nullable: true),
                    availabilityid = table.Column<int>(type: "integer", nullable: true),
                    materialid = table.Column<int>(type: "integer", nullable: true),
                    paymentid = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isread = table.Column<bool>(type: "boolean", nullable: false),
                    readat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.notificationid);
                    table.ForeignKey(
                        name: "fk_notifications_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_notifications_userid_isread_createdat",
                table: "notifications",
                columns: new[] { "userid", "isread", "createdat" });

            migrationBuilder.CreateIndex(
                name: "ix_notifications_userid_type_referencekey",
                table: "notifications",
                columns: new[] { "userid", "type", "referencekey" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications");
        }
    }
}
