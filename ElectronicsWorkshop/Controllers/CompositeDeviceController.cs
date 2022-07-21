using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompositeDeviceController : ControllerBase
    {
        private readonly ICompositeDeviceService _compositeDeviceService;

        public CompositeDeviceController(ICompositeDeviceService compositeDeviceService)
        {
            _compositeDeviceService = compositeDeviceService;
        }

        // GET api/<CompositeDeviceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompositeDeviceRead>> Get(int id)
        {
            return await _compositeDeviceService.GetCompositeDeviceAsync(id);
        }

        // POST api/<CompositeDeviceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompositeDeviceWrite device)
        {
            await _compositeDeviceService.CreateCompositeDeviceAsync(device);
            return Ok();
        }

        // PUT api/<CompositeDeviceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompositeDeviceUpdate device)
        {
            await _compositeDeviceService.UpdateCompositeDeviceAsync(device, id);
            return Ok();
        }

        // DELETE api/<CompositeDeviceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _compositeDeviceService.DeleteCompositeDeviceAsync(id);
            return Ok();
        }
    }
}
