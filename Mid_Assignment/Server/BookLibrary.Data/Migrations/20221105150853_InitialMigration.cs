using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCategory_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedBy = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowRequest_User_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowRequest_User_RequestedBy",
                        column: x => x.RequestedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRequestDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BorrowRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRequestDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowRequestDetail_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowRequestDetail_BorrowRequest_BorrowRequestId",
                        column: x => x.BorrowRequestId,
                        principalTable: "BorrowRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Cover", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "https://dummyimage.com/150x300/", null, "Harry Potter" },
                    { 2, "https://dummyimage.com/150x300/", null, "Homo Sapiens" },
                    { 3, "https://dummyimage.com/150x300/", null, "Homo Deus" },
                    { 4, "https://dummyimage.com/150x300/", null, "Algorithm" },
                    { 5, "https://dummyimage.com/150x300/", null, "Clean Code" },
                    { 6, "https://dummyimage.com/150x300/", null, "Head First: Design Pattern" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Science" },
                    { 3, "Technology" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Normal 1", "normie1", 0, "normie1" },
                    { 2, "Normal 2", "normie2", 0, "normie2" },
                    { 3, "Super 1", "supreme2", 1, "supreme1" },
                    { 4, "Super 2", "supreme2", 1, "supreme1" }
                });

            migrationBuilder.InsertData(
                table: "BookCategory",
                columns: new[] { "Id", "BookId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 2 },
                    { 4, 4, 3 },
                    { 5, 5, 3 },
                    { 6, 6, 3 },
                    { 7, 3, 1 },
                    { 8, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "BorrowRequest",
                columns: new[] { "Id", "ApprovedAt", "ApprovedBy", "RequestedAt", "RequestedBy", "Status" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2022, 11, 5, 22, 8, 53, 477, DateTimeKind.Local).AddTicks(4828), 1, 0 },
                    { 2, new DateTime(2022, 11, 5, 22, 8, 53, 477, DateTimeKind.Local).AddTicks(4837), 3, new DateTime(2022, 11, 5, 22, 8, 53, 477, DateTimeKind.Local).AddTicks(4836), 1, 1 },
                    { 3, new DateTime(2022, 11, 5, 22, 8, 53, 477, DateTimeKind.Local).AddTicks(4840), 4, new DateTime(2022, 11, 5, 22, 8, 53, 477, DateTimeKind.Local).AddTicks(4840), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "BorrowRequestDetail",
                columns: new[] { "Id", "BookId", "BorrowRequestId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 2 },
                    { 5, 5, 2 },
                    { 6, 6, 2 },
                    { 7, 1, 2 },
                    { 8, 2, 2 },
                    { 9, 3, 3 },
                    { 10, 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_BookId",
                table: "BookCategory",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_CategoryId",
                table: "BookCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequest_ApprovedBy",
                table: "BorrowRequest",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequest_RequestedBy",
                table: "BorrowRequest",
                column: "RequestedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequestDetail_BookId",
                table: "BorrowRequestDetail",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequestDetail_BorrowRequestId",
                table: "BorrowRequestDetail",
                column: "BorrowRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "BorrowRequestDetail");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "BorrowRequest");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
