using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StarSystemApp.API.Migrations
{
    /// <inheritdoc />
    public partial class ds7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpaceObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<double>(type: "double precision", nullable: false),
                    Diameter = table.Column<double>(type: "double precision", nullable: false),
                    Mass = table.Column<double>(type: "double precision", nullable: false),
                    StarSystemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StarSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<double>(type: "double precision", nullable: false),
                    MassCenterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StarSystems_SpaceObjects_MassCenterId",
                        column: x => x.MassCenterId,
                        principalTable: "SpaceObjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpaceObjects_StarSystemId",
                table: "SpaceObjects",
                column: "StarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_StarSystems_MassCenterId",
                table: "StarSystems",
                column: "MassCenterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceObjects_StarSystems_StarSystemId",
                table: "SpaceObjects",
                column: "StarSystemId",
                principalTable: "StarSystems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceObjects_StarSystems_StarSystemId",
                table: "SpaceObjects");

            migrationBuilder.DropTable(
                name: "StarSystems");

            migrationBuilder.DropTable(
                name: "SpaceObjects");
        }
    }
}
