using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.Application.MapperProfiles;

public class ConnectorProfile : Profile
{
    public ConnectorProfile()
    {
        CreateMap<ConnectorReadDto, ConnectorWriteDto>();
    }
}