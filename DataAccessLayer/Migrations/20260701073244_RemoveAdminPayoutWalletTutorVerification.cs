using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminPayoutWalletTutorVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payouts");

            migrationBuilder.DropTable(
                name: "tutorbankaccounts");

            migrationBuilder.DropTable(
                name: "wallettransactions");

            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropColumn(
                name: "cccdbackpublicid",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "cccdfrontpublicid",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "certificatepublicid",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "isverified",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "nationalidnumber",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "revenue",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "verificationrejectreason",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "verificationreviewedat",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "verificationstatus",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "verificationsubmittedat",
                table: "tutors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cccdbackpublicid",
                table: "tutors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cccdfrontpublicid",
                table: "tutors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "certificatepublicid",
                table: "tutors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isverified",
                table: "tutors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nationalidnumber",
                table: "tutors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "rating",
                table: "tutors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "revenue",
                table: "tutors",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "verificationrejectreason",
                table: "tutors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "verificationreviewedat",
                table: "tutors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "verificationstatus",
                table: "tutors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "verificationsubmittedat",
                table: "tutors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tutorbankaccounts",
                columns: table => new
                {
                    tutorbankaccountid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tutorid = table.Column<int>(type: "integer", nullable: false),
                    accountholdername = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    accountnumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bankbin = table.Column<string>(type: "text", nullable: false),
                    bankname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    branchname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tutorbankaccounts", x => x.tutorbankaccountid);
                    table.ForeignKey(
                        name: "fk_tutorbankaccounts_tutors_tutorid",
                        column: x => x.tutorid,
                        principalTable: "tutors",
                        principalColumn: "tutorid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    walletid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tutorid = table.Column<int>(type: "integer", nullable: false),
                    balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    pendingbalance = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wallets", x => x.walletid);
                    table.ForeignKey(
                        name: "fk_wallets_tutors_tutorid",
                        column: x => x.tutorid,
                        principalTable: "tutors",
                        principalColumn: "tutorid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wallettransactions",
                columns: table => new
                {
                    wallettransactionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    walletid = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wallettransactions", x => x.wallettransactionid);
                    table.ForeignKey(
                        name: "fk_wallettransactions_wallets_walletid",
                        column: x => x.walletid,
                        principalTable: "wallets",
                        principalColumn: "walletid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payouts",
                columns: table => new
                {
                    payoutid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tutorid = table.Column<int>(type: "integer", nullable: false),
                    wallettransactionid = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    approvedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    paidat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    payoutmethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    requestedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payouts", x => x.payoutid);
                    table.ForeignKey(
                        name: "fk_payouts_tutors_tutorid",
                        column: x => x.tutorid,
                        principalTable: "tutors",
                        principalColumn: "tutorid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payouts_wallettransactions_wallettransactionid",
                        column: x => x.wallettransactionid,
                        principalTable: "wallettransactions",
                        principalColumn: "wallettransactionid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payouts_tutorid",
                table: "payouts",
                column: "tutorid");

            migrationBuilder.CreateIndex(
                name: "ix_payouts_wallettransactionid",
                table: "payouts",
                column: "wallettransactionid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tutorbankaccounts_tutorid",
                table: "tutorbankaccounts",
                column: "tutorid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_wallets_tutorid",
                table: "wallets",
                column: "tutorid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_wallettransactions_walletid",
                table: "wallettransactions",
                column: "walletid");
        }
    }
}
