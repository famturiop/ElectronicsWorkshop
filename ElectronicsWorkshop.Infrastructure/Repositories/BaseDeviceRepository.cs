using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Repositories
{
    public class BaseDeviceRepository : IBaseDeviceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BaseDeviceRepository(
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<BaseDeviceReadDto> GetBaseDeviceAsync(int id)
        {
            var device = await _dbContext.BaseDevices.FirstOrDefaultAsync(bd => bd.Id == id);

            return _mapper.Map(device, new BaseDeviceReadDto());
        }

        public async Task CreateBaseDeviceAsync(BaseDeviceWriteDto deviceDto)
        {
            if (deviceDto != null)
            {
                var device = _mapper.Map(deviceDto, new BaseDevice());

                await _dbContext.BaseDevices.AddAsync(device);
            }
        }

        public async Task DeleteBaseDeviceAsync(int id)
        {
            var device = await _dbContext.BaseDevices.FindAsync(id);

            if (device != null)
            {
                _dbContext.BaseDevices.Remove(device);
            }
        }

        public async Task UpdateBaseDeviceAsync(BaseDeviceWriteDto deviceDto, int id)
        {
            var device = await _dbContext.BaseDevices.FindAsync(id);

            if (deviceDto != null && device != null)
            {
                _dbContext.BaseDevices.Update(_mapper.Map(deviceDto, device));
            }
        }
    }
}
