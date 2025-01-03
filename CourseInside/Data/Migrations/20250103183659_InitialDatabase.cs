using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseInside.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "57ba062a-4643-4cb1-b9ef-2a31d6a7f345", "admin@example.com", false, false, null, "Admin User", null, null, "AQAAAAIAAYagAAAAENJT3iXuiG79WgV8TbAoTCCg47KfL3cmtygA9Yrj+1Upes64Juj/eTK0urRpurAXzQ==", null, false, "Admin", "45dcf3bf-c2f8-482f-8622-ae74bc04aea1", false, "admin@example.com" },
                    { "2", 0, "20d6b4e0-2d7a-41dd-b7df-c72d892c9c35", "teacher@example.com", false, false, null, "Teacher User", null, null, "AQAAAAIAAYagAAAAEKlG1HiLVDD4Sp0RHTxXmAT6rav5Bc2cqbQ9K+ri6vrHPloOZRRxLE27KJ9O0yaKJQ==", null, false, "Teacher", "53a957bc-f663-4905-861f-f8a6817de763", false, "teacher@example.com" },
                    { "3", 0, "a5ac3cd7-9c97-4e92-8dc2-cad1122ebb33", "user@example.com", false, false, null, "Regular User", null, null, "AQAAAAIAAYagAAAAEMHnYu/mcztFwsazOF/5wtUmovvo4xGu8OgKoChc2rNBXl0vtLz5WRthFHptMyHcRA==", null, false, "User", "74d452f1-c370-48ad-9d1d-aea96d74c15d", false, "user@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Category A", "Description for Course 1", 110m, "Course 1" },
                    { 2, "Category A", "Description for Course 2", 120m, "Course 2" },
                    { 3, "Category A", "Description for Course 3", 130m, "Course 3" },
                    { 4, "Category A", "Description for Course 4", 140m, "Course 4" },
                    { 5, "Category A", "Description for Course 5", 150m, "Course 5" },
                    { 6, "Category A", "Description for Course 6", 160m, "Course 6" },
                    { 7, "Category A", "Description for Course 7", 170m, "Course 7" },
                    { 8, "Category A", "Description for Course 8", 180m, "Course 8" },
                    { 9, "Category A", "Description for Course 9", 190m, "Course 9" },
                    { 10, "Category A", "Description for Course 10", 200m, "Course 10" },
                    { 11, "Category A", "Description for Course 11", 210m, "Course 11" },
                    { 12, "Category A", "Description for Course 12", 220m, "Course 12" },
                    { 13, "Category A", "Description for Course 13", 230m, "Course 13" },
                    { 14, "Category A", "Description for Course 14", 240m, "Course 14" },
                    { 15, "Category A", "Description for Course 15", 250m, "Course 15" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CourseId", "OrderDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(865), "1" },
                    { 2, 2, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(879), "1" },
                    { 3, 3, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(892), "1" },
                    { 4, 4, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(893), "3" },
                    { 5, 5, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(895), "3" },
                    { 6, 6, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(897), "3" },
                    { 7, 7, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(898), "3" },
                    { 8, 8, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(900), "3" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate", "PaymentStatus" },
                values: new object[,]
                {
                    { 1, 110m, 1, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1332), "Completed" },
                    { 2, 120m, 2, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1335), "Completed" },
                    { 3, 130m, 3, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1337), "Completed" },
                    { 4, 140m, 4, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1347), "Completed" },
                    { 5, 150m, 5, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1350), "Completed" },
                    { 6, 160m, 6, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1353), "Completed" },
                    { 7, 170m, 7, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1363), "Completed" },
                    { 8, 180m, 8, new DateTime(2025, 1, 3, 18, 36, 59, 319, DateTimeKind.Utc).AddTicks(1367), "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourseId",
                table: "Orders",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
