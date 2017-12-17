using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;

namespace hw3webapp.Models
{
    public class TodoDbContext : DbContext
    {
    
        public  TodoDbContext(DbContextOptions<TodoDbContext> options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            optionsBuilder.UseSqlite("Filename=" + Path.Combine(path, "todo.db"));
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;");
        }
        
        public DbSet<TodoItem> TodoItems { get; set; }
        //public DbSet<TodoItemLabel> TodoItemLabels { get; set; }
        
    }
}