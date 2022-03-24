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
        public List<Account> Accounts { get; set; } = new List<Account>();
        public int customerId { get; set; }

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
            
            Accounts = _context.Accounts.Select(Id from )
        }
       
    }
}
