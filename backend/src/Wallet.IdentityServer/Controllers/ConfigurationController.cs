using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.IdentityServer.Controllers
{
    [ApiController]
    [Route("/_configuration")]
    [Produces("application/json")]
    public class ConfigurationController: ControllerBase
    {
        private readonly IClientRequestParametersProvider _parametersProvider;
        // TODO: Add logger
        public ConfigurationController(IClientRequestParametersProvider parametersProvider)
        {
            _parametersProvider = parametersProvider;
        }

        [HttpGet("{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = _parametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}