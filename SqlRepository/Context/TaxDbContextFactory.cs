using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SqlRepository.Context;

public class TaxDbContextFactory : IDesignTimeDbContextFactory<TaxDbContext>
{
    public TaxDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaxDbContext>();
        var connectionString =  "Data Source=tax.db";
        if (connectionString == null)
        {
            throw new InvalidOperationException("The connection string is not set in the settings.");
        }

        optionsBuilder.UseSqlite(connectionString);

        return new TaxDbContext(optionsBuilder.Options);
    }
}