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
        Task<ResultDTO> AddUser(UserVM userVM);
        Task<UserDTO> GetAllUsers();
    }
}
