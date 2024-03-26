using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab3.Migrations
{
    /// <inheritdoc />
    public partial class Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Masters_MasterID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MasterID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "MasterID",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProcedureTypes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProcedureTypes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Procedures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MasterID",
                table: "Appointments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MasterID",
                table: "Appointments",
                column: "MasterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Masters_MasterID",
                table: "Appointments",
                column: "MasterID",
                principalTable: "Masters",
                principalColumn: "MasterID");
        }
    }
}
