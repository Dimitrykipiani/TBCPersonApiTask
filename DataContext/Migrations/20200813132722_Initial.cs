using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    FistName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PrivateNumber = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(nullable: false),
                    PhoneType = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    RelatedPersonId = table.Column<int>(nullable: false),
                    RelationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relations_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "CityId", "FistName", "ImageURL", "LastName", "PrivateNumber", "Sex" },
                values: new object[] { 1, new DateTime(1732, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "George", "", "Washington", "04001099344", 1 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BirthDate", "CityId", "FistName", "ImageURL", "LastName", "PrivateNumber", "Sex" },
                values: new object[] { 2, new DateTime(1865, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Abraham", "", "Lincoln", "04041099344", 1 });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Number", "OwnerId", "PhoneType" },
                values: new object[,]
                {
                    { 1, "555111222", 1, 3 },
                    { 2, "555111223", 1, 2 },
                    { 3, "555111224", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Relations",
                columns: new[] { "Id", "PersonId", "RelatedPersonId", "RelationType" },
                values: new object[,]
                {
                    { 1, 1, 2, 1 },
                    { 2, 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_OwnerId",
                table: "PhoneNumbers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_PersonId",
                table: "Relations",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
