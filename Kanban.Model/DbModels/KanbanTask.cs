using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model
{
    public class KanbanTask : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
//Seedowanie bazy danych, jaka relacja, entity framework how to set relations
