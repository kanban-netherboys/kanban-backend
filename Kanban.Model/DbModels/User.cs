using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kanban.Model.DbModels
{
    public class User : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public List<UserTask> UserTask { get; set; }
    }
}
