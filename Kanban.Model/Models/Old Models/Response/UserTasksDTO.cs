using Kanban.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class UserTasksDTO
    {
        public User User { get; set; }
        public List<KanbanTask> KanbanTaskList { get; set; }
    }
}
