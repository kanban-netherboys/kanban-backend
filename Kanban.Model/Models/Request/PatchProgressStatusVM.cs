using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model.Models.Request
{
    public class PatchProgressStatusVM
    {
        [Required]
        public int ProgressStatus { get; set; }
    }
}
