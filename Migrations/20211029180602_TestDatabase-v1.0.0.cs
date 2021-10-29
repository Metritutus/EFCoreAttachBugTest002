using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreAttachBugTest002.Migrations
{
    public partial class TestDatabasev100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Holder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MysteryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MysteryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Example",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IntegerValue = table.Column<int>(type: "INTEGER", nullable: true),
                    MysteryTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    HolderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Example", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Example_Holder_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Holder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Example_MysteryType_MysteryTypeId",
                        column: x => x.MysteryTypeId,
                        principalTable: "MysteryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MysteryType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Mystery1" });

            migrationBuilder.InsertData(
                table: "MysteryType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Mystery2" });

            migrationBuilder.CreateIndex(
                name: "IX_Example_HolderId",
                table: "Example",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Example_MysteryTypeId",
                table: "Example",
                column: "MysteryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Example");

            migrationBuilder.DropTable(
                name: "Holder");

            migrationBuilder.DropTable(
                name: "MysteryType");
        }
    }
}
