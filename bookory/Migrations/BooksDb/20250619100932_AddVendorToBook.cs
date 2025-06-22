using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookory.Migrations.BooksDb
{
    /// <inheritdoc />
    public partial class AddVendorToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_VendorId",
                table: "Books",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Vendors_VendorId",
                table: "Books",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Vendors_VendorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_VendorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Books");
        }
    }
}
