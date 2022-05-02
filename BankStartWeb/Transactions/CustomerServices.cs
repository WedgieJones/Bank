﻿using BankStartWeb.Data;

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
		public ICustomerServices.ErrorCode AddCustomer(string givenname, string surname,
			string streetaddress, string city, string zipcode,
			string country, string countryCode, string nationalId,
			int telephoneCountryCode, string telephone, string emailAddress, DateTime birthday)
		{
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

			return ICustomerServices.ErrorCode.Ok; ;
		}

	}
}
