using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Resources;
using MM.IT.Core.Services.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Common.Extensions;

namespace MM.IT.Core.Services.Base;

/// <summary>
/// IService şartlarını barındıran Abstract Business Servis Nesnesi
/// Tüm Business servis sınıfları BaseService'den türemelidir.
/// </summary>
public abstract class BaseService : IService
{
    private readonly IOptions<AppConfigModel> _appConfigs;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    public BaseService(IServiceProvider serviceProvider)
    {
        _appConfigs = serviceProvider.GetService(typeof(IOptions<AppConfigModel>)) as IOptions<AppConfigModel>;
        _stringLocalizer = serviceProvider.GetService(typeof(IStringLocalizer<SharedResources>)) as IStringLocalizer<SharedResources>;
    }
    public ServiceResultModel Result(string message)
    {
        return Result(null, message, StatusCodes.Status200OK);
    }

    public ServiceResultModel Result(string message, int code)
    {
        return Result(null, message, code);
    }
    public ServiceResultModel Result(object data = null, string message = "", int code = StatusCodes.Status200OK)
    {
        message = code == StatusCodes.Status200OK && string.IsNullOrWhiteSpace(message) ? _stringLocalizer["Message.ProcessCompletedSuccessfully"] : message;

        return new ServiceResultModel
        {
            Code = code,
            Message = message?.ToTitleCase(),
            Data = data,
            Provider = _appConfigs.Value.Provider.Name,
            Version = _appConfigs.Value.Provider.Version,
        };
    }

    public ServiceResultModel<TDataModel> Result<TDataModel>(TDataModel data, string message = "", int code = StatusCodes.Status200OK)
    {
        message = code == StatusCodes.Status200OK && string.IsNullOrWhiteSpace(message) ? _stringLocalizer["Message.ProcessCompletedSuccessfully"] : message;

        return new ServiceResultModel<TDataModel>
        {
            Code = code,
            Message = message,
            Data = data,
            Provider = _appConfigs.Value.Provider.Name,
            Version = _appConfigs.Value.Provider.Version,
        };
    }
}
