using Kanban.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.Model.Models.Response
{
    public class UserDTO
    {
        public List<User> UserList { get; set; }
        public User SingleUser { get; set; }
    }
}
