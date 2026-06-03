using Microsoft.EntityFrameworkCore;
using TestTask.Data.Contract.Model;
using TestTask.Data.EF.Configuration;

namespace TestTask.Data.EF;

internal sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<DataUser> Users => Set<DataUser>();
    public DbSet<DataCandidate> Candidates => Set<DataCandidate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CandidateConfiguration());
    }
}
