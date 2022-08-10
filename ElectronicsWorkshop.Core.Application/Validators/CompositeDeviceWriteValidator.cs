using ElectronicsWorkshop.Core.Application.ApiModels;
using FluentValidation;

namespace ElectronicsWorkshop.Core.Application.Validators;

public class CompositeDeviceWriteValidator: AbstractValidator<CompositeDeviceWrite>
{
    public CompositeDeviceWriteValidator()
    {
        RuleFor(c => c.Name).Length(3, 30).NotEmpty();
        RuleFor(c => c.BasisId).GreaterThan(0).NotEmpty();
        RuleForEach(c => c.ConnectorIds).GreaterThan(0).NotEmpty();
        RuleFor(c => c.Quantity).GreaterThan(0).NotEmpty();
    }
}