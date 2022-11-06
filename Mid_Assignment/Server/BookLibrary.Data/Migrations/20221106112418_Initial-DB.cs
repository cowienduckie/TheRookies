using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Data.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
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
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => new { x.BooksId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRequests",
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
                    table.PrimaryKey("PK_BorrowRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowRequests_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowRequests_Users_RequestedBy",
                        column: x => x.RequestedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRequestDetails",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    BorrowRequestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRequestDetails", x => new { x.BooksId, x.BorrowRequestsId });
                    table.ForeignKey(
                        name: "FK_BorrowRequestDetails_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowRequestDetails_BorrowRequests_BorrowRequestsId",
                        column: x => x.BorrowRequestsId,
                        principalTable: "BorrowRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
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
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Science" },
                    { 3, "Technology" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Normal 1", "normie1", 0, "normie1" },
                    { 2, "Normal 2", "normie2", 0, "normie2" },
                    { 3, "Super 1", "supreme2", 1, "supreme1" },
                    { 4, "Super 2", "supreme2", 1, "supreme1" }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "BooksId", "CategoriesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 3 }
                });

            migrationBuilder.InsertData(
                table: "BorrowRequests",
                columns: new[] { "Id", "ApprovedAt", "ApprovedBy", "RequestedAt", "RequestedBy", "Status" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2022, 11, 6, 18, 24, 18, 473, DateTimeKind.Local).AddTicks(6761), 1, 0 },
                    { 2, new DateTime(2022, 11, 6, 18, 24, 18, 473, DateTimeKind.Local).AddTicks(6776), 3, new DateTime(2022, 11, 6, 18, 24, 18, 473, DateTimeKind.Local).AddTicks(6775), 1, 1 },
                    { 3, new DateTime(2022, 11, 6, 18, 24, 18, 473, DateTimeKind.Local).AddTicks(6781), 4, new DateTime(2022, 11, 6, 18, 24, 18, 473, DateTimeKind.Local).AddTicks(6780), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "BorrowRequestDetails",
                columns: new[] { "BooksId", "BorrowRequestsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 3 },
                    { 4, 2 },
                    { 4, 3 },
                    { 5, 2 },
                    { 6, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoriesId",
                table: "BookCategories",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequestDetails_BorrowRequestsId",
                table: "BorrowRequestDetails",
                column: "BorrowRequestsId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequests_ApprovedBy",
                table: "BorrowRequests",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequests_RequestedBy",
                table: "BorrowRequests",
                column: "RequestedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "BorrowRequestDetails");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BorrowRequests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
