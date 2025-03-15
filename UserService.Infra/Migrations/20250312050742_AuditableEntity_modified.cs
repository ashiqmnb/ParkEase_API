using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuditableEntity_modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated_on",
                table: "Users",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "Updated_by",
                table: "Users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Is_deleted",
                table: "Users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Created_on",
                table: "Users",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Created_by",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Updated_on",
                table: "Companies",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "Updated_by",
                table: "Companies",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Is_deleted",
                table: "Companies",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Created_on",
                table: "Companies",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Created_by",
                table: "Companies",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Updated_on",
                table: "Admins",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "Updated_by",
                table: "Admins",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Is_deleted",
                table: "Admins",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Created_on",
                table: "Admins",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Created_by",
                table: "Admins",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Updated_on",
                table: "Addresses",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "Updated_by",
                table: "Addresses",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Is_deleted",
                table: "Addresses",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Created_on",
                table: "Addresses",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Created_by",
                table: "Addresses",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Users",
                newName: "Updated_on");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Users",
                newName: "Updated_by");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "Is_deleted");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Users",
                newName: "Created_on");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "Created_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Companies",
                newName: "Updated_on");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Companies",
                newName: "Updated_by");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Companies",
                newName: "Is_deleted");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Companies",
                newName: "Created_on");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Companies",
                newName: "Created_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Admins",
                newName: "Updated_on");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Admins",
                newName: "Updated_by");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Admins",
                newName: "Is_deleted");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Admins",
                newName: "Created_on");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Admins",
                newName: "Created_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Addresses",
                newName: "Updated_on");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Addresses",
                newName: "Updated_by");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Addresses",
                newName: "Is_deleted");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Addresses",
                newName: "Created_on");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Addresses",
                newName: "Created_by");
        }
    }
}
