using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Core.Application.Responses;

public class WorkshopItemResponse : BaseResponse
{
    public WorkshopItem Item { get; set; }
}