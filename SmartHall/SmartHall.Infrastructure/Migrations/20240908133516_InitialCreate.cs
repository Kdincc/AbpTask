using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHall.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Halls",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Capacity = table.Column<int>(type: "int", nullable: false),
					BaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Halls", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "HallEquipmentTypes",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_HallEquipmentTypes", x => x.Id);
					table.ForeignKey(
						name: "FK_HallEquipmentTypes_Halls_HallId",
						column: x => x.HallId,
						principalTable: "Halls",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Reservations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
					HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Reservations", x => x.Id);
					table.ForeignKey(
						name: "FK_Reservations_Halls_HallId",
						column: x => x.HallId,
						principalTable: "Halls",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_HallEquipmentTypes_HallId",
				table: "HallEquipmentTypes",
				column: "HallId");

			migrationBuilder.CreateIndex(
				name: "IX_Reservations_HallId",
				table: "Reservations",
				column: "HallId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "HallEquipmentTypes");

			migrationBuilder.DropTable(
				name: "Reservations");

			migrationBuilder.DropTable(
				name: "Halls");
		}
	}
}
