using BankStartWeb.Data;
using BankStartWeb.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankStartWeb.Pages.Customerpages
{
    public class AddCustomerModel : PageModel
    {
		private ICustomerServices _services;
		private ApplicationDbContext _context;

		public AddCustomerModel(ApplicationDbContext context, ICustomerServices services)
		{
            _services = services;
            _context = context;
		}
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
       
        public List<SelectListItem> AllCountries { get; set; }
        
        public void OnGet()
        {
        }
        public void SetAllCountries()
		{
            AllCountries = _context.Customer.Select(customer => customer.Coun);

            //AllAccountTypes = _context.Accounts.Select(a => a.AccountType).Distinct().Select(accountType => new SelectListItem
            //{
            //    Text = accountType,
            //    Value = accountType
            //}).ToList();
        }
        public IActionResult OnPost()
		{

            return null;
		}
    }
}
