using System.Text.Json;
using Core.Rules;
using Core.Settings;
using MediatR;
using QueryApp.Dtos;
using QueryApp.Queries;
using SqlRepository;
using SqlRepository.Models;

namespace QueryApp.Handlers;

public class CalculateTaxesHandler : IRequestHandler<CalculateTaxesQuery, List<CalculateTaxesResponse>>
{
    private readonly TaxSqlRepository _sqlRepository;
    private readonly TaxRules? _taxRules;

    public CalculateTaxesHandler(TaxSqlRepository sqlRepository)
    {
        _sqlRepository = sqlRepository;


        var taxRulesJson = File.ReadAllText(Settings.AllSettings.RulesSetting.TaxRulesFilePath);
        _taxRules = JsonSerializer.Deserialize<TaxRules>(taxRulesJson);
    }


    public async Task<List<CalculateTaxesResponse>> Handle(CalculateTaxesQuery request,
        CancellationToken cancellationToken)
    {
        DateTime date = new DateTime(request.Year, 1, 1); 
        var vehicles = await _sqlRepository.GetCarsAsync(date);
        var vehicleList = vehicles.ToList();
        var totalTaxByVehicleType = CalculateTotalTax(vehicleList);

        var response = totalTaxByVehicleType.Select(kv => new CalculateTaxesResponse
        {
            VehicleType = kv.Key,
            TotalTax = kv.Value
        }).ToList();

        return response;
    }

    private Dictionary<string, int> CalculateTotalTax(List<VehicleDb> vehicles)
    {
        var totalTaxByVehicleType = new Dictionary<string, int>();

        foreach (var vehicle in vehicles)
        {
            if (IsTaxExempt(vehicle))
            {
                continue;
            }

            var tax = CalculateTax(vehicle.EnterTime, _taxRules.TaxTimes);
            
            if (totalTaxByVehicleType.ContainsKey(vehicle.Vehicle))
            {
                totalTaxByVehicleType[vehicle.Vehicle] += tax;
            }
            else
            {
                totalTaxByVehicleType[vehicle.Vehicle] = tax;
            }
            
            if (totalTaxByVehicleType[vehicle.Vehicle] > _taxRules.MaxAmountPerDay)
            {
                totalTaxByVehicleType[vehicle.Vehicle] = _taxRules.MaxAmountPerDay;
            }
        }
        
        return totalTaxByVehicleType;
    }

    


    private int CalculateTax(DateTime enterTime, List<TaxTime> taxTimes)
    {
        var totalTax = 0;
        foreach (var time in from time in taxTimes let startTime = TimeSpan.Parse(time.StartTime) let endTime = TimeSpan.Parse(time.EndTime) where enterTime.TimeOfDay >= startTime && enterTime.TimeOfDay <= endTime select time)
        {
            totalTax = time.Amount;
        }
        
        return totalTax;
    }

    private bool IsTaxExempt(VehicleDb vehicle)
    {
        if (vehicle.EnterTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return true;
        }

        return vehicle.EnterTime.Month == 7 || _taxRules.ExemptVehicles.Contains(vehicle.Vehicle);
    }
}