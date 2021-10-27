using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ShopApp.Enums;
using ShopApp.ViewModels;

namespace ShopApp.Services
{
    public class ErrorServis
    {
        public async Task<ErrorViewModel> HandleErrorAsync(int statusCode)
        {
            return await Task.Run(() => HandleError(statusCode));
        }
        private ErrorViewModel HandleError(int statusCode)
        {
            ErrorStatusCodes value = (ErrorStatusCodes)statusCode;

            ErrorViewModel model = new();
            model.StatusCode = statusCode;
            model.StatusCodeDescription = value.GetDescription();

            if(statusCode == 404)
            {
                model.ErrorMessage = "We could not find the page you were looking for.";
            }
            else if(statusCode >= 500)
            {
                model.ErrorMessage = "We will work on fixing that right away.";
            }
            else
            {
                model.ErrorMessage = "An error occurred while processing your request.";
            }

            return model;
        }
    }
    public static class EnumExtensions
    {
            public static string GetDescription(this Enum enumValue)
            {
                return enumValue.GetType()
                           .GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DescriptionAttribute>()
                           .Description;
            }
    }

}
