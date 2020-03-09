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
    }
}
