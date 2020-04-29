using Kanban.Model;
using Kanban.Model.DbModels;
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
        private readonly IRepository<UserTask> _usertaskrepo;
        private readonly IRepository<User> _userrepo;

        public KanbanTaskService(IRepository<KanbanTask> repo, IRepository<UserTask> usertaskrepo, IRepository<User> userrepo)
        {
            _repo = repo;
            _userrepo = userrepo;
            _usertaskrepo = usertaskrepo;
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
        public async Task<TaskWithUserDTO> GetSingleKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _repo.GetSingleEntity(x => x.Id == kanbanTaskId);
            if (kanbanTask == null)
                return null;
            else
            {
                var userTaskList = await _usertaskrepo.GetAll();
                var finalList = new List<UserWithoutIdDTO>();
                foreach (UserTask userTask in userTaskList)
                {
                    if (userTask.KanbanTaskId == kanbanTaskId)
                    {
                        var user = await _userrepo.GetSingleEntity(x => x.Id == userTask.UserId);
                        var userWithoutId = new UserWithoutIdDTO()
                        {
                            Name = user.Name,
                            Surname = user.Surname
                        };
                        finalList.Add(userWithoutId);
                    }
                }
                var finalTask = new TaskWithUserDTO()
                {
                    KanbanTask = kanbanTask,
                    UserList = finalList
                };
                return finalTask;
            }

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

        public async Task<ResultDTO> PatchProgressStatus(int kanbanTaskId, PatchProgressStatusVM progressStatusVM)
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
                if (progressStatusVM.ProgressStatus < 6)
                    kanbanTask.ProgressStatus = progressStatusVM.ProgressStatus;
                else
                    result.Response = "Progress Status not found";
                await _repo.Patch(kanbanTask);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;

        }
        public async Task<ResultDTO> AddKanbanTaskWithPriority(KanbanTaskWithPriorityVM kanbanTaskWithPriorityVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                KanbanTask task = (new KanbanTask
                {
                    Title = kanbanTaskWithPriorityVM.Title,
                    Description = kanbanTaskWithPriorityVM.Description,
                    Status = kanbanTaskWithPriorityVM.Status,
                    Priority = kanbanTaskWithPriorityVM.Priority
                });

                if (task.Priority < 1 || task.Priority >4)
                {
                    result.Response = "Invalid Priority";
                }
                else
                    await _repo.Add(task);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<TasksWithProrityListDTO> AllTasksWithSamePriority()
        {
            var maxStatus = 4;
            var minStatus = 1;
            var taskList = await _repo.GetAll();
            List<TasksWithProrityDTO> newList = new List<TasksWithProrityDTO>();
            var userTaskList = await _usertaskrepo.GetAll();
            for (var i = minStatus; i <= maxStatus; i++)
            {
                TasksWithProrityDTO newTask = (new TasksWithProrityDTO
                {
                    Priority = i,
                    KanbanTasksList = new List<AllTasksWithSamePriorityDTO>(),
                });
                foreach (KanbanTask task in taskList)
                {
                    if (task.Priority == i)
                    {
                        List<User> ifHasUser = new List<User>();
                        foreach (UserTask checkIfExists in userTaskList)
                        {
                            if (checkIfExists.KanbanTaskId == task.Id)
                            {
                                var user = await _userrepo.GetSingleEntity(x => x.Id == checkIfExists.UserId);
                                ifHasUser.Add(user);
                            } 
                        }
                        var temp = new AllTasksWithSamePriorityDTO()
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Description = task.Description,
                            Status = task.Status,
                            ProgressStatus = task.ProgressStatus,
                            UserList = ifHasUser
                        };
                        newTask.KanbanTasksList.Add(temp);
                    }
                }
                newList.Add(newTask);
            }
            var finalList = new TasksWithProrityListDTO()
            {
                TasksList = newList
            };
            return finalList;
        }
    }
}