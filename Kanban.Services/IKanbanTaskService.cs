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
        Task<KanbanTaskDTO> GetAllKanbanTasks();
        Task<TaskWithUserDTO> GetSingleKanbanTask(int kanbanTaskId);
        Task<ResultDTO> DeleteKanbanTask(int kanbanTaskId);
        Task<ResultDTO> PatchKanbanTask(int kanbanTaskId, KanbanTaskVM patchKanbanTaskVM);
        Task<ResultDTO> PatchStatus(int kanbanTaskId, PatchKanbanTaskVM patchSingleKanbanTaskVM);
        Task<ResultDTO> PatchProgressStatus(int kanbanTaskId, PatchProgressStatusVM progressStatusVM);
        Task<ResultDTO> AddKanbanTaskWithPriority(KanbanTaskWithPriorityVM kanbanTaskWithPriorityVM);
        Task<TasksWithProrityListDTO> AllTasksWithSamePriority();
    }
}
