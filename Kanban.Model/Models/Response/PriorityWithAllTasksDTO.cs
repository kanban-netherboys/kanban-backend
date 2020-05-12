using Kanban.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class PriorityWithAllTasksDTO
    {
        public int Priority { get; set; }
        public List<AllTasksWithSamePriorityDTO> KanbanTasksList { get; set; }
    }
}
