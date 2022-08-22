using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
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
            Description = ApiInfo.GetDescription,
            Summary = ApiInfo.GetSummary)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
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
            Description = ApiInfo.PostDescription,
            Summary = ApiInfo.PostSummary)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompositeDeviceWrite device)
        {
            var response = await _compositeDeviceService.CreateCompositeDeviceAsync(device);

            if (response.Success)
            {
                return CreatedAtAction(nameof(Get), new { response.Item.Id }, response.Item.Id);
            }

            return ErrorStatusCode(response);
        }

        /// <summary>
        /// Updates a composite device.
        /// </summary>
        [SwaggerOperation(
            Description = ApiInfo.UpdateDescription,
            Summary = ApiInfo.UpdateSummary)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CompositeDeviceUpdate device)
        {
            var response = await _compositeDeviceService.UpdateCompositeDeviceAsync(device, id);

            if (response.Success)
            {
                return Ok();
            }

            return ErrorStatusCode(response);
        }

        /// <summary>
        /// Deletes a composite device.
        /// </summary>
        [SwaggerOperation(
            Description = ApiInfo.DeleteDescription,
            Summary = ApiInfo.DeleteSummary)]
        [HttpDelete("{id}")]
        [Authorize("DeleteAccess")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _compositeDeviceService.DeleteCompositeDeviceAsync(id);

            if (response.Success)
            {
                return Ok();
            }

            return ErrorStatusCode(response);
        }
    }
}
