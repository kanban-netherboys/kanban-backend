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
            var result = new ResultDTO() 
            {
                Response = null
            };
            try
            {
                await _repo.Add(new KanbanTask
                {
                    Title = addKanbanTaskVM.Title,
                    Description = addKanbanTaskVM.Description,
                    Status = addKanbanTaskVM.Status
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<KanbanTaskDTO> GetAllKanbanTasks()
        {
            var kanbanTaskList = new KanbanTaskDTO()
            {
                KanbanList = await _repo.GetAll()
            };
            return kanbanTaskList;
        }
        public async Task<KanbanTaskDTO> GetSingleKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = new KanbanTaskDTO()
            {
                SingleTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId)
            };
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
        public async Task<ResultDTO> PatchStatus(int kanbanTaskId, PatchKanbanTaskVM patchSingleKanbanTask)
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