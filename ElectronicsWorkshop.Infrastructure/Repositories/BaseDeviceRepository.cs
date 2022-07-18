using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Repositories
{
    public class BaseDeviceRepository : IBaseDeviceRepository, IDisposable
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
        public async Task<BaseDeviceDto> GetBaseDeviceAsync(int id)
        {
            var device = await _dbContext.BaseDevices.FirstOrDefaultAsync(bd => bd.Id == id);

            return _mapper.Map(device, new BaseDeviceDto());
        }

        public async Task CreateBaseDeviceAsync(BaseDeviceDto deviceDto)
        {
            if (deviceDto != null)
            {
                var device = _mapper.Map(deviceDto, new BaseDevice());

                await _dbContext.BaseDevices.AddAsync(device);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBaseDeviceAsync(int id)
        {
            var device = await _dbContext.BaseDevices.FirstOrDefaultAsync(bd => bd.Id == id);

            if (device != null)
            {
                _dbContext.BaseDevices.Remove(device);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateBaseDeviceAsync(BaseDeviceDto deviceDto, int id)
        {
            var device = await _dbContext.BaseDevices.FirstOrDefaultAsync(bd => bd.Id == id);

            if (deviceDto != null && device != null)
            {
                _dbContext.BaseDevices.Update(_mapper.Map(deviceDto, device));
                await _dbContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
