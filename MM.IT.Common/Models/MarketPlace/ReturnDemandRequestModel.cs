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
    public class ReturnDemandRequestModel
    {
        public int article { get; set; }
        public string channelCode { get; set; }
        public string erpId{ get; set; }
        public decimal amount { get; set; }
        public string returnReason { get; set; }
        public DateTime? returnDate { get; set; }
        public string returnId { get; set; }
        public string returnDetailId { get; set; }
        public string shippingCompany { get; set; }
        public string cargoCode { get; set; }
    }

    /// <summary>
    /// Fluent Validateion Modeli
    /// </summary>
    public class ReturnDemandRequestValidator : AbstractValidator<ReturnDemandRequestModel>, IPmModelValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReturnDemandRequestValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            RuleFor(p => p.article)
                .NotNull()
                .GreaterThanOrEqualTo(1000000)
                .LessThanOrEqualTo(9999999);

            RuleFor(p => p.channelCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(p => p.erpId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.amount)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.returnReason)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(p => p.returnDate)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.returnId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.returnDetailId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.shippingCompany)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.cargoCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

        }
    }
}
