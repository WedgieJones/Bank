using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customerpages
{
	[Authorize(Roles = "Cashier")]
    public class DepositModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
