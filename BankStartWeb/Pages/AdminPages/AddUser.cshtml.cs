using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.AdminPages
{
    [Authorize(Roles="Admin")]
    public class AddUserModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
