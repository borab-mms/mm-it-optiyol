using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Sms;
using MM.IT.Common.Models.Sms.Redis;
using MM.IT.Common.Options;
using MM.IT.Core.Api.Attributes;
using MM.IT.Core.Attributes;
using MM.IT.Core.Services;
using MM.IT.Optiyol.Api.Controllers;
using MM.Optiyol.Api.Models.Optiyol;

namespace MM.IT.Optiyol.Controllers
{
    [ApiVersion("1")]
    public class OptiyolController : PrimeNodeCoreController
    {
        private readonly IServiceWrapper _serviceWrapper;
        public OptiyolController(
            IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }
        [HttpPost("webhook/v{version:apiVersion}")]
        [PrimeNodeAction]
        public async Task<IActionResult> CreateWebhookAsync([FromBody] OptiyolRequestModel input, [FromQuery] string createdDate)
        {
            var result = await _serviceWrapper.OptiyolService.WebhookAsync(input, createdDate);
            return StatusCode(result.Code, result);
        }
    }
}
