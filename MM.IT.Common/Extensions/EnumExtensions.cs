using MM.IT.Common.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        /// <summary>
        /// LocalizedName attribute bulunan enum bilgisinin resource'daki değerini döndürür.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetLocalizedName(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description.ToString();
            else
                return enumValue.ToString();
        }

        /// <summary>
        /// LocalizedName attribute bulunan enum bilgisinin resource'daki değerini liste şeklinde döndürür.
        /// </summary>
        /// <typeparam name="TEnum">Enum Tipi</typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValueModel<string, TEnum>> GetLocalizedList<TEnum>() where TEnum : Enum
        {
            var model = new List<KeyValueModel<string, TEnum>>();
            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
            foreach (var enumValue in enumValues)
            {
                model.Add(new KeyValueModel<string, TEnum>
                {
                    Key = enumValue.GetLocalizedName(),
                    Value = enumValue
                });
            }

            return model;
        }
    }
}
