using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using MM.IT.Core.Api.Attributes;
using MM.IT.Core.Services;
using MM.IT.Optiyol.Controllers;
using MM.Optiyol.Api.Models.Optiyol;

namespace MM.IT.Optiyol.Api.Controllers
{
    [ApiController]
    [Route("order/v{version:apiVersion}")]
    //[AllowAnonymous]
    public class OrderController : PrimeNodeCoreController
    {
        private readonly ILogger<OptiyolController> _logger;
        private readonly IServiceWrapper _serviceWrapper;

        public OrderController(IServiceWrapper serviceWrapper, ILogger<OptiyolController> logger)
        {
            _serviceWrapper = serviceWrapper;
            _logger = logger;
        }

        [HttpPost("create")]
        [PrimeNodeAction]
        public async Task<IActionResult> CreateBarcode([FromBody] OptiyolBarcodeCreateRequestModel model)
        {
            var result = await _serviceWrapper.OptiyolService.CreateBarcodeAsync(model);
            return StatusCode(result.Code, result);
        }
        [HttpPost("cancel")]
        [PrimeNodeAction]
        public async Task<IActionResult> CancelBarcode([FromBody] OptiyolBarcodeCancelRequestModel model)
        {
            var result = await _serviceWrapper.OptiyolService.CancelBarcodeAsync(model);
            return StatusCode(result.Code, result);
        }
    }
}
