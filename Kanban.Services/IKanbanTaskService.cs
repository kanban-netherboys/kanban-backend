using Kanban.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Services
{
    public interface IKanbanTaskService
    {
        Task AddKanbanTask(string title, string description, string status);
        Task<List<KanbanTask>> GetAllKanbanTasks();
        Task<KanbanTask> GetSingleKanbanTask(int kanbanTaskId);
        Task<string> DeleteKanbanTask(int kanbanTaskId);
        Task<string> PatchKanbanTask(int kanbanTaskId, string title, string description, string status);
        Task<string> PatchStatus(int kanbanTaskId, string status);
    }
}
