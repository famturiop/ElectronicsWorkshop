using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicsWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompositeDeviceController : BaseController
    {
        private readonly ICompositeDeviceService _compositeDeviceService;

        public CompositeDeviceController(ICompositeDeviceService compositeDeviceService)
        {
            _compositeDeviceService = compositeDeviceService;
        }


        /// <summary>
        /// Gets a composite device.
        /// </summary>
        [SwaggerOperation(
            Description = "Gets a composite device." +
                          "Quantity is the amount of devices stored at the workshop." +
                          "Price is the combined price of all of the composite device's parts." +
                          "Name is the device's name." +
                          "Id is the URI in the database." +
                          "BaseDevice is a founding part of a composite device. (for example Laptop Motherboard)" +
                          "Connectors are parts that responsible for connecting to other devices. (for example USB)",
            Summary = "Gets a composite device")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CompositeDeviceRead>> Get(int id)
        {
            var response = await _compositeDeviceService.GetCompositeDeviceAsync(id);

            if (response.Success)
            {
                return Ok(response.CompositeDevice);
            }

            return ErrorStatusCode(response);
        }

        /// <summary>
        /// Creates a composite device.  
        /// </summary>
        [SwaggerOperation(
            Description = "Creates a composite device." +
                          "A composite device has to have one BaseDevice " +
                          " and can have zero or one of each connector type." +
                          "Available BaseDevices and Connectors IDs are in the range of 1 to 8." +
                          "The amount of each part (i.e. a connector or BaseDevice) " +
                          "used to create a composite device is set by Quantity property." +
                          "If workshop has enough of each part then the value of Quantity " +
                          " is subtracted from each part's Quantity value and operation succeeds.",
            Summary = "Creates a composite device")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompositeDeviceWrite device)
        {
            var response = await _compositeDeviceService.CreateCompositeDeviceAsync(device);

            if (response.Success)
                return CreatedAtAction(nameof(Get), new { response.Item.Id }, response.Item.Id);

            return ErrorStatusCode(response);
        }

        /// <summary>
        /// Updates a composite device.
        /// </summary>
        [SwaggerOperation(
            Description = "Updates a composite device." +
                          "The Name and the Quantity of the composite device can be changed." +
                          "Quantity value is added to the existing " +
                          " Quantity value of the composite device in the database." +
                          "It represents the amount of new composite devices a workshop creates." +
                          "Consequently the workshop has to have enough parts" +
                          " to create new composite devices for operation to succeed.",
            Summary = "Updates a composite device")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompositeDeviceUpdate device)
        {
            var response = await _compositeDeviceService.UpdateCompositeDeviceAsync(device, id);

            if (response.Success)
                return Ok();

            return ErrorStatusCode(response);
        }

        /// <summary>
        /// Deletes a composite device.
        /// </summary>
        [SwaggerOperation(
            Description = "Deletes a composite device",
            Summary = "Deletes a composite device")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _compositeDeviceService.DeleteCompositeDeviceAsync(id);

            if (response.Success)
                return Ok();

            return ErrorStatusCode(response);
        }
    }
}
