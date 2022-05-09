using BankStartWeb.Data;
using BankStartWeb.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customerpages
{
    public class EditCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerServices _services;

        public EditCustomerModel(ApplicationDbContext context, ICustomerServices services)
        {
            _context = context;
            _services = services;
        }

        [BindProperty]
        [MaxLength(50)]
        public string Givenname { get; set; }
        [BindProperty] [MaxLength(50)] public string Surname { get; set; }
        [BindProperty] [MaxLength(50)] public string Streetaddress { get; set; }
        [BindProperty] [MaxLength(50)] public string City { get; set; }
        [BindProperty] [MaxLength(10)] public string Zipcode { get; set; }
        [BindProperty] [MaxLength(30)] public string Country { get; set; }
        public string NationalId { get; }
		[BindProperty] public string Telephone { get; set; }
        [BindProperty] [MaxLength(50)] public string EmailAddress { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [BindProperty] public int TelephoneCountryCode { get; set; }
        public List<SelectListItem> AllCountries { get; set; }
        public int CustomerId { get; set; }
        public void OnGet(int customerId)
        {
            var customer = _context.Customers.First(cust => cust.Id == customerId);
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Zipcode = customer.Zipcode;
            Country = customer.Country;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            CustomerId = customerId;
            Birthday = customer.Birthday;
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

        public IActionResult OnPost(int customerId)
        {
            if (ModelState.IsValid)
            {
                var person = _context.Customers.First(cust => cust.Id == customerId);
                int customerID = customerId;
                string givenname = Givenname;
                string surname = Surname;
                string streetaddress = Streetaddress;
                string city = City;
                string zipcode = Zipcode;
                string country = Country;
                string countryCode;
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

                var (ErrorCode, customerI) = _services.EditCustomer(givenname, surname, streetaddress, city, zipcode, customerID, country
                     , countryCode, telephoneCountryCode, telephone, emailAddress);

                return RedirectToPage("Customer", new { customerId });
            }
            SetAllCountries();
            return Page();

        }
    }
}
