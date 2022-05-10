using BankStartWeb.Data;
using BankStartWeb.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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
        [BindProperty]
        [Required]
        [MaxLength(50)]
        public string Givenname { get; set; }
        [Required] [BindProperty] [MaxLength(50)] public string Surname { get; set; }
        [Required] [BindProperty] [MaxLength(50)] public string Streetaddress { get; set; }
        [Required] [BindProperty] [MaxLength(50)] public string City { get; set; }
        [Required] [BindProperty] [MaxLength(10)] public string Zipcode { get; set; }
        [Required] [BindProperty] [MaxLength(30)] public string Country { get; set; }
        [Required] [BindProperty] [MaxLength(20)] public string NationalId { get; set; }
        [Required] [BindProperty] public string Telephone { get; set; }
        [Required] [BindProperty] [MaxLength(50)] public string EmailAddress { get; set; }
        [Required]
        
        [BindProperty, DataType(DataType.Date)] 
        public DateTime Birthday { get; set; } = DateTime.Now;
       
        public List<SelectListItem> AllCountries { get; set; }
        
        public void OnGet()
        {
            SetAllCountries();
        }
        public void SetAllCountries()
		{
            AllCountries = _context.Customers.Select(customer => customer.Country).Distinct().Select(country => new SelectListItem
			{
                Text = country,
                Value = country
			}).ToList();
        }
        public IActionResult OnPost()
		{
            
            if (ModelState.IsValid)
            {
                string givenname = Givenname;
                string surname = Surname;
                string streetaddress = Streetaddress;
                string city = City;
                string zipcode = Zipcode;
                string country = Country;
                string countryCode;
                string nationalId = NationalId;
                int telephoneCountryCode;
                string telephone = Telephone;
                string emailAddress = EmailAddress;
                DateTime birthday = Birthday;


                if (country == "Norge")
                {
                    countryCode = "NO";
                    telephoneCountryCode = 47;
                }
                else if (country == "Sverige")
                {
                    countryCode = "SE";
                    telephoneCountryCode = 46;
                }
                else
                {
                    countryCode = "FI";
                    telephoneCountryCode = 48;
                }

               var (ErrorCode, customerId) = _services.AddCustomer(givenname, surname, streetaddress, city, zipcode, country
                    , countryCode, nationalId, telephoneCountryCode, telephone, emailAddress, birthday);

                return RedirectToPage("Customer", new { customerId });
			}

            
            SetAllCountries();
            return Page();
		}
    }
}
