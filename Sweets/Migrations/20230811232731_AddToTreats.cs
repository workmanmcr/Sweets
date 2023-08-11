using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sweets.Migrations
{
    public partial class AddToTreats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treats_Flavors_FlavorId",
                table: "Treats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatFlavors",
                table: "TreatFlavors");

            migrationBuilder.DropIndex(
                name: "IX_TreatFlavors_TreatId",
                table: "TreatFlavors");

            migrationBuilder.AlterColumn<int>(
                name: "FlavorId",
                table: "Treats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TreatFlavorId",
                table: "TreatFlavors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TreatId1",
                table: "TreatFlavors",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatFlavors",
                table: "TreatFlavors",
                columns: new[] { "TreatId", "FlavorId" });

            migrationBuilder.CreateIndex(
                name: "IX_TreatFlavors_TreatId1",
                table: "TreatFlavors",
                column: "TreatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatFlavors_Treats_TreatId1",
                table: "TreatFlavors",
                column: "TreatId1",
                principalTable: "Treats",
                principalColumn: "TreatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treats_Flavors_FlavorId",
                table: "Treats",
                column: "FlavorId",
                principalTable: "Flavors",
                principalColumn: "FlavorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreatFlavors_Treats_TreatId1",
                table: "TreatFlavors");

            migrationBuilder.DropForeignKey(
                name: "FK_Treats_Flavors_FlavorId",
                table: "Treats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreatFlavors",
                table: "TreatFlavors");

            migrationBuilder.DropIndex(
                name: "IX_TreatFlavors_TreatId1",
                table: "TreatFlavors");

            migrationBuilder.DropColumn(
                name: "TreatId1",
                table: "TreatFlavors");

            migrationBuilder.AlterColumn<int>(
                name: "FlavorId",
                table: "Treats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TreatFlavorId",
                table: "TreatFlavors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreatFlavors",
                table: "TreatFlavors",
                column: "TreatFlavorId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatFlavors_TreatId",
                table: "TreatFlavors",
                column: "TreatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treats_Flavors_FlavorId",
                table: "Treats",
                column: "FlavorId",
                principalTable: "Flavors",
                principalColumn: "FlavorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
