using IdentityModel.OidcClient;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MM.IT.Common.Enums;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MasterData;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Common.Extensions;
using NetTopologySuite.Index.HPRtree;

namespace MM.IT.Core.Services.MMCustomerInfo
{
    public class MMCustomerInfoService : BaseService, IMMCustomerInfoService
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly ILogger<MMCustomerInfoService> _logger;
        private readonly ISterlingOrderClientService _sterlingOrderClientService;
        public MMCustomerInfoService(IServiceProvider serviceProvider
            , IServiceWrapper serviceWrapper
            , ILogger<MMCustomerInfoService> logger
            , ISterlingOrderClientService sterlingOrderClientService) : base(serviceProvider)
        {
            _serviceWrapper = serviceWrapper;
            _logger = logger;
            _sterlingOrderClientService = sterlingOrderClientService;
        }

        public async Task<ServiceResultModel<CustomerInfoSummaryModel>> GetCustomerInfo(CustomerInfoRequestModel model)
        {
            var response = new CustomerInfoSummaryModel();
            try
            {

                #region checkModel

                if (!string.IsNullOrEmpty(model.referenceType))
                {
                    if (string.IsNullOrEmpty(model.referenceValue))
                    {
                        return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.NotNullReferenceTypeOReferenceValue.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullReferenceTypeOReferenceValue);
                    }
                }
                else if (!string.IsNullOrEmpty(model.referenceValue))
                {
                    if (string.IsNullOrEmpty(model.referenceType))
                    {
                        return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.NotNullReferenceTypeOReferenceValue.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullReferenceTypeOReferenceValue);
                    }
                }
                if (string.IsNullOrEmpty(model.Mobile))
                {
                    if (string.IsNullOrEmpty(model.email))
                    {
                        if (string.IsNullOrEmpty(model.referenceType) && string.IsNullOrEmpty(model.referenceValue))
                        {
                            return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.NotNullMobileOrEmailReferenceTypeOReferenceValue.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotNullMobileOrEmailReferenceTypeOReferenceValue);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(model.Mobile))
                {
                    var mobileLength = model.Mobile.Length;
                    if (mobileLength > 16)
                    {
                        return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.ExceedMobile.GetDisplayName(), (int)MMCustomerInfoMessageCodes.ExceedMobile);
                    }
                }
                #endregion

                var url = "";

