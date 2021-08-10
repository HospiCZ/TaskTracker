using System;
using System.Linq;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DAL.App.EF
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TaskEntry> TaskEntries { get; set; }
        
        /*//Remove cascade delete
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }*/

    }
}