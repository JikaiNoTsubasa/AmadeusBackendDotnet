using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ama_back_api.Migrations
{
    /// <inheritdoc />
    public partial class cat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmaCategoryAmaEntity",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    EntitiesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmaCategoryAmaEntity", x => new { x.CategoriesId, x.EntitiesId });
                    table.ForeignKey(
                        name: "FK_AmaCategoryAmaEntity_AmaEntity_EntitiesId",
                        column: x => x.EntitiesId,
                        principalTable: "AmaEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmaCategoryAmaEntity_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AmaCategoryAmaEntity_EntitiesId",
                table: "AmaCategoryAmaEntity",
                column: "EntitiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmaCategoryAmaEntity");
        }
    }
}
