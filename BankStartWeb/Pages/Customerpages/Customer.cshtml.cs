using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }
        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();



        public void OnGet( int customerId)
        {
			var customer = _context.Customers
                .Include(a => a.Accounts)
                .First(x => x.Id == customerId);
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




			//Accounts = (from cust in _context.Customers
   //                    join account in _context.Accounts
   //                    on cust.Id equals account.CustomerId
   //                    where cust.Id == customerId
   //                    select new Account {
   //                        Id = account.Id,
   //                        AccountType = account.AccountType,
   //                        Customerid = customerId
			//		   });
			
            //var result = _context.Accounts.Where(x => x.CustomerId == customerId).ToList();
             
        }

	}
}
