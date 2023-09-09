using CommandApp.Dtos;
using MediatR;

namespace CommandApp.Commands;

public class RegisterTaxCommand:  IRequest<RegisterTaxResponse>
{
    public string Vehicle { get; set; } = default!;
    public DateTime EnterTime { get; set; }
}
