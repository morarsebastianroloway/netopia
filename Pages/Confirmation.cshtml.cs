using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netopia.Logic;

namespace Netopia.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class ConfirmationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPaymentProcessor _paymentProcessor;

        public ConfirmationModel(ILogger<IndexModel> logger, IPaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _paymentProcessor = paymentProcessor;
        }

        public void OnGet(IFormCollection formData)
        {
            _logger.LogInformation("GET Confirmation was called");
        }

        public async Task OnPostAsync(IFormCollection formData)
        {
            _logger.LogInformation("POST Confirmation was called");
            _logger.LogInformation("Data: {0}", formData["data"]);
            _logger.LogInformation("Env_key: {0}", formData["env_key"]);

            // We are receiving the data from Netopia by this server-to-server call. We should encrypt and parse the
            // message and update the status in our database, so when the user is redirected to ReturnUrl we know to
            // show him the success or failure message (or pending, if this ConfirmUrl was not called).

            // Return a message to Netopia to let it know we received the status change. If we don't do this, Netopia
            // will send a new message, identical with the current one in 15-25 seconds
            string message;

            var result = _paymentProcessor.ConfirmPayment(formData["data"], formData["env_key"]);

            Response.ContentType = "text/xml";
            message = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            if (result.ErrorCode == "0")
            {
                message = message + "<crc>" + result.ErrorMessage + "</crc>";
            }
            else
            {
                message = message + "<crc error_type=\"" + result.ErrorType + "\" error_code=\"" + result.ErrorCode + "\"> " + result.ErrorMessage + "</crc>";
            }

            _logger.LogDebug($"Confirmation return: {message}");

            await Response.WriteAsync(message);
        }
    }
}
