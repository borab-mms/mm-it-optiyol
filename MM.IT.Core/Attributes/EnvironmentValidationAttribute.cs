using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Attributes;

/// <summary>
/// Environment kontrolünün olması gereken Controller.Action'lar için kullanılan Attribute nesnesi
/// </summary>
public class EnvironmentValidationAttribute : Attribute
{
    public string Include { get; set; }

    public string Exclude { get; set; }

    public EnvironmentValidationAttribute(string include = "", string exclude = "")
    {
        this.Include = include;
        this.Exclude = exclude;
    }
}