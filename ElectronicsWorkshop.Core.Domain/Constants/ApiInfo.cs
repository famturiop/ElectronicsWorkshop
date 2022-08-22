namespace ElectronicsWorkshop.Core.Domain.Constants;

public static class ApiInfo
{
    public const string GetDescription = "Gets a composite device." +
                               "Quantity is the amount of devices stored at the workshop." +
                               "Price is the combined price of all of the composite device's parts." +
                               "Name is the device's name." +
                               "Id is the URI in the database." +
                               "BaseDevice is a founding part of a composite device. (for example Laptop Motherboard)" +
                               "Connectors are parts that responsible for connecting to other devices. (for example USB)";

    public const string GetSummary = "Gets a composite device";

    public const string PostDescription = "Creates a composite device." +
                                          "A composite device has to have one BaseDevice " +
                                          " and can have zero or one of each connector type." +
                                          "Available BaseDevices and Connectors IDs are in the range of 1 to 8." +
                                          "The amount of each part (i.e. a connector or BaseDevice) " +
                                          "used to create a composite device is set by Quantity property." +
                                          "If workshop has enough of each part then the value of Quantity " +
                                          " is subtracted from each part's Quantity value and operation succeeds.";

    public const string PostSummary = "Creates a composite device";

    public const string UpdateDescription = "Updates a composite device." +
                                            "The Name and the Quantity of the composite device can be changed." +
                                            "Quantity value is added to the existing " +
                                            " Quantity value of the composite device in the database." +
                                            "It represents the amount of new composite devices a workshop creates." +
                                            "Consequently the workshop has to have enough parts" +
                                            " to create new composite devices for operation to succeed.";

    public const string UpdateSummary = "Updates a composite device";

    public const string DeleteDescription = "Deletes a composite device";

    public const string DeleteSummary = "Deletes a composite device";
}