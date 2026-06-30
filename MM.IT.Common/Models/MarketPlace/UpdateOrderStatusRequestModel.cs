using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class UpdateOrderStatusRequestModel
    {
        public int Article { get; set; }
        public string ChannelCode { get; set; }
        public string ErpId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
        public string StatusType { get; set; }
        [JsonIgnore]
        public string CancelledStatus { get; set; }
        [JsonIgnore]
        public int CancelledQuantity { get; set; }
        [JsonIgnore]
        public DateTime CancelledDate { get; set; }
        [JsonIgnore]
        public int? CustomerOrderNumber { get; set; }
        [JsonIgnore]
        public string? OrderPackageNumber { get; set; }
        [JsonIgnore]
        public int? WhId { get; set; }
        [JsonIgnore]
        public DateTime? firstFomCheck { get; set; }
    }

    /// <summary>
    /// Fluent Validateion Modeli
    /// </summary>
    public class UpdateOrderStatusRequestModelValidator : AbstractValidator<UpdateOrderStatusRequestModel>, IPmModelValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UpdateOrderStatusRequestModelValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            RuleFor(p => p.Article)
                .NotNull()
                .GreaterThanOrEqualTo(1000000)
                .LessThanOrEqualTo(9999999);

            RuleFor(p => p.ChannelCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(p => p.ErpId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Quantity)
                .NotNull()
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.Status)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.StatusDate)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.StatusType)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

        }
    }
}
