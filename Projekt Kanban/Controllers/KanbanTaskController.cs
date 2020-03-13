using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kanban.Model;
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

        [HttpPost("{description},{status}")]
        public async Task<IActionResult> AddKanbanTask(string title, string description, string status)
        {
            await _kanbanTaskService.AddKanbanTask(title, description, status);
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
           var respond =  await _kanbanTaskService.DeleteKanbanTask(kanbanTaskId);
            if (respond == null)
                return Ok("Task was deleted");
            else
            return Ok(respond);
        }

        [HttpPatch ("{kanbanTaskId}")]
        public async Task<IActionResult> PatchKanbanTask(int kanbanTaskId, string title, string description, string status)
        {
            var respond = await _kanbanTaskService.PatchKanbanTask(kanbanTaskId, title, description, status);
            return Ok(respond);
        }
        [HttpPatch ("{kanbanTaskId},{status}")]
        public async Task<IActionResult> PatchStatus(int kanbanTaskId, string status)
        {
            var respond = await _kanbanTaskService.PatchStatus(kanbanTaskId, status);
            return Ok(respond);
        }
    }
}