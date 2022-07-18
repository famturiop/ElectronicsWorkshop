using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Infrastructure.MapperProfiles;

public class CompositeDeviceProfile : Profile
{
    public CompositeDeviceProfile()
    {
        CreateMap<CompositeDevice, CompositeDeviceDto>().ReverseMap();
    }
}