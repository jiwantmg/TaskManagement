using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskManagementDbContext
    {
        DbSet<T> Set<T>() where T: class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}