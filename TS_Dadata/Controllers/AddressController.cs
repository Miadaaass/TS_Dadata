using TS_Dadata.Models;
using TS_Dadata.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TS_Dadata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IDadataService _dadataService;

        public AddressController(IDadataService dadataService)
        {
            _dadataService = dadataService;
        }

        [HttpGet("clean")]
        public async Task<ActionResult<AddressResponse>> Clean([FromQuery] string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest("Address is required.");
            }

            var result = await _dadataService.CleanAddressAsync(address);
            return Ok(result);
        }
    }
}
