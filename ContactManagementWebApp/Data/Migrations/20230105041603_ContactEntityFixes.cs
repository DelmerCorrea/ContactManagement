using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagementWebApp.Data.Migrations
{
    public partial class ContactEntityFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Contacts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Contacts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62b41e1f-6f51-4b55-803d-d0ede094f1ad", "AQAAAAEAACcQAAAAEN1QQKBMKNYmgbpYZ8pwBoypJDEFVDSnW+DHI1weDycwuvPutir+UGi4Fy4VkY1qQg==", "516b917a-a95e-4af2-825f-9498f0b5c798" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "UpdatedBy",
                keyValue: null,
                column: "UpdatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Contacts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "DeletedBy",
                keyValue: null,
                column: "DeletedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Contacts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c14bf3bb-2022-4620-86b4-a7803b4187cb", "AQAAAAEAACcQAAAAEHCs/SXiOkaX6Av92Iq2LGNAkb/PklBCQSe1tM6c56PpP466JrazLPG9X4cSqRu4vg==", "28eb9b13-1b08-4c7a-9a74-759c0e6c2059" });
        }
    }
}
