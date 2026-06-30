using Microsoft.AspNetCore.Mvc;
using MM.IT.Common.Models.Common;
using MM.IT.Core.Api.Attributes;

namespace MM.IT.Optiyol.Api.Controllers;

/// <summary>
/// Net Core Api için kullanılan BaseApiController nesnesi
/// Tüm Api Controller sınıfları BaseApiController'den türemelidir.
/// </summary>
[ApiController]
[PrimeNodeAuthorize]
public abstract class PrimeNodeCoreController : ControllerBase
{
    [NonAction]
    protected IActionResult Failure<T>(int errorCode, T data, string message = null)
    {
        return Failure(new ApiResultModel<T>
        {
            Message = message,
            Code = errorCode,
            Data = data,
            Success = false
        });
    }

    [NonAction]
    protected IActionResult Failure(string message = null)
    {
        return Failure(new ApiResultModel
        {
            Message = message,
            Success = false
        });
    }

    [NonAction]
    IActionResult Failure<T>(ApiResultModel<T> data)
    {
        return BadRequest(data);
    }

    [NonAction]
    protected IActionResult Success<T>(int errorCode, T data, string message = null)
    {
        return Success(new ApiResultModel<T>
        {
            Success = true,
            //Message = message,
            Code = errorCode,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Success<T>(T data)
    {
        return Success(new ApiResultModel<T>
        {
            Success = true,
            Data = data
        });
    }

    [NonAction]
    IActionResult Success<T>(ApiResultModel<T> data)
    {
        return Ok(data);
    }
}