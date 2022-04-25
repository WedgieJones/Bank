using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customerpages
{
    public class TransferModel : PageModel
    {
	    [Authorize(Roles = "Cashier")]

        public void OnGet()
        {
        }
    }
}
