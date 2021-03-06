using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace BankStartWeb.Pages.Customerpages
{
    [Authorize]
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
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }
        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public decimal TotalBalance { get; set; }

        public class AccountViewModel
		{
            public int Id { get; set; }

            public string AccountType { get; set; }

            public DateTime Created { get; set; }
            public decimal Balance { get; set; }

            public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        }

        public IActionResult OnGet(int customerId)
        {
            Id = customerId;

			var customer = _context.Customers
                .Include(a => a.Accounts)
                .FirstOrDefault(x => x.Id == customerId);
            if (customer == null) return RedirectToPage("/Index");
			Givenname = customer.Givenname;
			Surname = customer.Surname;
			Streetaddress = customer.Streetaddress;
			City = customer.City;
            Accounts = customer.Accounts;  
            Zipcode = customer.Zipcode;
            Country = customer.Country;
            CountryCode = customer.CountryCode;
            NationalId = customer.NationalId;
            TelephoneCountryCode = customer.TelephoneCountryCode;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            Birthday = customer.Birthday;

            foreach (var account in Accounts)
			{
                TotalBalance = TotalBalance + account.Balance;
			}

            return Page();
        }

        

    }
}
