using Kanban.Model;
using Kanban.Model.DbModels;
using Kanban.Model.Models.Request;
using Kanban.Model.Models.Response;
using Kanban.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userrepo;
        private readonly IRepository<KanbanTask> _kanbantaskrepo;
        private readonly IRepository<UserTask> _usertaskrepo;
        public UserService(IRepository<User> repo, IRepository<KanbanTask> taskrepo, IRepository<UserTask> usertaskrepo)
        {
            _userrepo = repo;
            _kanbantaskrepo = taskrepo;
            _usertaskrepo = usertaskrepo;
        }

        public async Task<ResultDTO> AddUser(UserWithoutIdVM userVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _userrepo.Add(new User
                {
                    Name = userVM.Name,
                    Surname = userVM.Surname
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<UserDTO> GetAllUsers()
        {
            var userList = new UserDTO()
            {
                UserList = await _userrepo.GetAll()
            };
            return userList;
        }

        public async Task<ResultDTO> AddTaskWithUser(AddTaskWithUserVM addTaskWithUserVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var minPriority = 1;
                var maxPriority = 4;
                var task = (new KanbanTask
                {
                    Title = addTaskWithUserVM.Title,
                    Description = addTaskWithUserVM.Description,
                    Status = addTaskWithUserVM.Status,
                    Priority = addTaskWithUserVM.Priority,
                    Color = addTaskWithUserVM.Color,
                    Blocked = addTaskWithUserVM.Blocked
                });
                if (task.Priority < minPriority || task.Priority > maxPriority)
                {
                    result.Response = "Invalid Priority";
                }
                else
                {
                    await _kanbantaskrepo.Add(task);
                    var userList = addTaskWithUserVM.UserList;
                    foreach (UserWithoutIdDTO user in userList)
                    {
                        var findUser = await _userrepo.GetSingleEntity(x => x.Name == user.Name && x.Surname == user.Surname);
                        var usertask = new UserTask()
                        {
                            User = findUser,
                            KanbanTask = task
                        };
                        await _usertaskrepo.Add(usertask);
                    }
                }
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchTaskWithUser(TaskWithUsersVM taskToUsersVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var task = await _kanbantaskrepo.GetSingleEntity(x => x.Id == taskToUsersVM.Id);
                if (task != null)
                {
                    if (taskToUsersVM.Title != null)
                        task.Title = taskToUsersVM.Title;
                    if (taskToUsersVM.Description != null)
                        task.Description = taskToUsersVM.Description;
                    if (taskToUsersVM.Status != null)
                        task.Status = taskToUsersVM.Status;
                    if (taskToUsersVM.Color != null)
                        task.Color = taskToUsersVM.Color;
                    if (taskToUsersVM.Blocked != null)
                        task.Blocked = taskToUsersVM.Blocked;
                    await _kanbantaskrepo.Patch(task);

                    var userTaskList = await _usertaskrepo.GetAll();
                    foreach (UserTask userTask in userTaskList)
                    {
                        if (userTask.KanbanTaskId == task.Id)
                        {
                            await _usertaskrepo.Delete(userTask);
                        }
                    }
                    foreach (UserWithoutIdDTO user in taskToUsersVM.UserList)
                    {
                        var findUser = await _userrepo.GetSingleEntity(x => x.Name == user.Name && x.Surname == user.Surname);
                        if (findUser != null)
                        {
                            var usertask = new UserTask()
                            {
                                User = findUser,
                                KanbanTask = task
                            };
                            await _usertaskrepo.Add(usertask);
                        }
                        else
                            result.Response = "Task was patched, but one of the users does not exist";
                    }
                }
                else
                    result.Response = "Task does not exist";
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        //-------------------------------- Funkcje używane do poprzednich etapów projektu -------------------------



        //public async Task<UserDTO> GetSingleUser(int userId)
        //{
        //    var user = new UserDTO()
        //    {
        //        SingleUser = await _repo.GetSingleEntity(x => x.Id == userId)
        //    };
        //    if (user == null)
        //        return null;
        //    return user;
        //}

        //public async Task<ResultDTO> DeleteUser(int userId)
        //{
        //    var result = new ResultDTO()
        //    {
        //        Response = null
        //    };
        //    try
        //    {
        //        var user = await _repo.GetSingleEntity(x => x.Id == userId);
        //        if (user == null)
        //            result.Response = "User not found";
        //        await _repo.Delete(user);
        //    }
        //    catch (Exception e)
        //    {
        //        result.Response = e.Message;
        //        return result;
        //    }
        //    return result;
        //}

        //public async Task<ResultDTO> PatchUser(int userId, UserVM userVM)
        //{
        //    var result = new ResultDTO()
        //    {
        //        Response = null
        //    };
        //    try
        //    {
        //        var user = await _repo.GetSingleEntity(x => x.Id == userId);
        //        if (user == null)
        //            result.Response = "User not found";
        //        if (userVM.Name != null)
        //            user.Name = userVM.Name;
        //        if (userVM.Surname != null)
        //            user.Surname = userVM.Surname;
        //        await _repo.Patch(user);
        //    }
        //    catch (Exception e)
        //    {
        //        result.Response = e.Message;
        //        return result;
        //    }
        //    return result;
        //}

        //public async Task<ResultDTO> AssignTaskToUser(int taskId, int userId)
        //{
        //    var result = new ResultDTO()
        //    {
        //        Response = null
        //    };
        //    try
        //    {
        //        var user = await _repo.GetSingleEntity(x => x.Id == userId);
        //        var kanbanTask = await _taskrepo.GetSingleEntity(y => y.Id == taskId);
        //        if (user != null && kanbanTask != null)
        //        {
        //            var task = await _usertaskrepo.GetSingleEntity(x => x.UserId == userId && x.KanbanTaskId == taskId);
        //            if (task == null)
        //            {
        //                var usertask = new UserTask()
        //                {
        //                    User = user,
        //                    KanbanTask = kanbanTask
        //                };
        //                await _usertaskrepo.Add(usertask);
        //            }
        //            else
        //                result.Response = "Task already assigned to user";
        //        }
        //        else
        //            result.Response = "Task or user not found";
        //    }
        //    catch (Exception e)
        //    {
        //        result.Response = e.Message;
        //        return result;
        //    }
        //    return result;
        //}

        //public async Task<UsersTasksListDTO> GetAllTasksPerUser()
        //{
        //    var list = await _usertaskrepo.GetAll( x => x.User, y => y.KanbanTask);
        //    var newlist = list.GroupBy(x => x.UserId)
        //        .Select(y => new UserTasksDTO
        //        {
        //            User = y.Select(z => z.User).FirstOrDefault(x => x.Id == y.Key),
        //            KanbanTaskList = y.Select(z => z.KanbanTask).OrderBy( x => x.Id).ToList()
        //        }).OrderBy(x => x.User.Id).ToList(); 
        //    var userList = new UsersTasksListDTO()
        //    {
        //        UsersTasksList = newlist
        //    };
        //    return userList;
        //}

        //public async Task<ResultDTO> DeleteTaskFromUser(int userId, int taskId)
        //{
        //    var result = new ResultDTO()
        //    {
        //        Response = null
        //    };
        //    try
        //    {
        //        var task = await _usertaskrepo.GetSingleEntity(x => x.UserId == userId && x.KanbanTaskId == taskId);
        //        if (task == null)
        //            result.Response = "Task is not assigned to this user";
        //        await _usertaskrepo.Delete(task);
        //    }
        //    catch (Exception e)
        //    {
        //        result.Response = e.Message;
        //        return result;
        //    }
        //    return result;
        //}


        //public async Task<TaskWithUserListDTO> GetAllUsersPerTask()
        //{
        //    var list = await _taskrepo.GetAll();
        //    var userTaskList = await _usertaskrepo.GetAll();
        //    List<TaskWithUserDTO> newList = new List<TaskWithUserDTO>();
        //    foreach (KanbanTask task in list)
        //    {
        //        List<UserTask> ifHasUser = new List<UserTask>();
        //        foreach (UserTask checkIfExists in userTaskList)
        //        {
        //            if (checkIfExists.KanbanTaskId == task.Id)
        //                ifHasUser.Add(checkIfExists);
        //        }
        //        if (ifHasUser == null)
        //        {
        //            newList.Add(new TaskWithUserDTO
        //            {
        //                KanbanTask = task,
        //                UserList = new List<UserWithoutIdDTO>()
        //            });
        //        }
        //        else
        //        {
        //            List<UserWithoutIdDTO> finalList = new List<UserWithoutIdDTO>();
        //            foreach (UserTask userTask in userTaskList)
        //            {
        //                if (userTask.KanbanTaskId == task.Id)
        //                {
        //                    var user = await _repo.GetSingleEntity(x => x.Id == userTask.UserId);
        //                    var newUser = new UserWithoutIdDTO
        //                    {
        //                        Name = user.Name,
        //                        Surname = user.Surname
        //                    };
        //                    finalList.Add(newUser);
        //                }
        //            }
        //            newList.Add(new TaskWithUserDTO
        //            {
        //                KanbanTask = task,
        //                UserList = finalList
        //            });
        //        }
        //    }
        //    var userList = new TaskWithUserListDTO()
        //    {
        //        TaskWithUserList = newList
        //    };
        //    return userList;
        //}


    }
}
