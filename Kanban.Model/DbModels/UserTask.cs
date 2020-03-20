using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.DbModels
{
    public class UserTask
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTask KanbanTask { get; set; }
    }
}
