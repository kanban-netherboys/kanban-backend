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

        [HttpGet("GetSingleTask")]
        public async Task<IActionResult> GetSingleKanbanTask(int kanbanTaskId)
        {
            var kanbanTask = await _kanbanTaskService.GetSingleKanbanTask(kanbanTaskId);
            if (kanbanTask == null)
                return BadRequest("Task not found");
            return Ok(kanbanTask);
        }
        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteKanbanTask(int kanbanTaskId)
        {
            var result = await _kanbanTaskService.DeleteKanbanTask(kanbanTaskId);
            if (result.Response != null)
                return BadRequest("Task not found");
            return Ok("Task was deleted");
        }
        [HttpPatch("PatchTaskStatus")]
        public async Task<IActionResult> PatchStatus(int kanbanTaskId, PatchKanbanTaskStatusVM patchKanbanTaskStatusVM)
        {
            var result = await _kanbanTaskService.PatchStatus(kanbanTaskId, patchKanbanTaskStatusVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Task status was patched");
        }
        [HttpPatch("PatchTaskProgressStatus")]
        public async Task<IActionResult> PatchProgressStatus(int kanbanTaskId, PatchKanbanTaskProgressStatusVM progressStatusVM)
        {
            var result = await _kanbanTaskService.PatchProgressStatus(kanbanTaskId, progressStatusVM);
            if (result.Response != null)
                return BadRequest(result);
            return Ok("Progress status was patched");
        }
        [HttpGet("AllTasksWithSamePriority")]
        public async Task<IActionResult> AllTasksWithSamePriority()
        {
            var taskWithProrityList = await _kanbanTaskService.AllTasksWithSamePriority();
            if (taskWithProrityList == null)
                return BadRequest("No tasks to show");
            return Ok(taskWithProrityList);
        }


        // --------------- Endpointy używane do poprzednich etapów projektu -------------------------------------------


        //[HttpPost]
        //public async Task<IActionResult> AddKanbanTask(KanbanTaskVM addKanbanTaskVM)
        //{
        //    var result = await _kanbanTaskService.AddKanbanTask(addKanbanTaskVM);
        //    if (result.Response != null)
        //        return BadRequest(result);
        //    return Ok("Task was added");
        //}

        //[HttpGet ("AllTasks")]
        //public async Task<IActionResult> GetAllKanbanTasks()
        //{
        //    var kanbanTaskList = await _kanbanTaskService.GetAllKanbanTasks();
        //    if (kanbanTaskList == null)
        //        return BadRequest("No tasks to show");
        //    return Ok(kanbanTaskList);
        //}

        //[HttpPatch ("PatchTask")]
        //public async Task<IActionResult> PatchKanbanTask(int kanbanTaskId, KanbanTaskVM patchKanbanTaskVM)
        //{
        //    var result = await _kanbanTaskService.PatchKanbanTask(kanbanTaskId, patchKanbanTaskVM);
        //    if (result.Response != null)
        //        return BadRequest(result);
        //    return Ok("Task was patched");
        //}

        //[HttpPost ("AddKanbanTaskWithPriority")]
        //public async Task<IActionResult> AddKanbanTaskWithPriority(KanbanTaskWithPriorityVM kanbanTaskWithPriorityVM)
        //{
        //    var result = await _kanbanTaskService.AddKanbanTaskWithPriority(kanbanTaskWithPriorityVM);
        //    if (result.Response != null)
        //        return BadRequest(result);
        //    return Ok("Task was added");
        //}

        //[HttpPatch ("PatchBlockedStatus")]
        //public async Task<IActionResult> PatchBlockedStatus(int kanbanTaskId, bool blockedStatus)
        //{
        //    var result = await _kanbanTaskService.PatchBlockedStatus(kanbanTaskId, blockedStatus);
        //    if (result.Response != null)
        //        return BadRequest(result);
        //    return Ok("Blocked status was patched");
        //}

        //[HttpPatch("PatchColor")]
        //public async Task<IActionResult> PatchColor(int kanbanTaskId, string color)
        //{
        //    var result = await _kanbanTaskService.PatchColor(kanbanTaskId, color);
        //    if (result.Response != null)
        //        return BadRequest(result);
        //    return Ok("Color was patched");
        //}
    }
}