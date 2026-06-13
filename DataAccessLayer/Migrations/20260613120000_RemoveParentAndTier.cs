using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    [Migration("20260613120000_RemoveParentAndTier")]
    public partial class RemoveParentAndTier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bookings_parents_parentid",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "fk_favoritetutors_parents_parentid",
                table: "favoritetutors");

            migrationBuilder.DropForeignKey(
                name: "fk_parents_users_userid",
                table: "parents");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_parents_parentid",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_students_parents_parentid",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "fk_tutors_tiers_tierid",
                table: "tutors");

            migrationBuilder.DropTable(
                name: "parents");

            migrationBuilder.DropTable(
                name: "tiers");

            migrationBuilder.DropIndex(
                name: "ix_bookings_parentid",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "ix_favoritetutors_parentid_tutorid",
                table: "favoritetutors");

            migrationBuilder.DropIndex(
                name: "ix_reviews_parentid",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "ix_students_parentid",
                table: "students");

            migrationBuilder.DropIndex(
                name: "ix_tutors_tierid",
                table: "tutors");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "favoritetutors");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "students");

            migrationBuilder.DropColumn(
                name: "tierid",
                table: "tutors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "parentid",
                table: "bookings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parentid",
                table: "favoritetutors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parentid",
                table: "reviews",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parentid",
                table: "students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tierid",
                table: "tutors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "parents",
                columns: table => new
                {
                    parentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parents", x => x.parentid);
                    table.ForeignKey(
                        name: "fk_parents_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tiers",
                columns: table => new
                {
                    tierid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currentstreak = table.Column<int>(type: "integer", nullable: false),
                    rate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tiers", x => x.tierid);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bookings_parentid",
                table: "bookings",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_favoritetutors_parentid_tutorid",
                table: "favoritetutors",
                columns: new[] { "parentid", "tutorid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_parents_userid",
                table: "parents",
                column: "userid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_reviews_parentid",
                table: "reviews",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_students_parentid",
                table: "students",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_tutors_tierid",
                table: "tutors",
                column: "tierid");

            migrationBuilder.AddForeignKey(
                name: "fk_bookings_parents_parentid",
                table: "bookings",
                column: "parentid",
                principalTable: "parents",
                principalColumn: "parentid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_favoritetutors_parents_parentid",
                table: "favoritetutors",
                column: "parentid",
                principalTable: "parents",
                principalColumn: "parentid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_parents_parentid",
                table: "reviews",
                column: "parentid",
                principalTable: "parents",
                principalColumn: "parentid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_students_parents_parentid",
                table: "students",
                column: "parentid",
                principalTable: "parents",
                principalColumn: "parentid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_tutors_tiers_tierid",
                table: "tutors",
                column: "tierid",
                principalTable: "tiers",
                principalColumn: "tierid",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
