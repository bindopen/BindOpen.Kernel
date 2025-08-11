using Microsoft.EntityFrameworkCore;

namespace BindOpen.Data;

public class SqliteDataDbContext : DataDbContext
{
    public SqliteDataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    private string _connectionString;

    public SqliteDataDbContext(string connectionString) : base(connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite(_connectionString);
    }
}
