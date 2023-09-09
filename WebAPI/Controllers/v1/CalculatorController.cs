using CommandApp.Commands;
using CommandApp.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryApp.Dtos;
using QueryApp.Queries;

namespace WebAPI.Controllers.v1;

public class CalculatorController: BaseController
{


    private readonly ISender _sender;

    public CalculatorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<CalculateTaxesResponse>), 200)]
    public async Task<IActionResult> CalculateTaxes([FromQuery] CalculateTaxesQuery calculateTaxes, CancellationToken cancellation)
    {
        return Ok(await _sender.Send(calculateTaxes, cancellation));
    }
    
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(RegisterTaxResponse), 200)]
    public async Task<IActionResult> RegisterTax([FromBody] RegisterTaxCommand registerTaxCommand, CancellationToken cancellation)
    {
        return Ok(await _sender.Send(registerTaxCommand, cancellation));
    }
    
}