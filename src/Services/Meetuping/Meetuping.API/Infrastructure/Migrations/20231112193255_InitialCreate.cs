using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventor.Services.Meetuping.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "meetuping");

            migrationBuilder.CreateSequence(
                name: "mateseq",
                schema: "meetuping",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "meetupseq",
                schema: "meetuping",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "meetupstatus",
                schema: "meetuping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meetupstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "meetuping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "meetups",
                schema: "meetuping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetupStatusId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    MeetupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_meetups_meetupstatus_MeetupStatusId",
                        column: x => x.MeetupStatusId,
                        principalSchema: "meetuping",
                        principalTable: "meetupstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mates",
                schema: "meetuping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetupId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    ApprovalAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    MateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mates_meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalSchema: "meetuping",
                        principalTable: "meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mates_MeetupId",
                schema: "meetuping",
                table: "mates",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_meetups_MeetupStatusId",
                schema: "meetuping",
                table: "meetups",
                column: "MeetupStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mates",
                schema: "meetuping");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "meetuping");

            migrationBuilder.DropTable(
                name: "meetups",
                schema: "meetuping");

            migrationBuilder.DropTable(
                name: "meetupstatus",
                schema: "meetuping");

            migrationBuilder.DropSequence(
                name: "mateseq",
                schema: "meetuping");

            migrationBuilder.DropSequence(
                name: "meetupseq",
                schema: "meetuping");
        }
    }
}
