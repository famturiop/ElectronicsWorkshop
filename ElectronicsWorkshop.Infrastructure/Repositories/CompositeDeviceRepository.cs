using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Repositories;

public class CompositeDeviceRepository : ICompositeDeviceRepository, IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CompositeDeviceRepository(
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<CompositeDeviceDto> GetCompositeDeviceAsync(int id)
    {
        var device = await _dbContext.CompositeDevices.FirstOrDefaultAsync(bd => bd.Id == id);

        return _mapper.Map(device, new CompositeDeviceDto());
    }

    public async Task CreateCompositeDeviceAsync(CompositeDeviceDto deviceDto)
    {
        if (deviceDto != null)
        {
            var device = _mapper.Map(deviceDto, new CompositeDevice());

            await _dbContext.CompositeDevices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCompositeDeviceAsync(int id)
    {
        var device = await _dbContext.CompositeDevices.FirstOrDefaultAsync(bd => bd.Id == id);

        if (device != null)
        {
            _dbContext.CompositeDevices.Remove(device);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateCompositeDeviceAsync(CompositeDeviceDto deviceDto, int id)
    {
        var device = await _dbContext.CompositeDevices.FirstOrDefaultAsync(bd => bd.Id == id);

        if (deviceDto != null && device != null)
        {
            _dbContext.CompositeDevices.Update(_mapper.Map(deviceDto, device));
            await _dbContext.SaveChangesAsync();
        }
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}