using Kanban.Model.DbModels;
using Kanban.Model.Models.Request;
using Kanban.Model.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Service
{
    public interface IUserService
    {
        Task<ResultDTO> AddUser(UserWithoutIdVM userVM);
        Task<UserDTO> GetAllUsers();
        Task<ResultDTO> AddTaskWithUser(AddTaskWithUserVM addTaskWithUserVM);
        Task<ResultDTO> PatchTaskWithUser(TaskWithUsersVM taskToUsersVM);

        //------------------------------ Interfejsy używane do poprzednich etapów projektu --------------------------


        // Task<UserDTO> GetSingleUser(int userId);
        //Task<ResultDTO> DeleteUser(int userId);
        // Task<ResultDTO> PatchUser(int userId, UserVM userVM);
        // Task<ResultDTO> AssignTaskToUser(int taskId, int userId);
        //  Task<UsersTasksListDTO> GetAllTasksPerUser();
        //   Task<ResultDTO> DeleteTaskFromUser(int userId, int taskId);
        //  Task<TaskWithUserListDTO> GetAllUsersPerTask();

    }
}
