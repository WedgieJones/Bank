using BankStartWeb.Data;
using BankStartWeb.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customerpages
{
	[Authorize]

	public class WithdrawalModel : PageModel
   	 {
		private readonly ITransactionServices _services;
		private readonly ApplicationDbContext _context;

		public WithdrawalModel(ITransactionServices services, ApplicationDbContext context)
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
		[BindProperty]
		public int CustomerId { get; set; }
		public List<Account> Accounts { get; set; }
		public string Errormsg { get; set; }
		public List<SelectListItem> AllAccounts { get; set; }

		public void OnGet(int customerId)
		{
			CustomerId = customerId;
			SetAllAccounts();
			var customer = _context.Customers
				.First(cust => cust.Id == customerId);
			Name = customer.Givenname + " " + customer.Surname;

		}
		public void SetAllAccounts()
		{
			var customer = _context.Customers
				.Include(acc => acc.Accounts)
				.First(cust => cust.Id == CustomerId);

			AllAccounts = customer.Accounts.Select(account => new SelectListItem
			{
				Text = account.AccountType + " Balans: " + account.Balance + " kr",
				Value = account.Id.ToString()
			}).ToList();
		}

		public IActionResult OnPost()
		{
			
			int customerId = CustomerId;
			int accountId = AccountId;
			decimal amount = Amount;
			string operation = Operation;

			if (ModelState.IsValid)
			{
				var error = _services.Withdraw(accountId, operation, amount);
				if (error == ITransactionServices.ErrorCode.BalanceTooLow)
				{
					ModelState.AddModelError("amount", "Beloppet �r f�r stort! Det finns inte tillr�ckligt med pengar p� kontot!");
					//Errormsg = "Beloppet �r f�r stort! Det finns inte tillr�ckligt med pengar p� kontot!";
					SetAllAccounts();
					return Page();
				}
					return RedirectToPage("Customer", new { customerId });

			}
			
			SetAllAccounts();
			return Page();
		}

	}



}

