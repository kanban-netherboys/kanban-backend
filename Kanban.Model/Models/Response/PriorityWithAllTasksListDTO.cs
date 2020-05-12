using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class PriorityWithAllTasksListDTO
    {
        public List<PriorityWithAllTasksDTO> TasksList { get; set; }
    }
}
