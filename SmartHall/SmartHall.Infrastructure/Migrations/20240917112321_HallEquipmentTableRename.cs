using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHall.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class HallEquipmentTableRename : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_HallEquipmentTypes_Halls_HallId",
				table: "HallEquipmentTypes");

			migrationBuilder.DropPrimaryKey(
				name: "PK_HallEquipmentTypes",
				table: "HallEquipmentTypes");

			migrationBuilder.RenameTable(
				name: "HallEquipmentTypes",
				newName: "HallEquipment");

			migrationBuilder.RenameIndex(
				name: "IX_HallEquipmentTypes_HallId",
				table: "HallEquipment",
				newName: "IX_HallEquipment_HallId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_HallEquipment",
				table: "HallEquipment",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_HallEquipment_Halls_HallId",
				table: "HallEquipment",
				column: "HallId",
				principalTable: "Halls",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_HallEquipment_Halls_HallId",
				table: "HallEquipment");

			migrationBuilder.DropPrimaryKey(
				name: "PK_HallEquipment",
				table: "HallEquipment");

			migrationBuilder.RenameTable(
				name: "HallEquipment",
				newName: "HallEquipmentTypes");

			migrationBuilder.RenameIndex(
				name: "IX_HallEquipment_HallId",
				table: "HallEquipmentTypes",
				newName: "IX_HallEquipmentTypes_HallId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_HallEquipmentTypes",
				table: "HallEquipmentTypes",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_HallEquipmentTypes_Halls_HallId",
				table: "HallEquipmentTypes",
				column: "HallId",
				principalTable: "Halls",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
