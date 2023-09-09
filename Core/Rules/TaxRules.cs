namespace Core.Rules;

public class TaxRules
{
    public List<TaxTime> TaxTimes { get; set; }
    public List<string> ExemptVehicles { get; set; }
    public int MaxAmountPerDay { get; set; }
}