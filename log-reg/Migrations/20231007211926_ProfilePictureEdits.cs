using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace log_reg.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HasProfileImage",
                table: "UsersObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasProfileImage",
                table: "UsersObjects");
        }
    }
}
