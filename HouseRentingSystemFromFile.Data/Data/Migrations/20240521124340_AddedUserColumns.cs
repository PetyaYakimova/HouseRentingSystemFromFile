using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystemFromFile.Data.Migrations
{
    public partial class AddedUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "869559ca-82db-4d4a-9773-af38dcb817bf", "Teodor", "Lesly", "AQAAAAEAACcQAAAAEEF4BNRoZFusxa6iRihCS6e6cCIrK58CLFtc1Hdo5BbVophQkx3HGmE6AHe1bi5Zdw==", "ae571be8-7573-45a6-99e2-9d94509f1034" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c32a27e8-78ac-4144-93ac-3f33e850e0df", "Linda", "Michaels", "AQAAAAEAACcQAAAAEEq8vJAgOl+BNJNDbnqa8uKkSfvzpBNZXbaDqFQqakXvwARFLLyreQxvRHOAZJwNXQ==", "8d8c519b-cf98-4a3d-a9c7-643b492c07d7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f18eb66-720d-4da8-b56f-ffdc7a84b4a9", "AQAAAAEAACcQAAAAEHeJTrd38oImEKJ4TfrVdWYdwmZIJmoRCYCF2gtzVkGXx1hLIGBd8PaSkYK7qpcyGA==", "5781be13-cb6e-40ac-9e1c-758ae36b5110" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "446fce1c-b290-403a-a0d0-6a549f03b17e", "AQAAAAEAACcQAAAAEHVuGyBUJDxYaLyQi376rnblaYBCIHOc5BwpzfTz5vvG7Di++e3O56A0xjUc/gFb3g==", "147401c8-ee63-4970-8212-d5df6b3eb132" });
        }
    }
}
