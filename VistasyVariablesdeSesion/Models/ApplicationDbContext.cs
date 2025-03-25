using Microsoft.EntityFrameworkCore;

namespace VistasyVariablesdeSesion.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<usuarios> usuarios { get; set; }

    }
}
