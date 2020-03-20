using Kanban.Model;
using Kanban.Model.DbModels;
using Kanban.Model.Models.Request;
using Kanban.Model.Models.Response;
using Kanban.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;

        public UserService(IRepository<User> repo)
        {
            _repo = repo;
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
    }
}
