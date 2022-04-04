using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
		private readonly ApplicationDbContext _context;

		public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
			_context = context;
		}

		public int NumOfAccounts { get; set; }
        public int NumOfCustomers { get; set; }
		public void OnGet()
        {

            NumOfAccounts = _context.Accounts.Count();
            NumOfCustomers = _context.Customers.Count();

        }
    }
}