                if (!string.IsNullOrEmpty(model.referenceType) && !string.IsNullOrEmpty(model.referenceValue))
                {
                    if (!string.IsNullOrEmpty(model.Mobile) && !string.IsNullOrEmpty(model.email))
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&referenceType={model.referenceType}&Mobile={model.Mobile.Trim().Replace(" ", "")}&email={model.email}&referenceValue={model.referenceValue}";
                    }
                    else if (!string.IsNullOrEmpty(model.Mobile))
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&Mobile={model.Mobile.Trim().Replace(" ", "")}";
                    }
                    else if (!string.IsNullOrEmpty(model.email))
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&email={model.email}";
                    }
                    else
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&referenceType={model.referenceType}&referenceValue={model.referenceValue}";

                    }
                }
                else
                {

                    if (!string.IsNullOrEmpty(model.Mobile) && !string.IsNullOrEmpty(model.email))
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&email={model.email}&Mobile={model.Mobile.Trim().Replace(" ", "")}";
                    }
                    else if (!string.IsNullOrEmpty(model.Mobile))
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&Mobile={model.Mobile.Trim().Replace(" ", "")}";
                    }
                    else if (!string.IsNullOrEmpty(model.email))
                    {
                        url = $"customers?result_mode=FULL&search_type=EXACT_CASE_INSENSITIVE&email={model.email}";
                    }
                }
                var result = await _sterlingOrderClientService.GetCustomerInfoAsync(url);

                if (result.Data != null)
                {
                    var myResponse = new CustomerInfoSummaryModel();

                    if (result.Data.Count > 1)
                    {

                        var customerInfoResponses = new List<CustomerInfoResponseModel>();
                        foreach (var dataItem in result.Data)
                        {
                            var isData = dataItem.legal_agreements.Where(b => b.name == "Loyalty");
                            if (isData.Any())
                            {
                                customerInfoResponses.Add(dataItem);
                            }
                        }

                        if (customerInfoResponses.Any())
                        {
                            var customerInfoResponsesForPARTYUUID = customerInfoResponses;

                            var leModels = new List<LegalAgreementsExternalReferenceSummaryModel>();

                            foreach (var customerInfoResponsesForPARTYUUIDItem in customerInfoResponsesForPARTYUUID)
                            {

                                var leModel = new LegalAgreementsExternalReferenceSummaryModel();

                                var legal_agreements = new List<legal_agreement>();
                                var external_references = new List<external_reference>();

                                var legalAgreements = customerInfoResponsesForPARTYUUIDItem.legal_agreements.Where(b => b.name == "Loyalty");
                                legal_agreements.AddRange(legalAgreements);

                                var externalReferences = customerInfoResponsesForPARTYUUIDItem.external_references.Where(b => b.reference_type == "PARTYUUID");
                                external_references.AddRange(externalReferences);

                                leModel.legal_agreements = legal_agreements;
                                leModel.external_references = external_references;

                                leModels.Add(leModel);


                            }
                            if (leModels.Any())
                            {

                                foreach (var externalReferenceItem in leModels)
                                {
                                    foreach (var item in externalReferenceItem.external_references)
                                    {
                                        foreach (var leModelItem in leModels)
                                        {
                                            var isData = leModelItem.external_references.Where(a => a.reference_value == item.reference_value);

                                            if (!isData.Any())
                                            {
                                                return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);

                                            }
                                        }
                                    }
                                } 

                                var externalReferencesEqualCIID = customerInfoResponses.Where(a => a.external_references.Any(b => b.reference_type == "CIID"));
                                if (externalReferencesEqualCIID.Any())
                                {
                                    var externalReferencesEqualCIIDFirstData = new CustomerInfoResponseModel();

                                    foreach (var externalReferencesEqualCIIDItem in externalReferencesEqualCIID)
                                    {
                                        var isCIIDData = externalReferencesEqualCIIDItem.external_references.Where(b => b.reference_type == "CIID");
                                        if (isCIIDData.Any())
                                        {
                                            externalReferencesEqualCIIDFirstData = externalReferencesEqualCIIDItem;
                                            continue;
                                        }

                                    }

                                    if (externalReferencesEqualCIIDFirstData != null)
                                    {
                                        myResponse.type = externalReferencesEqualCIIDFirstData.type;
                                        myResponse.first_name = externalReferencesEqualCIIDFirstData.first_name;
                                        myResponse.last_name = externalReferencesEqualCIIDFirstData.last_name;
                                        myResponse.salutation = externalReferencesEqualCIIDFirstData.salutation;

                                        #region getAddress

                                        var address = externalReferencesEqualCIIDFirstData.addresses.Where(a => a.active == true).OrderByDescending(a => a.id);
                                        if (address.Any())
                                        {
                                            myResponse.addresses = address.FirstOrDefault();

                                        }
                                        #endregion
                                        #region getLoyalty
                                        var loyalty = externalReferencesEqualCIIDFirstData.legal_agreements.Where(a => a.name == "Loyalty" && a.active == true);
                                        if (loyalty.Any())
                                        {
                                            myResponse.Loyalty = loyalty.FirstOrDefault().active;
                                        }
                                        #endregion
                                        #region getClubCard

                                        var clubCards = new List<ClubCard>();

                                        if (externalReferencesEqualCIID.FirstOrDefault() != null)
                                        {
                                            var external_references = externalReferencesEqualCIID.FirstOrDefault().external_references.Where(a => a.reference_type == "CIID" && a.deleted == false)
                                                                                      .OrderByDescending(a => a.id);
                                            if (external_references.Any())
                                            {
                                                if (!model.isAllClubCard)
                                                {
                                                    var external_reference = external_references.FirstOrDefault();

                                                    if (external_reference != null)
                                                    {
                                                        clubCards.Add(new ClubCard()
                                                        {
                                                            clubCard = external_reference.reference_value,
                                                            active = external_reference.deleted == true ? false : true,
                                                            createdDate = GetDateTimeFormat(external_reference.created)
                                                        });
                                                        myResponse.ClubCards = clubCards;
                                                    }


                                                }
                                                else
                                                {
                                                    clubCards.AddRange(external_references.Select(a => new ClubCard()
                                                    {
                                                        clubCard = a.reference_value,
                                                        active = a.deleted == true ? false : true,
                                                        createdDate = GetDateTimeFormat(a.created)
                                                    }));

                                                    myResponse.ClubCards = clubCards;

                                                }

                                            }
                                        }

                                        #endregion
                                        #region getcommunication_profile

                                        var communicationProfiles = new List<CommunicationProfile>();

                                        var communication_profiles = externalReferencesEqualCIIDFirstData.communication_profiles;
                                        if (communication_profiles.Any())
                                        {
                                            communicationProfiles.AddRange(communication_profiles.Select(a => new CommunicationProfile()
                                            {
                                                active = a.active,
                                                value = a.value,
                                                type = a.type
                                            }));

                                            myResponse.CommunicationProfiles = communicationProfiles;

                                        }
                                        #endregion

                                        return Result<CustomerInfoSummaryModel>(myResponse);
                                    }

                                }
                            }


                        }
                        return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);

                    }
                    else
                    {
                        myResponse.type = result.Data[0].type;
                        myResponse.first_name = result.Data[0].first_name;
                        myResponse.last_name = result.Data[0].last_name;
                        myResponse.salutation = result.Data[0].salutation;
                        #region getAddress

                        var address = result.Data[0].addresses.Where(a => a.active == true).OrderByDescending(a => a.id);
                        if (address.Any())
                        {
                            myResponse.addresses = address.FirstOrDefault();

                        }
                        #endregion
                        #region getLoyalty
                        var loyalty = result.Data[0].legal_agreements.Where(a => a.name == "Loyalty" && a.active == true);
                        if (loyalty.Any())
                        {
                            myResponse.Loyalty = loyalty.FirstOrDefault().active;
                        }
                        #endregion
                        #region getClubCard

                        var clubCards = new List<ClubCard>();

                        var external_references = result.Data[0].external_references.Where(a => a.reference_type == "CIID" && a.deleted == false)
                                                                  .OrderByDescending(a => a.id);
                        if (external_references.Any())
                        {
                            if (!model.isAllClubCard)
                            {
                                var external_reference = external_references.FirstOrDefault();

                                if (external_reference != null)
                                {
                                    clubCards.Add(new ClubCard()
                                    {
                                        clubCard = external_reference.reference_value,
                                        active = external_reference.deleted == true ? false : true,
                                        createdDate = GetDateTimeFormat(external_reference.created)
                                    });
                                    myResponse.ClubCards = clubCards;
                                }


                            }
                            else
                            {
                                clubCards.AddRange(external_references.Select(a => new ClubCard()
                                {
                                    clubCard = a.reference_value,
                                    active = a.deleted == true ? false : true,
                                    createdDate = GetDateTimeFormat(a.created)
                                }));

                                myResponse.ClubCards = clubCards;

                            }

                        }

                        #endregion
                        #region getcommunication_profile

                        var communicationProfiles = new List<CommunicationProfile>();

                        var communication_profiles = result.Data[0].communication_profiles;
                        if (communication_profiles.Any())
                        {
                            communicationProfiles.AddRange(communication_profiles.Select(a => new CommunicationProfile()
                            {
                                active = a.active,
                                value = a.value,
                                type = a.type
                            }));

                            myResponse.CommunicationProfiles = communicationProfiles;

                        }
                        #endregion

                        return Result<CustomerInfoSummaryModel>(myResponse);
                    }
                }
                else
                {
                    return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.NotFoundData.GetDisplayName(), (int)MMCustomerInfoMessageCodes.NotFoundData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"<MMCustomerInfoService>GetCustomerInfo:{ex.Message}</MMCustomerInfoService>");
                return Result<CustomerInfoSummaryModel>(null, MMCustomerInfoMessageCodes.UnknownError.GetDisplayName(), (int)MMCustomerInfoMessageCodes.UnknownError);
            }
        }

        #region PUBLIC_METHODS

        public DateTime GetDateTimeFormat(long value)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddMilliseconds(value).ToLocalTime();

            return date;
        }
        #endregion
    }
}
