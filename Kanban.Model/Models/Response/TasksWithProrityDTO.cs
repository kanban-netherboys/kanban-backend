using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class TasksWithProrityDTO
    {
        public int Priority { get; set; }
        public List<KanbanTask> KanbanTasksList { get; set; }
    }
}
