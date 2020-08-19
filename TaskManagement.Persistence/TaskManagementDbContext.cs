using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Persistence
{
    public class TaskManagementDbContext: IdentityDbContext<AppUser>, ITaskManagementDbContext
    {
        public TaskManagementDbContext(
            DbContextOptions options
        )
            :base(options)
        {
        }

        public DbSet<LTask> Tasks { get; set; }
    }
}