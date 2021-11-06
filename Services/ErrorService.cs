using System.Threading.Tasks;
using ShopApp.Enums;
using ShopApp.ViewModels;
using ShopApp.Extensions;
using Microsoft.Extensions.Logging;

namespace ShopApp.Services
{
    public class ErrorService
    {
        private readonly ILogger _logger;
        public ErrorService(ILogger<ErrorService> logger)
        {
            _logger = logger;
        }
        public async Task<ErrorViewModel> HandleErrorAsync(int statusCode)
        {
            return await Task.Run(() => HandleError(statusCode));
        }
        private ErrorViewModel HandleError(int statusCode)
        {

            ErrorStatusCodes value = (ErrorStatusCodes)statusCode;

            ErrorViewModel model = new();
            model.StatusCode = statusCode;
            model.StatusCodeDescription = value.GetDescription() ?? "Something went wrong";
            if(model.StatusCodeDescription == "Something went wrong")
            {
                _logger.LogError("Error status code not found: {statusCode}", statusCode);
            }
            if (statusCode == 404)
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
}
