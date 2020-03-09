using Demcio.Repository;
using Kanban.Model;
using Kanban.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kanban.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddKanbanTask(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> GetAllKanbanTasks()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }
        public async Task<T> GetSingleKanbanTask(Expression<Func<T, bool>> func)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(func);
            return entity;
        }
        //public async Task DeleteKanbanTask(T entity)
        //{

        //}
    }
}
