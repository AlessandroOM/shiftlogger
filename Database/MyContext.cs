using Models;
using Map;

using Microsoft.EntityFrameworkCore;

namespace Database;

public class MyContext : DbContext
{
    public DbSet<Logger>? Loggers { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=shiftLogger.db;");
        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Logger>(new LoggerMap().Configure);
        modelBuilder.Entity<Logger>();
         
    }
    
}
