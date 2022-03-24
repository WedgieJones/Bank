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

        public void OnGet( int customerId)
        {
            var customer = _context.Customers.First(x => x.Id == customerId);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
			
        }
    }
}
