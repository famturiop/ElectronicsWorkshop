using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Infrastructure.MapperProfiles;

public class ConnectorProfile : Profile
{
    public ConnectorProfile()
    {
        CreateMap<Connector, ConnectorReadDto>()
            .ReverseMap();
        CreateMap<ConnectorWriteDto, Connector>()
            .ForMember(m => m.CompositeDevices, c => c.AllowNull());
    }
}