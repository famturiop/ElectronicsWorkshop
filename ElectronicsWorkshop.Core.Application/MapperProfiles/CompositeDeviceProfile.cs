using AutoMapper;
using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.Application.MapperProfiles;

public class CompositeDeviceProfile : Profile
{
    public CompositeDeviceProfile()
    {
        CreateMap<CompositeDeviceReadDto, CompositeDeviceRead>();
    }
}