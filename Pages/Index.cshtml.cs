using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netopia.Logic;
using static System.Net.WebRequestMethods;

namespace Netopia.Pages
{
    public class IndexModel : PageModel
    {
        public string PostUrl { get; set; } = "https://sandboxsecure.mobilpay.ro";
        public string EnvKey { get; set; }
        public string Data { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IPaymentProcessor _paymentProcessor;

        public IndexModel(ILogger<IndexModel> logger,
            IPaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _paymentProcessor = paymentProcessor;
        }

        public void OnGet()
        {
            _logger.LogInformation("GET Index was called");

            var encrypt = _paymentProcessor.CreatePaymentForNetopia();
            EnvKey = encrypt.EnvelopeKey;
            Data = encrypt.EncryptedData;
        }
    }
}