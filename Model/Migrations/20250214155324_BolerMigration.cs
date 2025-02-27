using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class BolerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactPerson = table.Column<string>(type: "text", nullable: true),
                    ContactPersonNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role);
                });

            migrationBuilder.CreateTable(
                name: "BoilerRoom",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    adress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoilerRoom", x => x.name);
                    table.ForeignKey(
                        name: "FK_BoilerRoom_Company_CompanyName",
                        column: x => x.CompanyName,
                        principalTable: "Company",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "Role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoilerDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoilerRoomName = table.Column<string>(type: "character varying(255)", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NominalValue = table.Column<int>(type: "integer", nullable: true),
                    Instruction = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Installed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoilerDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoilerDevices_BoilerRoom_BoilerRoomName",
                        column: x => x.BoilerRoomName,
                        principalTable: "BoilerRoom",
                        principalColumn: "name");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Role", "Name" },
                values: new object[,]
                {
                    { 0, "Admin" },
                    { 1, "Worker" },
                    { 2, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoilerDevices_BoilerRoomName",
                table: "BoilerDevices",
                column: "BoilerRoomName");

            migrationBuilder.CreateIndex(
                name: "IX_BoilerRoom_CompanyName",
                table: "BoilerRoom",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Company_name",
                table: "Company",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoilerDevices");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BoilerRoom");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
