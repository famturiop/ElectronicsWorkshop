using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.Application.MapperProfiles;

public class BaseDeviceProfile : Profile
{
    public BaseDeviceProfile()
    {
        CreateMap<BaseDeviceReadDto, BaseDeviceWriteDto>();
    }
}