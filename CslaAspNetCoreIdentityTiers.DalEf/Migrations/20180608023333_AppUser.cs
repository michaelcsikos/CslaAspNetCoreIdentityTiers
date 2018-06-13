using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CslaAspNetCoreIdentityTiers.DalEf.Migrations
{
    public partial class AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name:    "AppUser",
                columns: table => new
                {
                    Id                   = table.Column<Guid>          (nullable: false),
                    UserName             = table.Column<string>        (maxLength: 256, nullable: false),
                    NormalizedUserName   = table.Column<string>        (maxLength: 256, nullable: true),
                    Email                = table.Column<string>        (maxLength: 256, nullable: false),
                    NormalizedEmail      = table.Column<string>        (maxLength: 256, nullable: true),
                    EmailConfirmed       = table.Column<bool>          (nullable: false),
                    PasswordHash         = table.Column<string>        (nullable: true),
                    SecurityStamp        = table.Column<string>        (nullable: true),
                    PhoneNumber          = table.Column<string>        (nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>          (nullable: false),
                    TwoFactorEnabled     = table.Column<bool>          (nullable: false),
                    LockoutEnd           = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled       = table.Column<bool>          (nullable: false),
                    AccessFailedCount    = table.Column<int>           (nullable: false),
                    ConcurrencyStamp     = table.Column<byte[]>        (rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
