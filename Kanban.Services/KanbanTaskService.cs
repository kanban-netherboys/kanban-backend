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
        public async Task<ResultDTO> AddKanbanTask(KanbanTaskVM addKanbanTaskVM)
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
                return null;
            return kanbanTask;
        }
        public async Task<ResultDTO> DeleteKanbanTask(int kanbanTaskId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
                if (kanbanTask == null)
                    result.Response = "Task not found";
                await _repo.Delete(kanbanTask);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;   
        }
        public async Task<ResultDTO> PatchKanbanTask(int kanbanTaskId, KanbanTaskVM patchKanbanTaskVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
                if (kanbanTask == null)
                    result.Response = "Task not found";
                if (patchKanbanTaskVM.Title != null)
                    kanbanTask.Title = patchKanbanTaskVM.Title;
                if (patchKanbanTaskVM.Description != null)
                    kanbanTask.Description = patchKanbanTaskVM.Description;
                if (patchKanbanTaskVM.Status != null)
                    kanbanTask.Status = patchKanbanTaskVM.Status;
                await _repo.Patch(kanbanTask);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<ResultDTO> PatchStatus(int kanbanTaskId, KanbanTaskVM patchSingleKanbanTask)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
                if (kanbanTask == null)
                    result.Response = "Task not found";
                if (patchSingleKanbanTask.Status != null)
                    kanbanTask.Status = patchSingleKanbanTask.Status;
                await _repo.Patch(kanbanTask);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
    }
}

// Database add-migration(tylko ja), update-database, drop-database 
// Dla patcha tak jak w add, delete żeby zwracał resultDTO, Gety zrobić KanbanTaskDTO, który zwraca listę, albo element
// Testy jednostkowe (Unit tests) - na przyszłość