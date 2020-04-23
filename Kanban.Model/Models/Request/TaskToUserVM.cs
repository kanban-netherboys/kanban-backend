using Kanban.Model.DbModels;
using Kanban.Model.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model.Models.Request
{
    public class TaskToUserVM
    {

        public List<UserWithoutIdDTO> UserList { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public int Priority { get; set; }
    }
}
