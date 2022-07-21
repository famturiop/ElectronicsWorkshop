using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Infrastructure.MapperProfiles;

public class CompositeDeviceProfile : Profile
{
    public CompositeDeviceProfile()
    {
        CreateMap<CompositeDevice, CompositeDeviceReadDto>();
        CreateMap<CompositeDeviceWriteDto, CompositeDevice>()
            .ForMember(m => m.Basis, c => c.AllowNull())
            .ForMember(m => m.Connectors, c => c.AllowNull())
            .ForMember(m => m.BasisId, c => c.MapFrom(dto => dto.Basis.Id))
            .ForMember(m => m.Id, c => c.UseDestinationValue());
        CreateMap<CompositeDeviceUpdateDto, CompositeDevice>();
    }
}