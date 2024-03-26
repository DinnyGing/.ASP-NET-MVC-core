using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab3.Migrations
{
    /// <inheritdoc />
    public partial class AddVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ClientID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Procedures_ProcedureID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Masters_MasterID",
                table: "Procedures");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_ProcedureTypes_ProcedureTypeID",
                table: "Procedures");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureTypeID",
                table: "Procedures",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MasterID",
                table: "Procedures",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Procedures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureID",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ClientID",
                table: "Appointments",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Procedures_ProcedureID",
                table: "Appointments",
                column: "ProcedureID",
                principalTable: "Procedures",
                principalColumn: "ProcedureID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_Masters_MasterID",
                table: "Procedures",
                column: "MasterID",
                principalTable: "Masters",
                principalColumn: "MasterID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_ProcedureTypes_ProcedureTypeID",
                table: "Procedures",
                column: "ProcedureTypeID",
                principalTable: "ProcedureTypes",
                principalColumn: "ProcedureTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ClientID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Procedures_ProcedureID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Masters_MasterID",
                table: "Procedures");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_ProcedureTypes_ProcedureTypeID",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Procedures");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureTypeID",
                table: "Procedures",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MasterID",
                table: "Procedures",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureID",
                table: "Appointments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Appointments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ClientID",
                table: "Appointments",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Procedures_ProcedureID",
                table: "Appointments",
                column: "ProcedureID",
                principalTable: "Procedures",
                principalColumn: "ProcedureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_Masters_MasterID",
                table: "Procedures",
                column: "MasterID",
                principalTable: "Masters",
                principalColumn: "MasterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_ProcedureTypes_ProcedureTypeID",
                table: "Procedures",
                column: "ProcedureTypeID",
                principalTable: "ProcedureTypes",
                principalColumn: "ProcedureTypeID");
        }
    }
}
