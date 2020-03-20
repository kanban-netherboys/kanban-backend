using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanban.Repository.Migrations
{
    public partial class Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    KanbanTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => new { x.UserId, x.KanbanTaskId });
                    table.ForeignKey(
                        name: "FK_UserTask_KanbanTasks_KanbanTaskId",
                        column: x => x.KanbanTaskId,
                        principalTable: "KanbanTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTask_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_KanbanTaskId",
                table: "UserTask",
                column: "KanbanTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTask");
        }
    }
}
