using BankStartWeb.Data;

namespace BankStartWeb.Transactions
{
	public class CustomerServices : ICustomerServices
	{
		private ApplicationDbContext _context;

		public CustomerServices(ApplicationDbContext context)
		{
			_context = context;
		}

		public ICustomerServices.ErrorCode AddAccount(int customerId, string accountType)
		{

			var customer = _context.Customers.First(customer => customer.Id == customerId);

			var account = new Account()
			{
				AccountType = accountType,
				Created = DateTime.Now,
				Balance = 0
			};
			customer.Accounts.Add(account);
			_context.SaveChanges();

			return ICustomerServices.ErrorCode.Ok;
		}
		public (ICustomerServices.ErrorCode, int) AddCustomer(string givenname, string surname,
			string streetaddress, string city, string zipcode,
			string country, string countryCode, string nationalId,
			int telephoneCountryCode, string telephone, string emailAddress, DateTime birthday)
		{
		
			if (_context.Customers.Any(customer => customer.NationalId == nationalId))
			{
				return (ICustomerServices.ErrorCode.CustomerAlreadyExists, 0);
			}
			
			var customer = new Customer()
			{
				Givenname = givenname,
				Surname = surname,
				Streetaddress = streetaddress,
				City = city,
				Zipcode = zipcode,
				Country = country,
				CountryCode = countryCode,
				NationalId = nationalId,
				Telephone = telephone,
				EmailAddress = emailAddress,
				Birthday = birthday,
				TelephoneCountryCode = telephoneCountryCode,
			};
			_context.Customers.Add(customer);
			_context.SaveChanges();
			int customerid = customer.Id;
			AddAccount(customerid, "Personal");

			return (ICustomerServices.ErrorCode.Ok, customerid); ;
		}

	}
}
