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

    public class TransferModel : PageModel
	{
		private readonly ITransactionServices _services;
		private readonly ApplicationDbContext _context;

		public TransferModel(ITransactionServices services, ApplicationDbContext context)
		{

			_services = services;
			_context = context;
		}

		public string Name { get; set; }
		[BindProperty]
		public int FromAccountId { get; set; }
		[BindProperty]
		public int ToAccountId { get; set; }
		[BindProperty]
		public decimal Amount { get; set; }
		
		[BindProperty]
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
			int fromaccountId = FromAccountId;
			int toaccountId = ToAccountId;
			decimal amount = Amount;

			if (ModelState.IsValid)
			{
				var error = _services.Transfer(fromaccountId,toaccountId, amount);
				if (error == ITransactionServices.ErrorCode.BalanceTooLow)
				{
					ModelState.AddModelError("amount", "Beloppet är för stort! Det finns inte tillräckligt med pengar på kontot!");
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
