using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystemFromFile.Data.Migrations
{
    public partial class AddedRenterForeignKeyInHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ff6097a-f876-40e1-aefd-e710ecadc1a8", "AQAAAAEAACcQAAAAEIRKVBH8gtqY/aLWIU+s4cz7pRb6DiVayIBU3Lh01mJYkPp6cdT4nVtflwINtTLzWw==", "a7381efb-f1ac-4808-8ae6-d2bdede20e9a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8be94639-2c1d-437f-8df0-3eb5f4075427",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f5d1a61-bf2c-4f41-9473-7662cd044544", "AQAAAAEAACcQAAAAELEzUwdRe8M4Lxnh4CM21S6vFXtVLAwk+jKf/NYG3kO3guct80J7OZHHl+az1m++EQ==", "30267eec-678f-44c8-a78e-a646d1ae6be4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20abc214-629a-4dfc-8722-198c28b6aa40", "AQAAAAEAACcQAAAAEEqcHBLQi6D0SCHFbVIbCZu+Cqriq5zAl/Rk2AbVpiaiIiL9mjvLJRCKHl0RDjS6Zw==", "e7d8f32b-e792-4152-b1da-a999e15de015" });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_RenterId",
                table: "Houses",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_RenterId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_RenterId",
                table: "Houses");

            migrationBuilder.AlterColumn<string>(
                name: "RenterId",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47ba2bc0-d6ab-4ae7-8ee7-c31b0f20263e", "AQAAAAEAACcQAAAAEDBkcqRQXUYqiZBZcma8KvRG1WB8WZoMC7o9UQpbfcJJ5ZmJS82GEHmTbHO6JDGZJg==", "912ae66a-ff39-4810-813e-f5d0f77cd4a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8be94639-2c1d-437f-8df0-3eb5f4075427",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54eb782e-b8a4-496e-9b8c-ed449d57725a", "AQAAAAEAACcQAAAAEPrOwpKP5I7zWDd5qu+WE+Q4tdHF5Fc23kDokTpaXIr1MM5pCyLfVNWVtEVxG/tucg==", "ee96cb42-048a-4f1e-9294-21872c2441c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4dc273c-7cec-4b60-99bc-e46a6c35493e", "AQAAAAEAACcQAAAAECRFNpxz5QYFamh1W9lssLA2qlwMCupAeTLMsgQZwYY/10W5pIjqKeMXN2qUcW/82A==", "d7939a6f-a279-402f-9e1d-88040ddb5a79" });
        }
    }
}
