using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kanban.Model.Models.Request;
using Kanban.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Projekt_Kanban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser (UserVM userVM)
        {
            var result = await _userService.AddUser(userVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was added");      
        }
        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userList = await _userService.GetAllUsers();
            if (userList == null)
                return BadRequest("No users to show");
            return Ok(userList);
        }
        [HttpGet ("GetSingleUser")]
        public async Task<IActionResult> GetSingleUser(int userId)
        {
            var singleUser = await _userService.GetSingleUser(userId);
            if (singleUser.SingleUser == null)
                return BadRequest("User not found");
            return Ok(singleUser);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was deleted");
        }
        [HttpPatch]
        public async Task<IActionResult> PatchUser(int userId, UserVM userVM)
        {
            var result = await _userService.PatchUser(userId, userVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("User was patched");
        }
        [HttpPost ("AddTaskToUser")]
        public async Task<IActionResult> AssignTaskToUser(int taskId, int userId)
        {
            var result = await _userService.AssignTaskToUser(taskId, userId);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Task was assigned to user");

        }
    }
}