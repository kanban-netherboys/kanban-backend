﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model.Models.Request
{
    public class KanbanTaskVM : PatchKanbanTaskVM
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
