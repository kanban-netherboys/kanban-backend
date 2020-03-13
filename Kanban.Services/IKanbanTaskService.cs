using Kanban.Model;
using Kanban.Model.Models.Request;
using Kanban.Model.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Services
{
    public interface IKanbanTaskService
    {
        Task<ResultDTO> AddKanbanTask(AddKanbanTaskVM addKanbanTaskVM);
        Task<List<KanbanTask>> GetAllKanbanTasks();
        Task<KanbanTask> GetSingleKanbanTask(int kanbanTaskId);
        Task<bool> DeleteKanbanTask(int kanbanTaskId);
        Task<string> PatchKanbanTask(int kanbanTaskId, string title, string description, string status);
        Task<string> PatchStatus(int kanbanTaskId, string status);
    }
}
