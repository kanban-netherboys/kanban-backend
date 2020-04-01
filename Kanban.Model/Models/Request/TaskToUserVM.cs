using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model.Models.Request
{
    public class TaskToUserVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
