using Kanban.Model;
using Kanban.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Services
{
    public class KanbanTaskService : IKanbanTaskService
    {
        private readonly IRepository<KanbanTask> _repo;
        public KanbanTaskService(IRepository<KanbanTask> repo)
        {
            _repo = repo;
        }
        public async Task AddKanbanTask(string title, string description, string status)
        {
            await _repo.AddKanbanTask(new KanbanTask {Title = title, Description = description, Status = status });
        }
        public async Task<List<KanbanTask>> GetAllKanbanTasks()
        {
            var KanbanTaskList = await _repo.GetAllKanbanTasks();
            return KanbanTaskList;
        }
        public async Task<KanbanTask> GetSingleKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _repo.GetSingleKanbanTask(x => x.Id == kanbanTaskId);
            return kanbanTask;
        }
        public async Task DeleteKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _repo.GetSingleKanbanTask(x => x.Id == kanbanTaskId);
            await _repo.DeleteKanbanTask(kanbanTask);
        }
        public async Task<string> PatchKanbanTask(int kanbanTaskId, string title, string description, string status)
        {
            var kanbanTask = await _repo.GetSingleKanbanTask(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
            {
                return ("Brak takiego Taska");
            }
            kanbanTask.Title = title;
            kanbanTask.Description = description;
            kanbanTask.Status = status;
            await _repo.PatchKanbanTask(kanbanTask);
            return null;
        }
    }
}
