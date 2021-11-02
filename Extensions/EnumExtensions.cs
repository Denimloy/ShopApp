using Microsoft.Extensions.Logging;
using ShopApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ShopApp.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            try
            {
                return value.GetType()
                    .GetMember(value.ToString())
                    .First()
                    .GetCustomAttribute<DescriptionAttribute>()
                    .Description;
            }
            catch
            {
                return null;
            }
        }

    }
}
