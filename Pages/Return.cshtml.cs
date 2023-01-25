using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Netopia.Pages
{
    public class ReturnModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ReturnModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(IFormCollection formData)
        {
            _logger.LogInformation("GET Return was called");
        }

        public void OnPostAsync(IFormCollection formData)
        {
            _logger.LogInformation("POST Return was called");
            _logger.LogInformation("Form data {0}", formData.ToList().ToString());
        }
    }
}
