﻿using Kanban.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Kanban.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task Add(T entity);
    }
}