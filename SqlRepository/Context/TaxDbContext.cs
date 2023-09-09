using Microsoft.EntityFrameworkCore;
using SqlRepository.Models;

namespace SqlRepository.Context;

public class TaxDbContext : DbContext
{
    public DbSet<VehicleDb> Tax { get; set; }

    public TaxDbContext(DbContextOptions<TaxDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}