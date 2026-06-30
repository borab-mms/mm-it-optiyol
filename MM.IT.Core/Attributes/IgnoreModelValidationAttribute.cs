using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Attributes;

/// <summary>
/// Model Validation kontrolünün olmaması gereken Controller.Action'lar için kullanılan Attribute nesnesi
/// </summary>
public class IgnoreModelValidationAttribute : Attribute
{

}