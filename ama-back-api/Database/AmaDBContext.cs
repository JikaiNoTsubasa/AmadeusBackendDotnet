using System;
using ama_back_api.DBModels;
using Microsoft.EntityFrameworkCore;

namespace ama_back_api.Database;

public class AmaDBContext : DbContext
{
    public AmaDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AmaEntity> Entities { get; set; }
    public DbSet<AmaUser> Users { get; set; }
    public DbSet<AmaCategory> Categories { get; set; }
    public DbSet<AmaProject> Projects { get; set; }
    public DbSet<AmaStatus> Status { get; set; }
    public DbSet<AmaTask> Tasks { get; set; }
    public DbSet<AmaUnit> Units { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
