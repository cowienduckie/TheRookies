using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Data.Migrations
{
    public partial class FixSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Cover",
                value: "https://dummyimage.com/300x450/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Cover",
                value: "https://dummyimage.com/300x450/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cover",
                value: "https://dummyimage.com/300x450/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "Cover",
                value: "https://dummyimage.com/300x450/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "Cover",
                value: "https://dummyimage.com/300x450/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "Cover",
                value: "https://dummyimage.com/300x450/");

            migrationBuilder.UpdateData(
                table: "BorrowRequests",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestedAt",
                value: new DateTime(2022, 11, 11, 20, 32, 53, 590, DateTimeKind.Local).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "BorrowRequests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedAt", "RequestedAt" },
                values: new object[] { new DateTime(2022, 11, 11, 20, 32, 53, 590, DateTimeKind.Local).AddTicks(3724), new DateTime(2022, 11, 11, 20, 32, 53, 590, DateTimeKind.Local).AddTicks(3723) });

            migrationBuilder.UpdateData(
                table: "BorrowRequests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApprovedAt", "RequestedAt" },
                values: new object[] { new DateTime(2022, 11, 11, 20, 32, 53, 590, DateTimeKind.Local).AddTicks(3728), new DateTime(2022, 11, 11, 20, 32, 53, 590, DateTimeKind.Local).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "supreme1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Cover",
                value: "https://dummyimage.com/150x300/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Cover",
                value: "https://dummyimage.com/150x300/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cover",
                value: "https://dummyimage.com/150x300/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "Cover",
                value: "https://dummyimage.com/150x300/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "Cover",
                value: "https://dummyimage.com/150x300/");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "Cover",
                value: "https://dummyimage.com/150x300/");

            migrationBuilder.UpdateData(
                table: "BorrowRequests",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestedAt",
                value: new DateTime(2022, 11, 7, 2, 17, 6, 589, DateTimeKind.Local).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "BorrowRequests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovedAt", "RequestedAt" },
                values: new object[] { new DateTime(2022, 11, 7, 2, 17, 6, 589, DateTimeKind.Local).AddTicks(7983), new DateTime(2022, 11, 7, 2, 17, 6, 589, DateTimeKind.Local).AddTicks(7982) });

            migrationBuilder.UpdateData(
                table: "BorrowRequests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApprovedAt", "RequestedAt" },
                values: new object[] { new DateTime(2022, 11, 7, 2, 17, 6, 589, DateTimeKind.Local).AddTicks(7986), new DateTime(2022, 11, 7, 2, 17, 6, 589, DateTimeKind.Local).AddTicks(7986) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "supreme2");
        }
    }
}
