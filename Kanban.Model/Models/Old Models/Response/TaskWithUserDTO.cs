using Kanban.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class TaskWithUserDTO
    {
        public KanbanTask KanbanTask { get; set; }
        public List<UserWithoutIdDTO> UserList { get; set; }
    }
}
