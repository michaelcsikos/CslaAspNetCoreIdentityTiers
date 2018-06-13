using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CslaAspNetCoreIdentityTiers.DalEf.Migrations
{
    public partial class Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name:    "Role",
                columns: table => new
                {
                    Id               = table.Column<Guid>  (nullable: false),
                    Name             = table.Column<string>(maxLength: 256,   nullable: false),
                    NormalizedName   = table.Column<string>(maxLength: 256,   nullable: true),
                    ConcurrencyStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
