using MediatR;
using QueryApp.Dtos;

namespace QueryApp.Queries;

public class CalculateTaxesQuery:IRequest<List<CalculateTaxesResponse>>
{
    public int Year { get; set; }
}