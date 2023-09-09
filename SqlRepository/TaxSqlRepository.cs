using Microsoft.EntityFrameworkCore;
using SqlRepository.Context;
using SqlRepository.Models;

namespace SqlRepository;

public class TaxSqlRepository : ISqlRepository
{
    private readonly TaxDbContext _context;

    public TaxSqlRepository(TaxDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddVehicleAsync(VehicleDb car)
    {
        _context.Tax.Add(car);
        var result = await _context.SaveChangesAsync();
        return result;
    }

    public async Task<IEnumerable<VehicleDb>> GetCarsAsync(DateTime year)
    {
        return await _context.Tax
            .Where(t => t.EnterTime.Year == year.Year)
            .ToListAsync();
    }
}