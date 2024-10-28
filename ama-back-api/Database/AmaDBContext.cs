using System;
using ama_back_api.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ama_back_api.Database;

public class AmaDBContext : DbContext
{
    public AmaDBContext(DbContextOptions options) : base(options)
    {
    }

    #nullable disable

    public DbSet<AmaUser> Users { get; set; }
    public DbSet<AmaCategory> Categories { get; set; }
    public DbSet<AmaProject> Projects { get; set; }
    public DbSet<AmaStatus> Status { get; set; }
    public DbSet<AmaTask> Tasks { get; set; }
    public DbSet<AmaUnit> Units { get; set; }

    #nullable enable


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        /*
        modelBuilder.Entity<AmaEntity>()
            .Property(b => b.CreationDate)
            .HasDefaultValueSql("now()");
            */
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTime?>().HaveConversion<DateTimeUTCNullableConvert>();

        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeUTCConvert>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        
        IConfiguration Config = configBuilder.Build();
        string connectionString = Config.GetConnectionString("Default") ?? "";

        string centralConnectionString = Config.GetConnectionString("Default") ?? "";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public class DateTimeUTCNullableConvert: ValueConverter<DateTime?, DateTime?>
    {
        public DateTimeUTCNullableConvert(): base(
            d => d == null ? null : d.GetValueOrDefault().ToUniversalTime(),
            d => d == null ? null : DateTime.SpecifyKind(d.GetValueOrDefault(), DateTimeKind.Utc)
        )
        {

        }

    }

    public class DateTimeUTCConvert: ValueConverter<DateTime, DateTime>
    {
        public DateTimeUTCConvert(): base(
            d => d.ToUniversalTime(),
            d => DateTime.SpecifyKind(d, DateTimeKind.Utc)
        )
        {
            
        }

    }
}
