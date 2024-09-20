using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHall.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class UpdateHallConfiguration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropPrimaryKey(
				name: "PK_Reservations",
				table: "Reservations");

			migrationBuilder.DropPrimaryKey(
				name: "PK_HallEquipment",
				table: "HallEquipment");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Reservations",
				table: "Reservations",
				column: "Id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_HallEquipment",
				table: "HallEquipment",
				column: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropPrimaryKey(
				name: "PK_Reservations",
				table: "Reservations");

			migrationBuilder.DropPrimaryKey(
				name: "PK_HallEquipment",
				table: "HallEquipment");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Reservations",
				table: "Reservations",
				columns: new[] { "Id", "HallId" });

			migrationBuilder.AddPrimaryKey(
				name: "PK_HallEquipment",
				table: "HallEquipment",
				columns: new[] { "Id", "HallId" });
		}
	}
}
