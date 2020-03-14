using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class KanbanTaskDTO
    {
        public List<KanbanTask> KanbanList { get; set; }
        public KanbanTask SingleTask { get; set; }
    }
}
