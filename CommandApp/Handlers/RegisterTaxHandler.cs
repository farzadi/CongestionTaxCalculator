using CommandApp.Commands;
using CommandApp.Dtos;
using MediatR;
using SqlRepository;
using SqlRepository.Models;

namespace CommandApp.Handlers;

public class RegisterTaxHandler:IRequestHandler<RegisterTaxCommand, RegisterTaxResponse>
{
    private readonly TaxSqlRepository _sqlRepository;
    public RegisterTaxHandler(TaxSqlRepository sqlRepository)
    {
        _sqlRepository = sqlRepository;
    }
    public async Task<RegisterTaxResponse> Handle(RegisterTaxCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new VehicleDb
        {
            Vehicle = request.Vehicle,
            EnterTime = request.EnterTime
        };
        var result = await _sqlRepository.AddVehicleAsync(vehicle);

        return new RegisterTaxResponse { Message = result };

    }
}