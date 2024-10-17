using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ama_back_api.Database;

public class AmaDBContextFactory : IDesignTimeDbContextFactory<AmaDBContext>
{
    public AmaDBContext CreateDbContext(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        
        IConfiguration Config = configBuilder.Build();
        var optionsBuilder = new DbContextOptionsBuilder<AmaDBContext>();

        string centralConnectionString = Config.GetConnectionString("Default") ?? "";

        optionsBuilder.UseMySql(centralConnectionString, ServerVersion.AutoDetect(centralConnectionString));

        return new AmaDBContext(optionsBuilder.Options);
    }
}
