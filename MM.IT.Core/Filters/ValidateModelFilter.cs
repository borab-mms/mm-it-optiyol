using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Core.Attributes;

namespace MM.IT.Core.Filters;

/// <summary>
/// Model Validation Action Filter Nesnesi
/// Request modelleri kontrol edip valid olmayanlar için BadRequest Response oluşturup dönmesini sağlar.
/// </summary>
public class ValidateModelFilter : ActionFilterAttribute
{
    private readonly IOptions<AppConfigModel> _appConfigs;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    /// <summary>
    /// ValidateModelFilter Constructor.
    /// </summary>
    /// <param name="appConfigs">AppConfigModel Options</param>
    /// <param name="stringLocalizer">IStringLocalizer: Resources</param>
    public ValidateModelFilter(IOptions<AppConfigModel> appConfigs, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _appConfigs = appConfigs;
        _stringLocalizer = stringLocalizer;
    }

    /// <summary>
    /// Her Action execute aşamasında IgnoreValidationAttribute eklenmemiş Action'ların Model validation'larını yapan method
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //INFO: IgnoreValidationAttribute eklenmiş Controller.Action'larda bu kontrolü yapmaz.
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var hasIgnoreValidationAttribute = descriptor?.MethodInfo.GetCustomAttributes(inherit: true)
            .Any(a => a.GetType().Equals(typeof(IgnoreModelValidationAttribute))) ?? false;

        if (!hasIgnoreValidationAttribute && !context.ModelState.IsValid)
        {
            //    context.Result = new BadRequestObjectResult(new ApiResultModel
            //    {
            //        Code = StatusCodes.Status400BadRequest,
            //        Data = context.ModelState
            //.Where(ms => ms.Value.Errors.Any())
            //.Select(x => new { x.Key, Error = x.Value.Errors.Select(p => p.ErrorMessage).FirstOrDefault() }),
            //        Message = "Hatalar aşağıda listelenmiştir!"
            //    });


            context.Result = new BadRequestObjectResult(new MediaSaturnTrResultModel
            {
                //code = StatusCodes.Status400BadRequest,
                success = false,
                data = null,
                message = null,
                errors = context.ModelState.Where(a => a.Value.Errors.Any()).Select(a => new MediaSaturnTrResultErrorModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    error = a.Key + "=>" + a.Value.Errors.Select(b => b.ErrorMessage).FirstOrDefault()
                }).ToList()
            });
        }
    }
}