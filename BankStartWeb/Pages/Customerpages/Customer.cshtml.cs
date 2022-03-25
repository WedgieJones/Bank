using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customerpages
{
    public class CustomerModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public CustomerModel(ApplicationDbContext context)
		{
			_context = context;
		}

        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public IEnumerable<Account> Accounts { get; set; } 
        public int CustomerId { get; set; }

        public class Account
		{
			public int Id { get; set; }
			public string AccountType { get; set; }
			public int Customerid { get; set; }

		}

        public void OnGet( int customerId)
        {
			var customer = _context.Customers.First(x => x.Id == customerId);
			Givenname = customer.Givenname;
			Surname = customer.Surname;
			Streetaddress = customer.Streetaddress;
			City = customer.City;


			Accounts = (from cust in _context.Customers
                       join account in _context.Accounts
                       on cust.Id equals account.CustomerId
                       where cust.Id == customerId
                       select new Account {
                           Id = account.Id,
                           AccountType = account.AccountType,
                           Customerid = customerId
					   });
			
            //var result = _context.Accounts.Where(x => x.CustomerId == customerId).ToList();
             
        }

	}
}
