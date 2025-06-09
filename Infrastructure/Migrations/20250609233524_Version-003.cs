using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tenants");

            migrationBuilder.EnsureSchema(
                name: "System");

            migrationBuilder.EnsureSchema(
                name: "MultiTenancy");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "Tenants",
                newSchema: "MultiTenancy");

            migrationBuilder.RenameTable(
                name: "ErrorLogs",
                newName: "ErrorLogs",
                newSchema: "System");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "AuditLogs",
                newSchema: "System");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "MultiTenancy",
                table: "Tenants",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "MultiTenancy",
                table: "Tenants",
                newName: "LastModifiedOn");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                schema: "MultiTenancy",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "MultiTenancy",
                table: "Tenants",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "MultiTenancy",
                table: "Tenants",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "MultiTenancy",
                table: "Tenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "MultiTenancy",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "MultiTenancy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "MultiTenancy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "MultiTenancy",
                table: "Tenants");

            migrationBuilder.RenameTable(
                name: "Tenants",
                schema: "MultiTenancy",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "ErrorLogs",
                schema: "System",
                newName: "ErrorLogs");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                schema: "System",
                newName: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "LastModifiedOn",
                table: "Tenants",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Tenants",
                newName: "UpdatedBy");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tenants",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tenants",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETUTCDATE()");
        }
    }
}
