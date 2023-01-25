using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Netopia.Pages
{
    public class ConfirmationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ConfirmationModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(IFormCollection formData)
        {
            _logger.LogInformation("GET Confirmation was called");
        }

        public void OnPostAsync(IFormCollection formData)
        {
            _logger.LogInformation("POST Confirmation was called");
            _logger.LogInformation("Form data {0}", formData.ToList().ToString());
        }
    }
}
