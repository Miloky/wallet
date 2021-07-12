using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Data;
using System.Linq;
using Wallet.Api.SmartTable;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContactController: ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SmartTableParams smartTableParams)
        {
            var pageSize = smartTableParams.Pagination.PageSize;
            var skip = smartTableParams.Pagination.PageNumber * smartTableParams.Pagination.PageSize;
            var contacts = _applicationDbContext.Contacts.OrderBy(x => x.FirstName).Skip(skip - pageSize)
                .Take(pageSize);
            return Ok(contacts);
        }

        [HttpGet("/api/contacts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = _applicationDbContext.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            // TODO: Mapper
            return Ok(contact);
        }
    }
}