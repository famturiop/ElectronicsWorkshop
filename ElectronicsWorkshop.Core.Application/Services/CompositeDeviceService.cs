using AutoMapper;
using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.Responses;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Domain.Constants;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Net;


namespace ElectronicsWorkshop.Core.Application.Services;

public class CompositeDeviceService : ICompositeDeviceService
{
    private readonly IUnitOfWork _repositories;
    private readonly IMapper _mapper;
    private readonly ResponseFactory _responseFactory;
    private readonly IValidator<CompositeDeviceWrite> _writeValidator;
    private readonly IValidator<CompositeDeviceUpdate> _updateValidator;
    private readonly ICompositeDeviceFactory _compositeDeviceFactory;

    public CompositeDeviceService(
        IUnitOfWork repositories,
        IMapper mapper,
        ResponseFactory responseFactory,
        IValidator<CompositeDeviceWrite> writeValidator,
        IValidator<CompositeDeviceUpdate> updateValidator,
        ICompositeDeviceFactory compositeDeviceFactory)
    {
        _repositories = repositories;
        _mapper = mapper;
        _responseFactory = responseFactory;
        _writeValidator = writeValidator;
        _updateValidator = updateValidator;
        _compositeDeviceFactory = compositeDeviceFactory;
    }
    public async Task<CompositeDeviceResponse> GetCompositeDeviceAsync(int id)
    {
        if (id <= 0)
        {
            return _responseFactory.Failure<CompositeDeviceResponse>(
                ResponseMessages.IdNotNegative, HttpStatusCode.UnprocessableEntity);
        }

        var deviceDto = await _repositories.CompositeDevices.GetCompositeDeviceAsync(id);

        if (deviceDto == null)
        {
            return _responseFactory.Failure<CompositeDeviceResponse>(
                ResponseMessages.WorkshopItemNotFound(id), HttpStatusCode.NotFound);
        }

        var compositeDevice = _mapper.Map(deviceDto, new CompositeDeviceRead());
        return _responseFactory.Success(compositeDevice);
    }

    public async Task<WorkshopItemResponse> CreateCompositeDeviceAsync(CompositeDeviceWrite deviceApiModel)
    {
        var modelValidation = await _writeValidator.ValidateAsync(deviceApiModel);

        if (!modelValidation.IsValid)
        {
            return _responseFactory.Failure<WorkshopItemResponse>(
                GenerateFailureMessage(modelValidation),
                HttpStatusCode.UnprocessableEntity);
        }

        var baseDevice = await _repositories.BaseDevices.GetBaseDeviceAsync(deviceApiModel.BasisId);

        if (baseDevice == null)
        {
            return _responseFactory.Failure<WorkshopItemResponse>(
                ResponseMessages.WorkshopItemNotFound(deviceApiModel.BasisId),
                HttpStatusCode.NotFound);
        }

        var connectors =
            (await _repositories.Connectors.GetVariableAmountOfConnectorsAsync(deviceApiModel.ConnectorIds)).ToList();

        var notFoundConnectors = deviceApiModel.ConnectorIds.Where(id => connectors.All(c => c.Id != id)).ToList();

        if (notFoundConnectors.Count != 0)
        {
            return _responseFactory.Failure<WorkshopItemResponse>(
                ResponseMessages.WorkshopItemsNotFound(notFoundConnectors),
                HttpStatusCode.NotFound);
        }

        if (_compositeDeviceFactory.HaveEnoughBaseDevices(baseDevice, deviceApiModel.Quantity) &&
            _compositeDeviceFactory.HaveEnoughConnectors(connectors, deviceApiModel.Quantity))
        {
            var compositeDeviceDto = await
                _compositeDeviceFactory.ConstructCompositeDeviceWriteDto(baseDevice, connectors, deviceApiModel);

            var item = await _repositories.CompositeDevices.CreateCompositeDeviceAsync(compositeDeviceDto);
            await _repositories.SaveChangesAsync();

            return _responseFactory.Success(item);
        }

        return _responseFactory.Failure<WorkshopItemResponse>(ResponseMessages.CoreRulesViolation, HttpStatusCode.Conflict);
    }

    public async Task<BaseResponse> UpdateCompositeDeviceAsync(CompositeDeviceUpdate deviceApiModel, int id)
    {
        var modelValidation = await _updateValidator.ValidateAsync(deviceApiModel);

        if (!modelValidation.IsValid)
        {
            return _responseFactory.Failure<BaseResponse>(
                GenerateFailureMessage(modelValidation),
                HttpStatusCode.UnprocessableEntity);
        }

        var compositeDevice = await _repositories.CompositeDevices.GetCompositeDeviceAsync(id);

        if (compositeDevice == null)
        {
            return _responseFactory.Failure<BaseResponse>(
                ResponseMessages.WorkshopItemNotFound(id),
                HttpStatusCode.NotFound);
        }

        if (_compositeDeviceFactory.HaveEnoughBaseDevices(compositeDevice.Basis, deviceApiModel.Quantity) &&
            _compositeDeviceFactory.HaveEnoughConnectors(compositeDevice.Connectors, deviceApiModel.Quantity))
        {
            var compositeDeviceDto = await
                _compositeDeviceFactory.ConstructCompositeDeviceUpdateDto(compositeDevice, deviceApiModel);

            await _repositories.CompositeDevices.UpdateCompositeDeviceAsync(compositeDeviceDto, id);
            await _repositories.SaveChangesAsync();

            return _responseFactory.Success();
        }

        return _responseFactory.Failure<BaseResponse>(ResponseMessages.CoreRulesViolation, HttpStatusCode.Conflict);
    }

    public async Task<BaseResponse> DeleteCompositeDeviceAsync(int id)
    {
        await _repositories.CompositeDevices.DeleteCompositeDeviceAsync(id);
        await _repositories.SaveChangesAsync();

        return _responseFactory.Success();
    }

    private string GenerateFailureMessage(ValidationResult modelValidation)
    {
        return modelValidation.Errors.ConvertAll(c => c.ErrorMessage).Aggregate((a, b) => $"{a}; {b}");
    }
}