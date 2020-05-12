using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model.Models.Request
{
    public class PatchKanbanTaskProgressStatusVM
    {
        [Required]
        public int ProgressStatus { get; set; }
    }
}
