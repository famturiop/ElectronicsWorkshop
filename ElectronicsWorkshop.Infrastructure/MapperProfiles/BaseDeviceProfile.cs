using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Infrastructure.MapperProfiles;

public class BaseDeviceProfile : Profile
{
    public BaseDeviceProfile()
    {
        CreateMap<BaseDevice, BaseDeviceDto>().ReverseMap();
    }
}