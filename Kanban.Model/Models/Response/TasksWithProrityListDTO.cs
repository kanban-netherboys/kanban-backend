using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class TasksWithProrityListDTO
    {
        public List<TasksWithProrityDTO> TasksList { get; set; }
    }
}
