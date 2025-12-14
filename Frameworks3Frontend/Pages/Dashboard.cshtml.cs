using Frameworks3Frontend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frameworks3Frontend.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ApiService _apiService;

        public dynamic? IssData { get; set; }

        public DashboardModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            var issData = await _apiService.GetStringAsync("/SpaceCache/iss/last");
            if (!string.IsNullOrEmpty(issData))
            {
                IssData = System.Text.Json.JsonSerializer.Deserialize<dynamic>(issData);
            }
        }
    }
}
