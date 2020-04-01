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
        private readonly IRepository<User> _repo;
        private readonly IRepository<KanbanTask> _taskrepo;
        private readonly IRepository<UserTask> _usertaskrepo;
        public UserService(IRepository<User> repo, IRepository<KanbanTask> taskrepo, IRepository<UserTask> usertaskrepo)
        {
            _repo = repo;
            _taskrepo = taskrepo;
            _usertaskrepo = usertaskrepo;
        }

        public async Task<ResultDTO> AddUser(UserVM userVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _repo.Add(new User
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
                UserList = await _repo.GetAll()
            };
            return userList;
        }
        public async Task<UserDTO> GetSingleUser(int userId)
        {
            var user = new UserDTO()
            {
                SingleUser = await _repo.GetSingleEntity(x => x.Id == userId)
            };
            if (user == null)
                return null;
            return user;
        }
        public async Task<ResultDTO> DeleteUser(int userId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var user = await _repo.GetSingleEntity(x => x.Id == userId);
                if (user == null)
                    result.Response = "User not found";
                await _repo.Delete(user);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<ResultDTO> PatchUser(int userId, UserVM userVM)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var user = await _repo.GetSingleEntity(x => x.Id == userId);
                if (user == null)
                    result.Response = "User not found";
                if (userVM.Name != null)
                    user.Name = userVM.Name;
                if (userVM.Surname != null)
                    user.Surname = userVM.Surname;
                await _repo.Patch(user);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<ResultDTO> AssignTaskToUser(int taskId, int userId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var user = await _repo.GetSingleEntity(x => x.Id == userId);
                var kanbanTask = await _taskrepo.GetSingleEntity(y => y.Id == taskId);
                if (user != null && kanbanTask != null)
                {
                    var task = await _usertaskrepo.GetSingleEntity(x => x.UserId == userId && x.KanbanTaskId == taskId);
                    if (task == null)
                    {
                        var usertask = new UserTask()
                        {
                            User = user,
                            KanbanTask = kanbanTask
                        };
                        await _usertaskrepo.Add(usertask);
                    }
                    else
                        result.Response = "Task already assigned to user";
                }
                else
                    result.Response = "Task or user not found";
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<UsersTasksListDTO> GetAllTasksPerUser()
        {
            var list = await _usertaskrepo.GetAll( x => x.User, y => y.KanbanTask);

            var newlist = list.GroupBy(x => x.UserId)
                .Select(y => new UserTasksDTO
                {
                    User = y.Select(z => z.User).FirstOrDefault(x => x.Id == y.Key),
                    KanbanTaskList = y.Select(z => z.KanbanTask).OrderBy( x => x.Id).ToList()
                }).OrderBy(x => x.User.Id).ToList(); 

            var userList = new UsersTasksListDTO()
            {
                UsersTasksList = newlist
            };
            return userList;
        }

        public async Task<ResultDTO> DeleteTaskFromUser(int userId, int taskId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var task = await _usertaskrepo.GetSingleEntity(x => x.UserId == userId && x.KanbanTaskId == taskId);
                if (task == null)
                    result.Response = "Task is not assigned to this user";
                await _usertaskrepo.Delete(task);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }
        public async Task<ResultDTO> AddTaskToUser(TaskToUserVM taskToUser)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var task = (new KanbanTask
                {
                    Title = taskToUser.Title,
                    Description = taskToUser.Description,
                    Status = taskToUser.Status
                });
                await _taskrepo.Add(task);
                var findUser = await _repo.GetSingleEntity(x => x.Name == taskToUser.Name && x.Surname == taskToUser.Surname);
                var usertask = new UserTask()
                {
                    User = findUser,
                    KanbanTask = task
                };
                await _usertaskrepo.Add(usertask);
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
