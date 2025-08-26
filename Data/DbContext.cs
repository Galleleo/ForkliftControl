using Microsoft.EntityFrameworkCore;
using ForkliftControl.Models;

namespace ForkliftControl.Data
{
    public class ForkliftContext : DbContext
    {
        public ForkliftContext(DbContextOptions<ForkliftContext> options) : base(options) { }

        public DbSet<Forklift> Forklifts { get; set; } = null!;
        public DbSet<CommandLog> CommandLogs { get; set; } = null!;
    }
}