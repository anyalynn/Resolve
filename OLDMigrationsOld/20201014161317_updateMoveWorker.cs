using Microsoft.EntityFrameworkCore.Migrations;

namespace Resolve.Migrations
{
    public partial class updateMoveWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CWorkerType",
                table: "MoveWorkerTracking");

            migrationBuilder.DropColumn(
                name: "HireType",
                table: "MoveWorkerTracking");

            migrationBuilder.DropColumn(
                name: "CWorkerType",
                table: "MoveWorker");

            migrationBuilder.DropColumn(
                name: "HireType",
                table: "MoveWorker");

            migrationBuilder.AddColumn<int>(
                name: "FWorkerType",
                table: "MoveWorkerTracking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FWorkerType",
                table: "MoveWorker",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FWorkerType",
                table: "MoveWorkerTracking");

            migrationBuilder.DropColumn(
                name: "FWorkerType",
                table: "MoveWorker");

            migrationBuilder.AddColumn<int>(
                name: "CWorkerType",
                table: "MoveWorkerTracking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HireType",
                table: "MoveWorkerTracking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CWorkerType",
                table: "MoveWorker",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HireType",
                table: "MoveWorker",
                type: "int",
                nullable: true);
        }
    }
}
