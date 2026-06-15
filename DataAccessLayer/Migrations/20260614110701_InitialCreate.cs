using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appmetrics",
                columns: table => new
                {
                    appmetricid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    deviceid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    platform = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    appversion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appmetrics", x => x.appmetricid);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    subjectid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subjects", x => x.subjectid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    avatarpublicid = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    emailverificationtoken = table.Column<string>(type: "text", nullable: true),
                    emailverificationtokenexpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    refreshtoken = table.Column<string>(type: "text", nullable: true),
                    refreshtokenexpirytime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "conversations",
                columns: table => new
                {
                    conversationid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    lastmessageat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversations", x => x.conversationid);
                    table.ForeignKey(
                        name: "fk_conversations_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    studentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    grade = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    school = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.studentid);
                    table.ForeignKey(
                        name: "fk_students_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tutors",
                columns: table => new
                {
                    tutorid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    revenue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: false),
                    isverified = table.Column<bool>(type: "boolean", nullable: false),
                    verificationstatus = table.Column<string>(type: "text", nullable: false),
                    nationalidnumber = table.Column<string>(type: "text", nullable: true),
                    cccdfrontpublicid = table.Column<string>(type: "text", nullable: true),
                    cccdbackpublicid = table.Column<string>(type: "text", nullable: true),
                    certificatepublicid = table.Column<string>(type: "text", nullable: true),
                    verificationsubmittedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    verificationreviewedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    verificationrejectreason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tutors", x => x.tutorid);
                    table.ForeignKey(
                        name: "fk_tutors_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "conversationusers",
                columns: table => new
                {
                    conversationid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversationusers", x => new { x.conversationid, x.userid });
                    table.ForeignKey(
                        name: "fk_conversationusers_conversations_conversationid",
                        column: x => x.conversationid,
                        principalTable: "conversations",
                        principalColumn: "conversationid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_conversationusers_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    messageid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    conversationid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    isread = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.messageid);
                    table.ForeignKey(
                        name: "fk_messages_conversations_conversationid",
                        column: x => x.conversationid,
                        principalTable: "conversations",
                        principalColumn: "conversationid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messages_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "availabilities",
                columns: table => new
                {
                    availabilityid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tutorid = table.Column<int>(type: "integer", nullable: false),
                    subjectid = table.Column<int>(type: "integer", nullable: true),
                    dayofweek = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    mode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    offlineareas = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    starttime = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    endtime = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    startcoursetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endcoursetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    slot = table.Column<int>(type: "integer", nullable: false),
                    priceperslot = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_availabilities", x => x.availabilityid);
                    table.ForeignKey(
                        name: "fk_availabilities_subjects_subjectid",
                        column: x => x.subjectid,
                        principalTable: "subjects",
                        principalColumn: "subjectid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_availabilities_tutors_tutorid",
                        column: x => x.tutorid,
                        principalTable: "tutors",
                        principalColumn: "tutorid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tutorbankaccounts",
                columns: table => new
                {
                    tutorbankaccountid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tutorid = table.Column<int>(type: "integer", nullable: false),
                    bankname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    accountnumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    accountholdername = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    bankbin = table.Column<string>(type: "text", nullable: false),
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
                name: "tutorsubjects",
                columns: table => new
                {
                    subjectid = table.Column<int>(type: "integer", nullable: false),
                    tutorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tutorsubjects", x => new { x.subjectid, x.tutorid });
                    table.ForeignKey(
                        name: "fk_tutorsubjects_subjects_subjectid",
                        column: x => x.subjectid,
                        principalTable: "subjects",
                        principalColumn: "subjectid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tutorsubjects_tutors_tutorid",
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
                name: "bookings",
                columns: table => new
                {
                    bookingid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    availabilityid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: true),
                    studentid = table.Column<int>(type: "integer", nullable: true),
                    priceatbooking = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookings", x => x.bookingid);
                    table.ForeignKey(
                        name: "fk_bookings_availabilities_availabilityid",
                        column: x => x.availabilityid,
                        principalTable: "availabilities",
                        principalColumn: "availabilityid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_bookings_students_studentid",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "studentid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_bookings_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "materialsections",
                columns: table => new
                {
                    materialsectionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    availabilityid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    displayorder = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materialsections", x => x.materialsectionid);
                    table.ForeignKey(
                        name: "fk_materialsections_availabilities_availabilityid",
                        column: x => x.availabilityid,
                        principalTable: "availabilities",
                        principalColumn: "availabilityid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wallettransactions",
                columns: table => new
                {
                    wallettransactionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    walletid = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "lessons",
                columns: table => new
                {
                    lessonid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(type: "integer", nullable: false),
                    scheduletime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    meetinglink = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lessons", x => x.lessonid);
                    table.ForeignKey(
                        name: "fk_lessons_bookings_bookingid",
                        column: x => x.bookingid,
                        principalTable: "bookings",
                        principalColumn: "bookingid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    paymentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bookingid = table.Column<int>(type: "integer", nullable: false),
                    totalprice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    provider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    providerordercode = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    checkouturl = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    qrcode = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    paidat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.paymentid);
                    table.ForeignKey(
                        name: "fk_payments_bookings_bookingid",
                        column: x => x.bookingid,
                        principalTable: "bookings",
                        principalColumn: "bookingid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    materialid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    availabilityid = table.Column<int>(type: "integer", nullable: false),
                    materialsectionid = table.Column<int>(type: "integer", nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    fileurl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    filename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    contenttype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    filesize = table.Column<long>(type: "bigint", nullable: true),
                    materialtype = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materials", x => x.materialid);
                    table.ForeignKey(
                        name: "fk_materials_availabilities_availabilityid",
                        column: x => x.availabilityid,
                        principalTable: "availabilities",
                        principalColumn: "availabilityid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_materials_materialsections_materialsectionid",
                        column: x => x.materialsectionid,
                        principalTable: "materialsections",
                        principalColumn: "materialsectionid",
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
                    requestedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    approvedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    paidat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    payoutmethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
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
                name: "ix_appmetrics_type_deviceid",
                table: "appmetrics",
                columns: new[] { "type", "deviceid" });

            migrationBuilder.CreateIndex(
                name: "ix_availabilities_subjectid",
                table: "availabilities",
                column: "subjectid");

            migrationBuilder.CreateIndex(
                name: "ix_availabilities_tutorid_dayofweek",
                table: "availabilities",
                columns: new[] { "tutorid", "dayofweek" });

            migrationBuilder.CreateIndex(
                name: "ix_bookings_availabilityid_studentid_status",
                table: "bookings",
                columns: new[] { "availabilityid", "studentid", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_bookings_studentid",
                table: "bookings",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_userid",
                table: "bookings",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_conversations_userid",
                table: "conversations",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_conversationusers_userid",
                table: "conversationusers",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_lessons_bookingid_scheduletime",
                table: "lessons",
                columns: new[] { "bookingid", "scheduletime" });

            migrationBuilder.CreateIndex(
                name: "ix_materials_availabilityid_materialsectionid_createdat",
                table: "materials",
                columns: new[] { "availabilityid", "materialsectionid", "createdat" });

            migrationBuilder.CreateIndex(
                name: "ix_materials_materialsectionid",
                table: "materials",
                column: "materialsectionid");

            migrationBuilder.CreateIndex(
                name: "ix_materialsections_availabilityid_displayorder",
                table: "materialsections",
                columns: new[] { "availabilityid", "displayorder" });

            migrationBuilder.CreateIndex(
                name: "ix_messages_conversationid",
                table: "messages",
                column: "conversationid");

            migrationBuilder.CreateIndex(
                name: "ix_messages_userid",
                table: "messages",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_payments_bookingid",
                table: "payments",
                column: "bookingid");

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
                name: "ix_students_userid",
                table: "students",
                column: "userid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tutorbankaccounts_tutorid",
                table: "tutorbankaccounts",
                column: "tutorid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tutors_userid",
                table: "tutors",
                column: "userid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tutorsubjects_tutorid",
                table: "tutorsubjects",
                column: "tutorid");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appmetrics");

            migrationBuilder.DropTable(
                name: "conversationusers");

            migrationBuilder.DropTable(
                name: "lessons");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "payouts");

            migrationBuilder.DropTable(
                name: "tutorbankaccounts");

            migrationBuilder.DropTable(
                name: "tutorsubjects");

            migrationBuilder.DropTable(
                name: "materialsections");

            migrationBuilder.DropTable(
                name: "conversations");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "wallettransactions");

            migrationBuilder.DropTable(
                name: "availabilities");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "tutors");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
