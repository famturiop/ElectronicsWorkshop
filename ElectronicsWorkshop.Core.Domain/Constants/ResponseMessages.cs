using System.Text;

namespace ElectronicsWorkshop.Core.Domain.Constants;

public static class ResponseMessages
{
    public const string Success = "SUCCESS";

    public const string BadRequest = "BADREQUEST";

    public static string WorkshopItemNotFound(int id) => $"Workshop item with id {id} is not found";

    public static string WorkshopItemsNotFound(IEnumerable<int> ids)
    {
        var stringIds = String.Join(',', ids);

        return $"Workshop item with id {stringIds} is not found";
    }

    public const string IdNotNegative = "Id can not be negative";

    public const string CoreRulesViolation = "You are violating core business rules. " + 
                                             "Workshop does not have enough Connectors or Base Devices " +
                                             "to construct CompositeDevice";
}