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
        Task<ResultDTO> AddKanbanTask(KanbanTaskVM addKanbanTaskVM);
        Task<List<KanbanTask>> GetAllKanbanTasks();
        Task<KanbanTask> GetSingleKanbanTask(int kanbanTaskId);
        Task<ResultDTO> DeleteKanbanTask(int kanbanTaskId);
        Task<ResultDTO> PatchKanbanTask(int kanbanTaskId, KanbanTaskVM patchKanbanTaskVM);
        Task<ResultDTO> PatchStatus(int kanbanTaskId, KanbanTaskVM patchSingleKanbanTaskVM);
    }
}
