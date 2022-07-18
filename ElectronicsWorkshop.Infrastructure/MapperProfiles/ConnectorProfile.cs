using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Infrastructure.MapperProfiles;

public class ConnectorProfile : Profile
{
    public ConnectorProfile()
    {
        CreateMap<Connector, ConnectorDto>().ReverseMap();
    }
}