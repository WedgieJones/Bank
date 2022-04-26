using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankStartWeb.Transactions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customerpages
{
	[Authorize(Roles = "Cashier")]
    public class DepositModel : PageModel
    {
		private readonly ITransactionServices _services;
		private readonly ApplicationDbContext _context;

		public DepositModel(ITransactionServices services, ApplicationDbContext context)
	    {
			
			_services = services;
			_context = context;
	    }

        public string Name { get; set; }
		[BindProperty]
		public int AccountId { get; set; }
		[BindProperty]
		public decimal Amount { get; set; }
		[BindProperty]
		public string Operation { get; set; }

		public int CustomerId { get; set; }
		public List<Account> Accounts { get; set; }

		public List<SelectListItem> AllAccounts { get; set; }

		public void OnGet(int customerId)
        {
			CustomerId = customerId;
			SetAllAccounts();
			var customer = _context.Customers
		        .First(cust => cust.Id == customerId);
	        Name = customer.Givenname + " " + customer.Surname;
			
			
			//var account = _context.Accounts.First(a=>a.Id == accountId);
			//Balance = account.Balance;
			//AccountType = account.AccountType;

		}
       public void SetAllAccounts()
       {
	       var customer = _context.Customers
		       .Include(acc=>acc.Accounts)
		       .First(cust => cust.Id == CustomerId);

			AllAccounts = customer.Accounts.Select(account => new SelectListItem
	        {
		        Text = account.AccountType +" Balans: "+account.Balance +" kr",
				Value = account.Id.ToString()
	        }).ToList();
        }

        public IActionResult OnPost(int customerId)
        {
	        
			if (ModelState.IsValid)
			{
				_services.Deposit(AccountId, Operation, Amount);
		        return RedirectToPage("Customer");
	        }
			CustomerId = customerId;
			SetAllAccounts();
			return Page();
        }

	}
}
