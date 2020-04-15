using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class TaskWithUserListDTO
    {
        public List<TaskWithUserDTO> TaskWithUserList { get; set; }
    }
}
