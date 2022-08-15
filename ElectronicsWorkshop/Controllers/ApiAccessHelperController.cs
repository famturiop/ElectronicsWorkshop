using ElectronicsWorkshop.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace ElectronicsWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAccessHelperController : BaseController
    {
        private readonly IOptions<ServiceCollectionsExtensions.AuthenticationOptions> _options;

        public ApiAccessHelperController(
            IOptions<ServiceCollectionsExtensions.AuthenticationOptions> options)
        {
            _options = options;
        }


        [SwaggerOperation(
            Description = "Asks IdentityServer for an access token that is used to" +
                          " access Delete api endpoint.",
            Summary = "Retrieves access token from IdentityServer")]
        [HttpGet]
        public async Task<IActionResult> GetAccessToken()
        {
            using var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(_options.Value.IdentityVerifier);
            if (disco.IsError)
            {
                return Conflict();
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = _options.Value.ClientId,
                ClientSecret = _options.Value.ClientSecret,
                Scope = _options.Value.Scope
            });

            if (tokenResponse.IsError)
            {
                return Conflict();
            }

            return Ok(tokenResponse);
        }
    }
}
