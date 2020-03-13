using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Kanban.Model;
using Kanban.Model.Models.Request;
using Kanban.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Projekt_Kanban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KanbanTaskController : ControllerBase
    {
        private readonly IKanbanTaskService _kanbanTaskService;

        public KanbanTaskController(IKanbanTaskService kanbanTaskService)
        {
            _kanbanTaskService = kanbanTaskService;
        }

        [HttpPost]
        public async Task<IActionResult> AddKanbanTask(AddKanbanTaskVM addKanbanTaskVM)
        {
            var result = await _kanbanTaskService.AddKanbanTask(addKanbanTaskVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Task was added");
        }
        [HttpGet ("AllTasks")]
        public async Task<IActionResult> GetAllKanbanTasks()
        {
            var kanbanTaskList = await _kanbanTaskService.GetAllKanbanTasks();
            return Ok(kanbanTaskList);
        }

        [HttpGet ("BySingleTask")]
        public async Task<IActionResult> GetSingleKanbanTask(int kanbanTaskId)
        {
            var task = await _kanbanTaskService.GetSingleKanbanTask(kanbanTaskId);
            if (task == null)
            {
                return BadRequest("Task not found");
            }
            return Ok(task);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteKanbanTask(int kanbanTaskId)
        {
           var isDeleted =  await _kanbanTaskService.DeleteKanbanTask(kanbanTaskId);
            if (isDeleted == false)
                return BadRequest("Task not found");

            return Ok("Task was deleted");
        }

        [HttpPatch ("{kanbanTaskId}")]
        public async Task<IActionResult> PatchKanbanTask(int kanbanTaskId, string title, string description, string status)
        {
            var respond = await _kanbanTaskService.PatchKanbanTask(kanbanTaskId, title, description, status);
            return Ok(respond);
        }
        [HttpPatch ("{kanbanTaskId}/{status}")]
        public async Task<IActionResult> PatchStatus(int kanbanTaskId, string status)
        {
            var respond = await _kanbanTaskService.PatchStatus(kanbanTaskId, status);
            return Ok(respond);
        }
    }
}