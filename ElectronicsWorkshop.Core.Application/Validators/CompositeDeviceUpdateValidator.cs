using ElectronicsWorkshop.Core.Application.ApiModels;
using FluentValidation;

namespace ElectronicsWorkshop.Core.Application.Validators;

public class CompositeDeviceUpdateValidator : AbstractValidator<CompositeDeviceUpdate>
{
    public CompositeDeviceUpdateValidator()
    {
        RuleFor(c => c.Name).Length(3, 30).NotEmpty();
        RuleFor(c => c.Quantity).GreaterThan(0).NotEmpty();
    }
}