using BankStartWeb.Data;
using BankStartWeb.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankStartWeb.Pages.Customerpages
{
    public class AddAccountModel : PageModel
    {
		private ICustomerServices _services;
		private ApplicationDbContext _context;

		public AddAccountModel(ApplicationDbContext context, ICustomerServices services)
		{
            _services = services;
            _context = context;
        }
        [BindProperty]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        [BindProperty] 
        public string AccountType { get; set; }
        public List<SelectListItem> AllAccountTypes { get; set; }

        public void OnGet(int customerId)
        {
            CustomerId = customerId;
            var customer = _context.Customers.First(customer=>customer.Id == customerId);
            Name = customer.Givenname + " " + customer.Surname;
            SetAllAccountTypes();
        }

        public void SetAllAccountTypes()
		{
            
            AllAccountTypes = _context.Accounts.Select(a=>a.AccountType).Distinct().Select(accountType => new SelectListItem
			{
                Text = accountType,
                Value = accountType
			}).ToList();
		}
       
        public IActionResult OnPost()
		{
            int customerId = CustomerId;
            string accountType = AccountType;
			if (ModelState.IsValid)
			{
                _services.AddAccount(customerId, accountType);
                return RedirectToPage("Customer", new { customerId });
            }

            SetAllAccountTypes();
            return Page();
		}
        
    }
}
