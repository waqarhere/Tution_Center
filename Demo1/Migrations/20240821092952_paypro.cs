using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo1.Migrations
{
    /// <inheritdoc />
    public partial class paypro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayId",
                table: "PaymentProcesses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProcesses_PayId",
                table: "PaymentProcesses",
                column: "PayId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProcesses_Pay_PayId",
                table: "PaymentProcesses",
                column: "PayId",
                principalTable: "Pay",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProcesses_Pay_PayId",
                table: "PaymentProcesses");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProcesses_PayId",
                table: "PaymentProcesses");

            migrationBuilder.DropColumn(
                name: "PayId",
                table: "PaymentProcesses");
        }
    }
}
