using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class OrderCancellationRequestModel
    {
        public int Article { get; set; }
        public string ChannelCode { get; set; }
        public string ErpId { get; set; }
        public int Quantity { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? OrderPackageNumber { get; set; }
        public DateTime? FomOrderDate { get; set; }
        public string? CustomerOrderNumber { get; set; }
        public string? WhId { get; set; }
        public string? Sender { get; set; }
    }
    public class OrderCancellationRequestModelValidator : AbstractValidator<OrderCancellationRequestModel>, IPmModelValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderCancellationRequestModelValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            RuleFor(p => p.Article)
                .NotNull()
                .GreaterThanOrEqualTo(1000000)
                .LessThanOrEqualTo(9999999);

            RuleFor(p => p.ErpId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(p => p.Quantity)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(99);

            RuleFor(p => p.ChannelCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

        }
    }
}
