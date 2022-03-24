using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customerpages
{
    public class CustomersModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public List<Customer> Customers { get; set; }

		public CustomersModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public class Customer
		{
			public string Givenname { get; set; }
			public string Surname { get; set; }
			public string NationalId { get; set; }
			public int Id { get; set; } 

		}
		public void OnGet()
		{
			Customers = _context.Customers.Take(30).Select(s =>
			new Customer
			{
				Givenname = s.Givenname,
				Surname = s.Surname,
				NationalId = s.NationalId,
				Id = s.Id
			}).ToList();
		}
	}
}
