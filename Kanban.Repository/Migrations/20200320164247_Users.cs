using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanban.Repository.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "KanbanTasks",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KanbanTasks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "KanbanTasks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgressStatus",
                table: "KanbanTasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgressStatus",
                table: "KanbanTasks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "KanbanTasks",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KanbanTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "KanbanTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
