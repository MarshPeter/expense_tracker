namespace expense_tracker.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

// This is used for migrations that occur during design time
// i.e. when you use: dotnet ef migrations add <name>
public class ExpenseDBContextFactory : IDesignTimeDbContextFactory<ExpenseDBContext>
{
    public ExpenseDBContext CreateDbContext(string[] args)
    {

        string basePath = Path.Combine(
            Directory.GetCurrentDirectory()
        );

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ExpenseDBContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DbDefaultConnection")
        );

        return new ExpenseDBContext(optionsBuilder.Options);
    }
}
