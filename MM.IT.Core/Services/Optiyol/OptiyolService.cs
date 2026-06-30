using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Resources;
using MM.IT.Core.Adapters.IntegrationAdapter.Interfaces;
using MM.IT.Core.Services.Base;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using MM.Optiyol.Api.Constants;
using MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol;
using MM.Optiyol.Api.Models.Optiyol;
using MM.Optiyol.Api.Services.Optiyol.Interfaces;
using MM.Optiyol.Api.Utilities.Extensions;
using NetTopologySuite.Index.HPRtree;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MM.Optiyol.Api.Services.Optiyol
{
    public class OptiyolService : BaseService, IOptiyolService
    {
        private readonly IMMLogisticsRepositoryWrapper _mMLogisticsRepositoryWrapper;
        private readonly IUnitOfWork<EFCoreMMLogisticsSqlProvider> _mMLogisticsUnitOfWork;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptiyolIntegrationAdapter _optiyolIntegrationAdapter;
        private readonly ILogger<OptiyolService> _logger;

        public OptiyolService(IMMLogisticsRepositoryWrapper mMLogisticsRepositoryWrapper
            , IUnitOfWork<EFCoreMMLogisticsSqlProvider> mMLogisticsUnitOfWork
            , IOptiyolIntegrationAdapter optiyolIntegrationAdapter
            , IStringLocalizer<SharedResources> stringLocalizer, IHttpContextAccessor httpContextAccessor
            , IServiceProvider serviceProvider
            , ILogger<OptiyolService> logger) : base(serviceProvider)
        {
            _mMLogisticsRepositoryWrapper = mMLogisticsRepositoryWrapper;
            _mMLogisticsUnitOfWork = mMLogisticsUnitOfWork;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
            _optiyolIntegrationAdapter = optiyolIntegrationAdapter;
            _logger = logger;
        }
        public async Task<ServiceResultModel> WebhookAsyncld(OptiyolRequestModel model, string createdDate)
        {
            var datetimenow = DateTime.Now;//kaydedilecek bütün veriler aynı date parametresine sahip olması için

            if (!string.IsNullOrEmpty(createdDate))
            {
                datetimenow = Convert.ToDateTime(createdDate);
            }

            // 2. Geçersiz Event kontrolü
            var validEvents = new List<string>
                {
                    OptiyolHookEventNames.RoutePlanned,
                    OptiyolHookEventNames.RouteLoadListCompleted,
                    OptiyolHookEventNames.RouteStarted,
                    OptiyolHookEventNames.OrderArrived,
                    OptiyolHookEventNames.OrderCompleted,
                    OptiyolHookEventNames.OrderCompletedWithItems,
                    OptiyolHookEventNames.OrderCanceled,
                    OptiyolHookEventNames.OrderCanceledWithItems,
                    OptiyolHookEventNames.OrderUndoCanceled,
                    OptiyolHookEventNames.OrderReturned,
                    OptiyolHookEventNames.RouteFinished,
                    OptiyolHookEventNames.OrderPickupVirtualCompleted
                };
            var transactionId = Guid.NewGuid().ToString().ToLower();
            #region RawData

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var rawDataEntity = new OptiyolRawDataEntity();

            rawDataEntity.ServiceName = "Optiyol";
            rawDataEntity.TransactionId = transactionId;
            rawDataEntity.RawData = JsonSerializer.Serialize(model, serializeOptions);
            rawDataEntity.IsParsed = false;
            rawDataEntity.CreatedDate = DateTime.Now;

            #region IsNewEvent

            rawDataEntity.IsNewEvent = true;
            if (model != null)
            {
                if (model.Hook != null)
                {
                    var hookEventForNewData = model.Hook.Event.ToLower().Replace(" ", "");
                    if (validEvents.Contains(hookEventForNewData))
                    {
                        rawDataEntity.IsNewEvent = false;
                    }
                }
            }

            #endregion
            #region Save Order Status model
            var orderStatusModel = new OptiyolSaveStatusModel();
            orderStatusModel.EventName = model.Hook.Event;

            #endregion

            await _mMLogisticsUnitOfWork.BeginTransactionAsync();

            _mMLogisticsRepositoryWrapper.OptiyolRawDataRepository.Create(rawDataEntity);

            await _mMLogisticsUnitOfWork.SaveChangesAsync();
            await _mMLogisticsUnitOfWork.CommitAsync();

            #endregion


            // 1. Hook alanlarının boş olup olmadığını kontrol et
            var hookValidationErrors = WebhookValidateHook(model.Hook);
            if (hookValidationErrors.Count != 0)
            {

                _logger.LogError("<OptiyolService>ValidationErrors", string.Join(", ", hookValidationErrors));

                return Result(
                    code: StatusCodes.Status400BadRequest,
                    message: string.Format(_stringLocalizer["Global.MissingOrInvalidFields"],
                           string.Join(", ", hookValidationErrors))
                );
            }

            var hookEvent = model.Hook.Event.ToLower().Replace(" ", "");

            if (!validEvents.Contains(hookEvent))
            {

                _logger.LogInformation("<OptiyolService>hookEvent", string.Join(", ", hookEvent));

                return Result(
                    code: StatusCodes.Status404NotFound,
                    message: _stringLocalizer["OptiyolService.WebhookAsync.InvalidEventInfo"]
                );
            }
            try
            {

                await _mMLogisticsUnitOfWork.BeginTransactionAsync();

                var optiyolNotification = new OptiyolNotificationEntity()
                {
                    CreatedDate = datetimenow,
                    EventName = hookEvent,
                    HookId = model.Hook.Id,
                    TransactionId = transactionId
                };

                _mMLogisticsRepositoryWrapper.OptiyolNotificationsRepository.Create(optiyolNotification);
                await _mMLogisticsUnitOfWork.SaveChangesAsync();

                if (hookEvent == OptiyolHookEventNames.RoutePlanned)
                {
                    var data = ConvertToModelExtension.ConvertToModel<RoutePlannedModel>(model.Data);

                    optiyolNotification.RouteTrackerCode = data.RouteTrackerCode;

                    var entity = new OptiyolRoutePlannedEntity()
                    {
                        TransactionId = transactionId,
                        CompanyId = data.CompanyId,
                        CompanyName = data.CompanyName,
                        DriverEmail = data.DriverEmail,
                        DriverFirstName = data.DriverFirstName,
                        DriverLastName = data.DriverLastName,
                        RouteTrackerCode = data.RouteTrackerCode,
                        UserName = data.UserName,
                        VehicleCode = data.VehicleCode,
                        CreatedDate = datetimenow
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolRoutePlannedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                   .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                      .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                }
                else if (hookEvent == OptiyolHookEventNames.RouteLoadListCompleted)
                {
                    var data = ConvertToModelExtension.ConvertToModel<RouteLoadListCompletedModel>(model.Data);

                    optiyolNotification.RouteTrackerCode = data.RouteTrackerCode;
                    optiyolNotification.OrderId = data.Items.FirstOrDefault()?.OrderId;

                    var entity = new OptiyolRouteLoadListCompletedEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        LoadCompletionTime = data.LoadCompletionTime,
                        RouteTrackerCode = data.RouteTrackerCode,
                        VehicleCode = data.VehicleCode,
                        OptiyolRouteLoadListCompletedItems = data.Items.Select(i => new OptiyolRouteLoadListCompletedItemsEntity()
                        {
                            Channel = i.Channel,
                            TransactionId = transactionId,
                            LotNumber = i.LotNumber,
                            OrderId = i.OrderId,
                            OrderItemId = i.OrderId,
                            SkuId = i.SkuId,
                            SkuLoadQuantity = i.SkuLoadQuantity,
                            SkuName = i.SkuName,
                            UomQuantity = i.UomQuantity,
                            CreatedDate = datetimenow
                        }).ToList()
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolRouteLoadListCompletedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                   .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                       .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                }              
                else
                {

                    _logger.LogError("<OptiyolService>optiyolNotification", string.Join(", ", optiyolNotification));

                    return Result(string.Format(_stringLocalizer["Global.NotMatched"], "Event"), StatusCodes.Status404NotFound);
                }
                optiyolNotification.StatusId = await GetStatusId(optiyolNotification.EventName, optiyolNotification.ServiceType);
                _mMLogisticsRepositoryWrapper.OptiyolNotificationsRepository.Update(optiyolNotification);

                rawDataEntity.IsParsed = true;
                _mMLogisticsRepositoryWrapper.OptiyolRawDataRepository.Update(rawDataEntity);
                await _mMLogisticsUnitOfWork.SaveChangesAsync();
                await SaveOrderStatusAsync(orderStatusModel);


                await _mMLogisticsUnitOfWork.CommitAsync();

                return Result
                (
                    code: StatusCodes.Status200OK,
                    message: _stringLocalizer["Global.Success"].Value//"Başarılı İstek." mesajı
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("<OptiyolService>Error", ex.Message + "-" + ex);
                await _mMLogisticsUnitOfWork.RollbackAsync();

                return Result
                (
                    code: StatusCodes.Status500InternalServerError,
                    message: _stringLocalizer["Global.InternalServerError"].Value//"Uygulama Hatası" mesajı
                );
            }
        }
        public async Task<ServiceResultModel> WebhookAsync(OptiyolRequestModel model, string createdDate)
        {
            var datetimenow = DateTime.Now;//kaydedilecek bütün veriler aynı date parametresine sahip olması için

            if (!string.IsNullOrEmpty(createdDate))
            {
                datetimenow = Convert.ToDateTime(createdDate);
            }

            // 2. Geçersiz Event kontrolü
            var validEvents = new List<string>
                {
                    OptiyolHookEventNames.RoutePlanned,
                    OptiyolHookEventNames.RouteLoadListCompleted,
                    OptiyolHookEventNames.RouteStarted,
                    OptiyolHookEventNames.OrderArrived,
                    OptiyolHookEventNames.OrderCompleted,
                    OptiyolHookEventNames.OrderCompletedWithItems,
                    OptiyolHookEventNames.OrderCanceled,
                    OptiyolHookEventNames.OrderCanceledWithItems,
                    OptiyolHookEventNames.OrderUndoCanceled,
                    OptiyolHookEventNames.OrderReturned,
                    OptiyolHookEventNames.RouteFinished,
                    OptiyolHookEventNames.OrderPickupVirtualCompleted
                };
            var transactionId = Guid.NewGuid().ToString().ToLower();
            #region RawData

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var rawDataEntity = new OptiyolRawDataEntity();

            rawDataEntity.ServiceName = "Optiyol";
            rawDataEntity.TransactionId = transactionId;
            rawDataEntity.RawData = JsonSerializer.Serialize(model, serializeOptions);
            rawDataEntity.IsParsed = false;
            rawDataEntity.CreatedDate = DateTime.Now;

            #region IsNewEvent

            rawDataEntity.IsNewEvent = true;
            if (model != null)
            {
                if (model.Hook != null)
                {
                    var hookEventForNewData = model.Hook.Event.ToLower().Replace(" ", "");
                    if (validEvents.Contains(hookEventForNewData))
                    {
                        rawDataEntity.IsNewEvent = false;
                    }
                }
            }

            #endregion
            #region Save Order Status model
            var orderStatusModel = new OptiyolSaveStatusModel();
            orderStatusModel.EventName = model.Hook.Event;

            #endregion

            await _mMLogisticsUnitOfWork.BeginTransactionAsync();

            _mMLogisticsRepositoryWrapper.OptiyolRawDataRepository.Create(rawDataEntity);

            await _mMLogisticsUnitOfWork.SaveChangesAsync();
            await _mMLogisticsUnitOfWork.CommitAsync();

            #endregion


            // 1. Hook alanlarının boş olup olmadığını kontrol et
            var hookValidationErrors = WebhookValidateHook(model.Hook);
            if (hookValidationErrors.Count != 0)
            {

                _logger.LogError("<OptiyolService>ValidationErrors", string.Join(", ", hookValidationErrors));

                return Result(
                    code: StatusCodes.Status400BadRequest,
                    message: string.Format(_stringLocalizer["Global.MissingOrInvalidFields"],
                           string.Join(", ", hookValidationErrors))
                );
            }

            var hookEvent = model.Hook.Event.ToLower().Replace(" ", "");

            if (!validEvents.Contains(hookEvent))
            {

                _logger.LogInformation("<OptiyolService>hookEvent", string.Join(", ", hookEvent));

                return Result(
                    code: StatusCodes.Status404NotFound,
                    message: _stringLocalizer["OptiyolService.WebhookAsync.InvalidEventInfo"]
                );
            }
            try
            {
                //// 3. Data validasyonu
                //var validationErrors = WebhookValidateDataModel(model.Data, hookEvent);
                //if (validationErrors.Count != 0)
                //{
                //    rawDataEntity.IsNewEvent = false;
                //    rawDataEntity.IsParsed = false;

                //    await _mMLogisticsUnitOfWork.BeginTransactionAsync();

                //    _mMLogisticsRepositoryWrapper.OptiyolRawDataRepository.Create(rawDataEntity);

                //    await _mMLogisticsUnitOfWork.SaveChangesAsync();
                //    await _mMLogisticsUnitOfWork.CommitAsync();

                //    Log.ForContext("ValidationErrors", string.Join(", ", validationErrors))
                //    .Error(_stringLocalizer["Global.InvalidParameters"]);

                //    //return Result(
                //    //    code: StatusCodes.Status400BadRequest,
                //    //    message: string.Format(_stringLocalizer["Global.MissingOrInvalidFields"],
                //    //       string.Join(", ", validationErrors))
                //    //);
                //}

                await _mMLogisticsUnitOfWork.BeginTransactionAsync();

                var optiyolNotification = new OptiyolNotificationEntity()
                {
                    CreatedDate = datetimenow,
                    EventName = hookEvent,
                    HookId = model.Hook.Id,
                    TransactionId = transactionId
                };

                _mMLogisticsRepositoryWrapper.OptiyolNotificationsRepository.Create(optiyolNotification);
                await _mMLogisticsUnitOfWork.SaveChangesAsync();

                if (hookEvent == OptiyolHookEventNames.RoutePlanned)
                {
                    var data = ConvertToModelExtension.ConvertToModel<RoutePlannedModel>(model.Data);

                    optiyolNotification.RouteTrackerCode = data.RouteTrackerCode;

                    var entity = new OptiyolRoutePlannedEntity()
                    {
                        TransactionId = transactionId,
                        CompanyId = data.CompanyId,
                        CompanyName = data.CompanyName,
                        DriverEmail = data.DriverEmail,
                        DriverFirstName = data.DriverFirstName,
                        DriverLastName = data.DriverLastName,
                        RouteTrackerCode = data.RouteTrackerCode,
                        UserName = data.UserName,
                        VehicleCode = data.VehicleCode,
                        CreatedDate = datetimenow
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolRoutePlannedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                   .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                      .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                }
                else if (hookEvent == OptiyolHookEventNames.RouteLoadListCompleted)
                {
                    var data = ConvertToModelExtension.ConvertToModel<RouteLoadListCompletedModel>(model.Data);

                    optiyolNotification.RouteTrackerCode = data.RouteTrackerCode;
                    optiyolNotification.OrderId = data.Items.FirstOrDefault()?.OrderId;

                    var entity = new OptiyolRouteLoadListCompletedEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        LoadCompletionTime = data.LoadCompletionTime,
                        RouteTrackerCode = data.RouteTrackerCode,
                        VehicleCode = data.VehicleCode,
                        OptiyolRouteLoadListCompletedItems = data.Items.Select(i => new OptiyolRouteLoadListCompletedItemsEntity()
                        {
                            Channel = i.Channel,
                            TransactionId = transactionId,
                            LotNumber = i.LotNumber,
                            OrderId = i.OrderId,
                            OrderItemId = i.OrderId,
                            SkuId = i.SkuId,
                            SkuLoadQuantity = i.SkuLoadQuantity,
                            SkuName = i.SkuName,
                            UomQuantity = i.UomQuantity,
                            CreatedDate = datetimenow
                        }).ToList()
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolRouteLoadListCompletedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                   .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                       .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                }
                else if (hookEvent == OptiyolHookEventNames.RouteStarted)
                {

                    var data = ConvertToModelExtension.ConvertToModel<RouterStartedModel>(model.Data);

                    optiyolNotification.RouteTrackerCode = data.RouteTrackerCode;
                    optiyolNotification.OrderId = data.Orders.FirstOrDefault()?.OrderId;

                    var entity = new OptiyolRouterStartedEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        CompanyId = data.CompanyId,
                        Lat = data.Lat,
                        Lon = data.Lon,
                        NumberOfOrders = data.NumberOfOrders,
                        NumberOfStops = data.NumberOfStops,
                        RouteTrackerCode = data.RouteTrackerCode,
                        StartTime = data.StartTime,
                        VehicleCode = data.VehicleCode,
                        OptiyolRouterStartedOrders = data.Orders.Select(i => new OptiyolRouterStartedOrdersEntity()
                        {
                            TransactionId = transactionId,
                            CreatedDate = datetimenow,
                            DeliveryTimeLower = i.DeliveryTimeLower,
                            IsLoaded = i.IsLoaded,
                            DeliveryTimeUpper = i.DeliveryTimeUpper,
                            OrderId = i.OrderId,
                            Sequence = i.Sequence != default ? i.Sequence : default,
                            ServiceType = i.ServiceType,
                            TrackUrl = i.TrackUrl,
                            VisitSequence = i.VisitSequence != default ? i.VisitSequence : default,

                        }).ToList(),
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolRouterStartedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                   .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update
                    foreach (var order in entity.OptiyolRouterStartedOrders)
                    {
                        if (order.OrderId.ToLower() != "start" && order.OrderId.ToLower() != "end")
                            orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                            {
                                OrderId = order.OrderId,
                                ServiceType = order.ServiceType,
                            });

                    }
                }
                else if (hookEvent == OptiyolHookEventNames.OrderArrived)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderArrivedModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderArrivedEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        Channel = data.Channel,
                        ArrivedTime = data.ArrivedTime,
                        ArrivalTime = data.ArrivalTime,
                        ArrivedLat = data.ArrivedLat,
                        ArrivedLon = data.ArrivedLon,
                        CustomerName = data.CustomerName,
                        CompanyId = data.CompanyId.ToString(),
                        IsArrived = data.IsArrived,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        OrderId = data.OrderId,
                        Otp = data.Otp,
                        Recipients = string.Join(",", data.Recipients),
                        ServiceType = data.ServiceType,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderArrivedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                        .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                        .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });



                }
                else if (hookEvent == OptiyolHookEventNames.OrderCompleted)
                {

                    var data = ConvertToModelExtension.ConvertToModel<OrderCompletedModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderCompletedEntity()
                    {
                        TransactionId = transactionId,
                        Recipients = string.Join(",", data.Recipients),
                        Images = string.Join(", ", data.Images),
                        CreatedDate = datetimenow,
                        Channel = data.Channel,
                        ArrivedLat = data.ArrivedLat,
                        ArrivedLon = data.ArrivedLon,
                        CustomerName = data.CustomerName,
                        CompanyId = data.CompanyId,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence,
                        CompletedTime = data.CompletedTime,
                        ContactPerson = data.ContactPerson,
                        ContactPersonPhone = data.ContactPersonPhone,
                        ContactPersonRelation = data.ContactPersonRelation,
                        CustomerNote = data.CustomerNote,
                        IsComplete = data.IsComplete,
                        IsDeliveredToOwner = data.IsDeliveredToOwner,
                        Note = data.Note,
                        PaymentMethod = data.PaymentMethod,
                        PlannedCompleteTime = data.PlannedCompleteTime,
                        RouteStartTime = (DateTime)data.RouteStartTime,
                        Sign = data.Sign//,
                        //OptiyolOrderCompletedImages = data.Images.Select(a => new OptiyolOrderCompletedImagesEntity()
                        //{
                        //    TransactionId = transactionId,
                        //    ImageUrl = a.ToString(),
                        //    CreatedDate = datetimenow,

                        //}).ToList()
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderCompletedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });

                }
                else if (hookEvent == OptiyolHookEventNames.OrderCompletedWithItems)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderCompletedWithItemModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderCompletedWithItemEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        ArrivedLat = data.ArrivedLat,
                        ArrivedLon = data.ArrivedLon,
                        Channel = data.Channel,
                        ChannelExtraData = JsonSerializer.Serialize(data.ChannelExtraData),
                        ClaimId = data.ClaimId,
                        CompanyId = data.CompanyId,
                        CompletedTime = data.CompletedTime,
                        ContactPerson = data.ContactPerson,
                        ContactPersonPhone = data.ContactPersonPhone,
                        ContactPersonRelation = data.ContactPersonRelation,
                        CustomerName = data.CustomerName,
                        CustomerNote = data.CustomerNote,
                        IsComplete = data.IsComplete,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        Note = data.Note,
                        OrderId = data.OrderId,
                        OrderPk = data.OrderPk,
                        PaymentMethod = data.PaymentMethod,
                        PlannedCompleteTime = data.PlannedCompleteTime,
                        Recipients = string.Join(",", data.Recipients),
                        ServiceType = data.ServiceType,
                        Sign = data.Sign,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence,
                        OptiyolOrderCompletedWithItems = data.Items.Select(i => new OptiyolOrderCompletedWithItemsEntity()
                        {
                            TransactionId = transactionId,
                            CreatedDate = datetimenow,
                            ActualPrice = i.ActualPrice,
                            ActualSkuPercentageQuantity = i.ActualSkuPercentageQuantity,
                            ActualSkuQuantity = i.ActualSkuQuantity,
                            Barcode = i.Barcode,
                            CancelReason = i.CancelReason,
                            DriverNote = i.DriverNote,
                            OrderItemId = i.OrderItemId,
                            PlannedPrice = i.PlannedPrice,
                            PlannedSkuQuantity = i.PlannedSkuQuantity,
                            SkuId = i.SkuId,
                            SkuName = i.SkuName,
                            Status = i.Status,
                            UomQuantity = i.UomQuantity,
                            OptiyolOrderCompletedWithItemLotNumbers = i.ActualSkuLotNumbers.Select(p => new OptiyolOrderCompletedWithItemLotNumbersEntity()
                            {
                                TransactionId = transactionId,
                                CreatedDate = datetimenow,
                                LotNumber = p.LotNumber,
                                Quantity = p.Quantity,
                            }).ToList(),
                        }).ToList(),
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderCompletedWithItemRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                        .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                        .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceTypes for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });

                }
                else if (hookEvent == OptiyolHookEventNames.OrderCanceled)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderCanceledModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderCanceledEntity()
                    {
                        TransactionId = transactionId,
                        Recipients = string.Join(",", data.Recipients),
                        Images = string.Join(", ", data.Images),
                        CreatedDate = datetimenow,
                        CanceledTime = data.CanceledTime,
                        CancelReason = data.CancelReason,
                        Channel = data.Channel,
                        CompanyId = data.CompanyId,
                        CustomerName = data.CustomerName,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        ArrivedLat = data.ArrivedLat,
                        ArrivedLon = data.ArrivedLat,
                        IsCanceled = data.IsCanceled,
                        Note = data.Note,
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence,
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderCanceledsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });

                }
                else if (hookEvent == OptiyolHookEventNames.OrderCanceledWithItems)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderCanceledWithItemsModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderCanceledWithItemsEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        CancelReason = data.CancelReason,
                        Channel = data.Channel,
                        CompanyId = data.CompanyId,
                        CustomerName = data.CustomerName,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        Note = data.Note,
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence,
                        ChannelExtraData = JsonSerializer.Serialize(data.ChannelExtraData),
                        LocationLon = data.LocationLon,
                        CanceledTime = data.CanceledTime,
                        ClaimId = data.ClaimId,
                        IsCanceled = data.IsCanceled,
                        LocationLat = data.LocationLat,
                        Recipients = string.Join(",", data.Recipients),
                        OptiyolOrderCanceledWithItemList = data.Items.Select(i => new OptiyolOrderCanceledWithItemListEntity()
                        {
                            OrderItemId = transactionId,
                            TransactionId = i.OrderItemId,
                            CreatedDate = datetimenow,
                            CancelReason = i.CancelReason,
                            ActualPrice = i.ActualPrice,
                            ActualSkuPercentageQuantity = i.ActualSkuPercentageQuantity,
                            ActualSkuQuantity = i.ActualSkuQuantity,
                            Barcode = i.Barcode,
                            DriverNote = i.DriverNote,
                            PlannedPrice = i.PlannedPrice,
                            PlannedSkuQuantity = i.PlannedSkuQuantity,
                            SkuId = i.SkuId,
                            SkuName = i.SkuName,
                            Status = i.Status,
                            UomQuantity = i.UomQuantity,

                        }).ToList(),

                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderCanceledWithItemsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });
                }
                else if (hookEvent == OptiyolHookEventNames.OrderUndoCanceled)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderUndoCanceledModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderUndoCanceledEntity()
                    {
                        TransactionId = transactionId,
                        Recipients = string.Join(",", data.Recipients),
                        CreatedDate = datetimenow,
                        Channel = data.Channel,
                        CompanyId = data.CompanyId,
                        CustomerName = data.CustomerName,
                        IsCanceled = data.IsCanceled,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        ArrivedLat = data.ArrivedLat,
                        ArrivedLon = data.ArrivedLon,
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                        UndoCanceledTime = data.UndoCanceledTime,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence,

                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderUndoCanceledsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });
                }
                else if (hookEvent == OptiyolHookEventNames.OrderReturned)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderReturnedModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderReturnedEntity()
                    {
                        TransactionId = transactionId,
                        CompanyId = data.CompanyId,
                        Recipients = string.Join(",", data.Recipients),
                        Images = string.Join(", ", data.Images),
                        CreatedDate = datetimenow,
                        CustomerName = data.CustomerName,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                        VehicleCode = data.VehicleCode,
                        Channel = data.Channel,
                        VisitSequence = data.VisitSequence,
                        ContactPerson = data.ContactPerson,
                        ContactPersonPhone = data.ContactPersonPhone,
                        ContactPersonRelation = data.ContactPersonRelation,
                        IsReturned = data.IsReturned,
                        Note = data.Note,
                        ReturnedLat = data.ReturnedLat,
                        ReturnedLocation = data.ReturnedLocation,
                        ReturnedLon = data.ReturnedLon,
                        ReturnedTime = data.ReturnedTime,
                        Sign = data.Sign

                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderReturnedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });
                }
                else if (hookEvent == OptiyolHookEventNames.RouteFinished)
                {
                    var data = ConvertToModelExtension.ConvertToModel<RouteFinishedModel>(model.Data);

                    optiyolNotification.RouteTrackerCode = data.RouteTrackerCode;
                    optiyolNotification.OrderId = data.Orders.FirstOrDefault()?.OrderId;

                    var entity = new OptiyolRouteFinishedEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        CompanyId = data.CompanyId,
                        FinishTime = data.FinishTime,
                        Lat = data.Lat,
                        Lon = data.Lon,
                        NumberOfOrders = data.NumberOfOrders,
                        NumberOfNotCompletedOrders = data.NumberOfNotCompletedOrders,
                        NumberOfStops = data.NumberOfStops,
                        RouteTrackerCode = data.RouteTrackerCode,
                        VehicleCode = data.VehicleCode,
                        NumberOfCompletedOrders = data.NumberOfCompletedOrders,
                        NumberOfCanceledOrders = data.NumberOfCanceledOrders,
                        OptiyolRouterFinishedOrders = data.Orders.Select(i => new OptiyolRouterFinishedOrdersEntity()
                        {
                            TransactionId = transactionId,
                            CreatedDate = datetimenow,
                            OrderId = i.OrderId

                        }).ToList(),
                    };

                    _mMLogisticsRepositoryWrapper.OptiyolRouteFinishedsRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update
                    foreach (var order in entity.OptiyolRouterFinishedOrders)
                    {
                        if (order.OrderId.ToLower() != "start" && order.OrderId.ToLower() != "end")
                            orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                            {
                                OrderId = order.OrderId,
                                //ServiceType = order.ServiceType,
                            });

                    }

                }
                else if (hookEvent == OptiyolHookEventNames.OrderPickupVirtualCompleted)
                {
                    var data = ConvertToModelExtension.ConvertToModel<OrderPickupVirtualCompletedModel>(model.Data);

                    optiyolNotification.OrderId = data.OrderId;
                    optiyolNotification.ServiceType = data.ServiceType;

                    var entity = new OptiyolOrderPickupVirtualCompletedEntity()
                    {
                        TransactionId = transactionId,
                        CreatedDate = datetimenow,
                        Channel = data.Channel,
                        ArrivedLat = data.ArrivedLat,
                        ArrivedLon = data.ArrivedLon,
                        CustomerName = data.CustomerName,
                        CompanyId = data.CompanyId,
                        IsDropoff = data.IsDropoff,
                        IsLocationVerified = data.IsLocationVerified,
                        IsPickup = data.IsPickup,
                        LocationAddress = data.LocationAddress,
                        LocationId = data.LocationId,
                        LocationLat = data.LocationLat,
                        LocationLon = data.LocationLon,
                        OrderId = data.OrderId,
                        Recipients = string.Join(",", data.Recipients),
                        Images = string.Join(", ", data.Images),
                        ServiceType = data.ServiceType,
                        VehicleCode = data.VehicleCode,
                        VisitSequence = data.VisitSequence,
                        ClaimId = data.ClaimId,
                        //ChannelExtraData = string.Join(",", data.ChannelExtraData),
                        OrderPK = data.OrderPK,
                        IsComplete = data.IsComplete,
                        RouteStartTime = data.RouteStartTime,
                        CompletedTime = data.CompletedTime,
                        PlannedCompleteTime = data.PlannedCompleteTime,
                        IsDeliveredToOwner = data.IsDeliveredToOwner,
                        ContactPersonRelation = data.ContactPersonRelation,
                        ContactPerson = data.ContactPerson,
                        ContactPersonPhone = data.ContactPersonPhone,
                        CustomerNote = data.CustomerNote,
                        PaymentMethod = data.PaymentMethod,
                        Note = data.Note,
                        Sign = data.Sign

                    };

                    _mMLogisticsRepositoryWrapper.OptiyolOrderPickupVirtualCompletedRepository.Create(entity);

                    await _mMLogisticsUnitOfWork.SaveChangesAsync();

                    Log.ForContext(nameof(model.Hook.Event), hookEvent)
                    .ForContext(nameof(optiyolNotification), optiyolNotification.Id)
                    .ForContext(nameof(optiyolNotification.RouteTrackerCode), optiyolNotification.RouteTrackerCode)
                    .Information(string.Format(_stringLocalizer["Global.EntityCreated"], nameof(OptiyolNotificationEntity)));

                    //Take OrderIds And ServiceType for status update

                    orderStatusModel.Orders.Add(new OptiyolSaveStatusDetail
                    {
                        OrderId = data.OrderId,
                        ServiceType = data.ServiceType,
                    });
                }
                else
                {

                    _logger.LogError("<OptiyolService>optiyolNotification", string.Join(", ", optiyolNotification));

                    return Result(string.Format(_stringLocalizer["Global.NotMatched"], "Event"), StatusCodes.Status404NotFound);
                }
                optiyolNotification.StatusId = await GetStatusId(optiyolNotification.EventName, optiyolNotification.ServiceType);
                _mMLogisticsRepositoryWrapper.OptiyolNotificationsRepository.Update(optiyolNotification);

                rawDataEntity.IsParsed = true;
                _mMLogisticsRepositoryWrapper.OptiyolRawDataRepository.Update(rawDataEntity);
                await _mMLogisticsUnitOfWork.SaveChangesAsync();
                await SaveOrderStatusAsync(orderStatusModel);


                await _mMLogisticsUnitOfWork.CommitAsync();

                return Result
                (
                    code: StatusCodes.Status200OK,
                    message: _stringLocalizer["Global.Success"].Value//"Başarılı İstek." mesajı
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("<OptiyolService>Error", ex.Message + "-" + ex);
                await _mMLogisticsUnitOfWork.RollbackAsync();

                return Result
                (
                    code: StatusCodes.Status500InternalServerError,
                    message: _stringLocalizer["Global.InternalServerError"].Value//"Uygulama Hatası" mesajı
                );
            }
        }
        public async Task<ServiceResultModel> CreateBarcodeAsync(OptiyolBarcodeCreateRequestModel model)
        {
            try
            {
                if (model.Barcodes == null || model.Barcodes.Any() == false) return new ServiceResultModel { Code = 400, Message = "Barkod bilgileri zorunludur" };
                var barcode = await GetLatestBarcodeIdAsync();
                if (barcode < 0) return new ServiceResultModel { Code = 500, Message = "Barkod oluşturulamadı" };
                var entities = new List<OptiyolBarcodeHeadEntity>();
                foreach (var item in model.Barcodes)
                {
                    barcode++;
                    entities.Add(new OptiyolBarcodeHeadEntity
                    {
                        Barcode = (long)(Math.Pow(10, 8) * DateTime.Now.Year) + barcode,
                        Status = 0,
                        ServiceType = item.ServiceType,
                        FromLocationId = item.FromLocationId,
                        ToLocationId = item.ToLocationId,
                        ToLocationName = item.ToLocationName,
                        ToLocationAddress = item.ToLocationAddress,
                        ToLocationCounty = item.ToLocationCounty,
                        ToLocationCity = item.ToLocationCity,
                        ToLocationLatitude = item.ToLocationLatitude,
                        ToLocationLongitude = item.ToLocationLongitude,
                        ToLocationPhone = item.ToLocationPhone,
                        Volume = item.Volume,
                        Weight = item.Weight,
                        Note = item.Note,
                        RequiresPaymentAtDoor = item.RequiresPaymentAtDoor == null ? "" : item.RequiresPaymentAtDoor.ToString(),
                        PaymentMethod = item.PaymentMethod,
                        PaymentAmount = item.PaymentAmount,
                        EarliestPickupTime = item.EarliestPickupTime,
                        LatestPickupTime = item.LatestPickupTime,
                        EarliestDeliveryTime = item.EarliestDeliveryTime,
                        LatestDeliveryTime = item.LatestDeliveryTime,
                        RequiredVehicleProperties = item.RequiredVehicleProperties == null ? null : string.Join(',', item.RequiredVehicleProperties),
                        BarcodeProperties = item.BarcodeProperties == null ? null : string.Join(',', item.BarcodeProperties),
                        BarcodePlannedDate = model.BarcodesPlannedDate,
                        BarcodePlannedTime = model.BarcodesPlannedTime
                    });
                }

                _mMLogisticsUnitOfWork.BeginTransaction();
                _mMLogisticsRepositoryWrapper.OptiyolBarcodeHeadRepository.BulkInsert(entities);
                await _mMLogisticsUnitOfWork.SaveChangesAsync();
                await UpdateLatestBarcodeIdAsync(barcode);
                await _mMLogisticsUnitOfWork.CommitAsync();
                return new ServiceResultModel { Code = 200, Message = "Barkod oluşturuldu" };
            }
            catch (Exception ex)
            {
                await _mMLogisticsUnitOfWork.RollbackAsync();

                return Result
                (
                    code: StatusCodes.Status500InternalServerError,
                    message: _stringLocalizer["Global.InternalServerError"].Value//"Uygulama Hatası" mesajı
                );
            }
        }
        public async Task<ServiceResultModel> CancelBarcodeAsync(OptiyolBarcodeCancelRequestModel model)
        {
            try
            {
                var entity = await _mMLogisticsRepositoryWrapper.OptiyolBarcodeHeadRepository.GetQuery().FirstOrDefaultAsync(b => b.Barcode.ToString() == model.OrderId);
                if (entity == null) return new ServiceResultModel { Code = 404, Message = "Barkod bulunamadı" };
                var result = await _optiyolIntegrationAdapter.CancelBarcodeAsync(model);
                if (result.Code == 200)
                {
                    entity.Status = 9;
                    _mMLogisticsUnitOfWork.BeginTransaction();
                    _mMLogisticsRepositoryWrapper.OptiyolBarcodeHeadRepository.Update(entity);
                    await _mMLogisticsUnitOfWork.SaveChangesAsync();
                    _mMLogisticsUnitOfWork.Commit();
                    return new ServiceResultModel { Code = 200, Message = "İptal işlemi başarılı" };
                }
                else
                {
                    return new ServiceResultModel { Code = 400, Message = result.Message };
                }

            }
            catch (Exception ex)
            {
                _mMLogisticsUnitOfWork.Rollback();
                return new ServiceResultModel { Code = 500, Message = "Beklenmeyen bir hata oluştu" };
            }
        }

        /// <summary>
        /// Hook verisinin zorunlu alanlarını kontrol eder
        /// </summary>
        /// <param name="hook">Kontrol edilecek hook nesnesi</param>
        /// <returns>Hataları içeren bir liste</returns>
        private List<string> WebhookValidateHook(OptiyolRequestHookModel hook)
        {
            var errors = new List<string>();

            if (hook == null)
            {
                errors.Add(string.Format(_stringLocalizer["Global.InvalidModel"], "hook"));
                return errors;
            }

            if (hook.Id <= 0)
                errors.Add(string.Format(_stringLocalizer["Global.InvalidIdParameter"], "Hook Id"));

            if (string.IsNullOrWhiteSpace(hook.Event))
                errors.Add(string.Format(_stringLocalizer["Global.InvalidStringParameter"], "Hook Event"));

            if (string.IsNullOrWhiteSpace(hook.Target))
                errors.Add(string.Format(_stringLocalizer["Global.InvalidStringParameter"], "Hook Target"));

            return errors;
        }

        /// <summary>
        /// Data validasyon metodu
        /// </summary>
        private List<string> WebhookValidateDataModel(object data, string hookEvent)
        {
            var errors = new List<string>();

            try
            {
                // Model türüne göre doğru tipe çevir
                object typedModel = hookEvent switch
                {
                    OptiyolHookEventNames.RoutePlanned => ConvertToModelExtension.ConvertToModel<RoutePlannedModel>(data),
                    OptiyolHookEventNames.RouteLoadListCompleted => ConvertToModelExtension.ConvertToModel<RouteLoadListCompletedModel>(data),
                    OptiyolHookEventNames.RouteStarted => ConvertToModelExtension.ConvertToModel<RouterStartedModel>(data),
                    OptiyolHookEventNames.OrderArrived => ConvertToModelExtension.ConvertToModel<OrderArrivedModel>(data),
                    OptiyolHookEventNames.OrderCompleted => ConvertToModelExtension.ConvertToModel<OrderCompletedModel>(data),
                    OptiyolHookEventNames.OrderCompletedWithItems => ConvertToModelExtension.ConvertToModel<OrderCompletedWithItemModel>(data),
                    OptiyolHookEventNames.OrderCanceled => ConvertToModelExtension.ConvertToModel<OrderCanceledModel>(data),
                    OptiyolHookEventNames.OrderCanceledWithItems => ConvertToModelExtension.ConvertToModel<OrderCanceledWithItemsModel>(data),
                    OptiyolHookEventNames.OrderUndoCanceled => ConvertToModelExtension.ConvertToModel<OrderUndoCanceledModel>(data),
                    OptiyolHookEventNames.OrderReturned => ConvertToModelExtension.ConvertToModel<OrderReturnedModel>(data),
                    OptiyolHookEventNames.RouteFinished => ConvertToModelExtension.ConvertToModel<RouteFinishedModel>(data),
                    OptiyolHookEventNames.OrderPickupVirtualCompleted => ConvertToModelExtension.ConvertToModel<OrderPickupVirtualCompletedModel>(data),
                    _ => throw new ArgumentException(string.Format(_stringLocalizer["Global.NotMatched"], "Event"))
                };

                // ValidationContext ile model validasyonu
                var validationContext = new ValidationContext(typedModel);
                Validator.ValidateObject(typedModel, validationContext, validateAllProperties: true);
            }
            catch (ValidationException ex)
            {
                errors.Add(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                errors.Add(string.Format(_stringLocalizer["Global.ExceptionConvert"], ex.Message));
            }
            catch (Exception ex)
            {
                errors.Add(string.Format(_stringLocalizer["Global.UnknownError"], ex.Message));
            }

            return errors;
        }
        private bool CheckConvertToModel<T>(T model)
        {
            try
            {
                var result = ConvertToModelExtension.ConvertToModel<OrderCompletedModel>(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private async Task SaveOrderStatusAsync(OptiyolSaveStatusModel model)
        {
            if (model.Orders.Any())
            {
                var statuses = await _mMLogisticsRepositoryWrapper.OptiyolStatusRepository.GetQuery()
                    .Select(s => new OptiyolStatusEntity
                    {
                        EventName = s.EventName == null ? null : s.EventName.Trim().Replace("\r", "").Replace("\n", ""),
                        ServiceType = s.ServiceType == null ? null : s.ServiceType.Trim().Replace("\r", "").Replace("\n", ""),
                        Id = s.Id,
                        Description = s.Description,
                        StatusName = s.StatusName,
                        CreatedDate = s.CreatedDate
                    })
                    .ToListAsync();


                foreach (var item in model.Orders)
                {
                    if (!string.IsNullOrEmpty(item.ServiceType))
                    {
                        var status = statuses.FirstOrDefault(s => s.ServiceType == item.ServiceType && s.EventName == model.EventName);
                        if (status != null)
                        {
                            var orderStatusEntity = await _mMLogisticsRepositoryWrapper.OptiyolOrderStatusRepository.GetQuery()
                            .FirstOrDefaultAsync(s => s.OrderId == item.OrderId);
                            if (orderStatusEntity != null)
                            {
                                //update order status 
                                orderStatusEntity.StatusId = status.Id;
                                orderStatusEntity.UpdatedDate = DateTime.Now;
                                _mMLogisticsRepositoryWrapper.OptiyolOrderStatusRepository.Update(orderStatusEntity);
                            }
                            else
                            {
                                //create order status if not exist
                                orderStatusEntity = new OptiyolOrderStatusEntity
                                {
                                    OrderId = item.OrderId,
                                    StatusId = status.Id
                                };
                                _mMLogisticsRepositoryWrapper.OptiyolOrderStatusRepository.Create(orderStatusEntity);
                            }
                        }
                    }
                }

                await _mMLogisticsUnitOfWork.SaveChangesAsync();
            }
        }
        private async Task<int?> GetStatusId(string eventName, string ServiceType)
        {
            if (String.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(ServiceType)) return null;
            var statuses = await _mMLogisticsRepositoryWrapper.OptiyolStatusRepository.GetQuery().ToListAsync();
            var status = statuses.FirstOrDefault(s => s.ServiceType?.Trim() == ServiceType && s.EventName?.Trim() == eventName);
            if (status == null) return null;
            return status.Id;

        }
        private async Task<int> GetLatestBarcodeIdAsync()
        {
            try
            {
                var latestBarcode = await _mMLogisticsRepositoryWrapper.OptiyolBarcodeYearIdLogsRepository.GetQuery().Where(b => b.Year == DateTime.Now.Year).FirstOrDefaultAsync();
                if (latestBarcode != null)
                {
                    return latestBarcode.LastId;
                }
                else
                {
                    var newYearRecord = new OptiyolBarcodeYearIdLogsEntity
                    {
                        Id = new Guid(),
                        Year = DateTime.Now.Year,
                        LastId = 0
                    };
                    _mMLogisticsRepositoryWrapper.OptiyolBarcodeYearIdLogsRepository.Create(newYearRecord);
                    _mMLogisticsUnitOfWork.SaveChanges();
                    return await GetLatestBarcodeIdAsync();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private async Task UpdateLatestBarcodeIdAsync(int lastId)
        {
            try
            {
                var latestBarcode = await _mMLogisticsRepositoryWrapper.OptiyolBarcodeYearIdLogsRepository.GetQuery().Where(b => b.Year == DateTime.Now.Year).FirstOrDefaultAsync();
                latestBarcode.LastId = lastId;
                _mMLogisticsRepositoryWrapper.OptiyolBarcodeYearIdLogsRepository.Update(latestBarcode);
                await _mMLogisticsUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
