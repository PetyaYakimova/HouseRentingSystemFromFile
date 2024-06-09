using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystemFromFile.Data.Migrations
{
    public partial class AddedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47ba2bc0-d6ab-4ae7-8ee7-c31b0f20263e", "AQAAAAEAACcQAAAAEDBkcqRQXUYqiZBZcma8KvRG1WB8WZoMC7o9UQpbfcJJ5ZmJS82GEHmTbHO6JDGZJg==", "912ae66a-ff39-4810-813e-f5d0f77cd4a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4dc273c-7cec-4b60-99bc-e46a6c35493e", "AQAAAAEAACcQAAAAECRFNpxz5QYFamh1W9lssLA2qlwMCupAeTLMsgQZwYY/10W5pIjqKeMXN2qUcW/82A==", "d7939a6f-a279-402f-9e1d-88040ddb5a79" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8be94639-2c1d-437f-8df0-3eb5f4075427", 0, "54eb782e-b8a4-496e-9b8c-ed449d57725a", "admin@mail.com", false, "Great", "Admin", false, null, "admin@mail.com", "admin@mail.com", "AQAAAAEAACcQAAAAEPrOwpKP5I7zWDd5qu+WE+Q4tdHF5Fc23kDokTpaXIr1MM5pCyLfVNWVtEVxG/tucg==", null, false, "ee96cb42-048a-4f1e-9294-21872c2441c1", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 5, "+359123456789", "8be94639-2c1d-437f-8df0-3eb5f4075427" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8be94639-2c1d-437f-8df0-3eb5f4075427");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "869559ca-82db-4d4a-9773-af38dcb817bf", "AQAAAAEAACcQAAAAEEF4BNRoZFusxa6iRihCS6e6cCIrK58CLFtc1Hdo5BbVophQkx3HGmE6AHe1bi5Zdw==", "ae571be8-7573-45a6-99e2-9d94509f1034" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c32a27e8-78ac-4144-93ac-3f33e850e0df", "AQAAAAEAACcQAAAAEEq8vJAgOl+BNJNDbnqa8uKkSfvzpBNZXbaDqFQqakXvwARFLLyreQxvRHOAZJwNXQ==", "8d8c519b-cf98-4a3d-a9c7-643b492c07d7" });
        }
    }
}
