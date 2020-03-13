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
            await _repo.Add(new KanbanTask { Title = title, Description = description, Status = status });
        }
        public async Task<List<KanbanTask>> GetAllKanbanTasks()
        {
            var KanbanTaskList = await _repo.GetAll();
            return KanbanTaskList;
        }
        public async Task<KanbanTask> GetSingleKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
            {
                return null;
            }
            return kanbanTask;
        }
        public async Task<string> DeleteKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
                 return ("Task not found");
            await _repo.Delete(kanbanTask);
            return null;
        
        }
        public async Task<string> PatchKanbanTask(int kanbanTaskId, string title, string description, string status)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
            {
                return ("Brak takiego Taska");
            }
            if(title != null)
                kanbanTask.Title = title;
            if (description != null)
                kanbanTask.Description = description;
            if (status !=null)
                kanbanTask.Status = status;
            await _repo.Patch(kanbanTask);
            return null;
        }
        public async Task<string> PatchStatus(int kanbanTaskId, string status)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
            {
                return ("Brak takiego Taska");
            }
            if (status != null)
                kanbanTask.Status = status;
            await _repo.Patch(kanbanTask);
            return null;
        }
    }
}