using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model
{
    public class KanbanTask : Entity
    {
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required] 
        public string Status { get; set; }
    }
}

