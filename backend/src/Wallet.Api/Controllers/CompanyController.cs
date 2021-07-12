using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallet.Api.SmartTable;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class CompanyController:ControllerBase
    {
        public async Task<IActionResult> Accounts([FromQuery] SmartTableParams smartTableParams)
        {
            return Ok();
        }
    }
}