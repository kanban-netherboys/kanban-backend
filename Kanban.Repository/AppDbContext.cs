using Kanban.Model;
using Kanban.Model.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demcio.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<KanbanTask> KanbanTasks { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTask>()
                .HasKey(t => new { t.UserId, t.KanbanTaskId });

            modelBuilder.Entity<UserTask>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserTask)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserTask>()
                .HasOne(pt => pt.KanbanTask)
                .WithMany(t => t.UserTask)
                .HasForeignKey(pt => pt.KanbanTaskId);
        }
    }
}
