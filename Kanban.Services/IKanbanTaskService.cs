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
    }
}
