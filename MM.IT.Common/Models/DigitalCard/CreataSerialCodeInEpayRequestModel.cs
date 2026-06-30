using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.DigitalCard
{
    public class CreataSerialCodeInEpayRequestModel
    {
        public int CustomerOrderNumber { get; set; }
        public string LineItemID { get; set; }
    }
    /// <summary>
    /// Fluent Validateion Modeli
    /// </summary>
    public class CreataSerialCodeInEpayRequestModelValidator : AbstractValidator<CreataSerialCodeInEpayRequestModel>, IPmModelValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CreataSerialCodeInEpayRequestModelValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            RuleFor(p => p.CustomerOrderNumber)
             .NotNull()
             .NotEmpty()
             .NotNull();

            RuleFor(p => p.LineItemID)
             .NotNull()
             .NotEmpty()
             .MaximumLength(100);
        }
    }
}
