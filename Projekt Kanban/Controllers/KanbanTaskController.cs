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
        public async Task<IActionResult> AddKanbanTask(KanbanTaskVM addKanbanTaskVM)
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
            if (kanbanTaskList == null)
                return BadRequest("No tasks to show");
            return Ok(kanbanTaskList);
        }

        [HttpGet ("BySingleTask")]
        public async Task<IActionResult> GetSingleKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _kanbanTaskService.GetSingleKanbanTask(kanbanTaskId);
            if (kanbanTask == null)
                return BadRequest("Task not found");
            return Ok(kanbanTask);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteKanbanTask(int kanbanTaskId)
        {
           var result =  await _kanbanTaskService.DeleteKanbanTask(kanbanTaskId);
            if (result.Response != null)
                return BadRequest("Task not found");
            return Ok("Task was deleted");
        }

        [HttpPatch ("PatchTask")]
        public async Task<IActionResult> PatchKanbanTask(int kanbanTaskId, KanbanTaskVM patchKanbanTaskVM)
        {
            var result = await _kanbanTaskService.PatchKanbanTask(kanbanTaskId, patchKanbanTaskVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Task was patched");
        }

        [HttpPatch ("PatchStatus")]
        public async Task<IActionResult> PatchStatus(int kanbanTaskId, PatchKanbanTaskVM patchSingleKanbanTaskVM)
        {
            var result = await _kanbanTaskService.PatchStatus(kanbanTaskId, patchSingleKanbanTaskVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Task status was patched");
        }
        [HttpPatch ("PatchProgressStatus")]
        public async Task<IActionResult> PatchProgressStatus(int kanbanTaskId, PatchProgressStatusVM progressStatusVM)
        {
            var result = await _kanbanTaskService.PatchProgressStatus(kanbanTaskId, progressStatusVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Progress status was patched");
        }

        [HttpPost ("AddKanbanTaskWithPriority")]
        public async Task<IActionResult> AddKanbanTaskWithPriority(KanbanTaskWithPriorityVM kanbanTaskWithPriorityVM)
        {
            var result = await _kanbanTaskService.AddKanbanTaskWithPriority(kanbanTaskWithPriorityVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Task was added");
        }
        [HttpGet ("AllTasksWithSamePriority")]
        public async Task<IActionResult> AllTasksWithSamePriority()
        {
            var taskWithProrityList = await _kanbanTaskService.AllTasksWithSamePriority();
            if (taskWithProrityList == null)
                return BadRequest("No tasks to show");
            return Ok(taskWithProrityList);
        }
    }
}