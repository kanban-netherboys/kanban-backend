using Kanban.Model;
using Kanban.Model.Models.Request;
using Kanban.Model.Models.Response;
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
        public async Task<ResultDTO> AddKanbanTask(AddKanbanTaskVM addKanbanTaskVM)
        {
            // Tworzymy puste DTO, żeby zwrócić do kontrolera
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                // Próbuję dodać, na podstawie modelu z VM
                await _repo.Add(new KanbanTask
                {
                    Title = addKanbanTaskVM.Title,
                    Description = addKanbanTaskVM.Description,
                    Status = addKanbanTaskVM.Status
                });
            }
            // Jeżeli nie wyjdzie, to wyłapujemy wyjątek e
            catch (Exception e)
            {
                // I zapisujemy message tego błędu do response i to returnujemy
                result.Response = e.Message;
                return result;
            }
           // Jak sie dodało, to returnujemy nic
            return result;
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
        public async Task<bool> DeleteKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
                 return false;
            await _repo.Delete(kanbanTask);
            return true;
        
        }
        public async Task<string> PatchKanbanTask(int kanbanTaskId, string title, string description, string status)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
            {
                return ("Brak takiego Taska");
            }
            if (title != null)
                kanbanTask.Title = title;
            if (description != null)
                kanbanTask.Description = description;
            if (status != null)
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

// Database add-migration(tylko ja), update-database, drop-database 
// Dla patcha tak jak w add, delete żeby zwracał resultDTO, Gety zrobić KanbanTaskDTO, który zwraca listę, albo element