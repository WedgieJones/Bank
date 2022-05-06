namespace BankStartWeb.Transactions
{
	public interface ICustomerServices
	{
		public enum ErrorCode
		{
			Ok,
			CustomerDoesNotExist,
			CustomerAlreadyExists
			
		}
		ErrorCode AddAccount(int customerId, string accountType);
		(ErrorCode,int) AddCustomer(string givenname, string surname,
            string streetaddress, string city, string zipcode, 
            string country, string countryCode, string nationalId, 
            int telephoneCountryCode, string telephone, string emailAddress, DateTime birthday);
		(ErrorCode, int) EditCustomer(string givenname, string surname,
			string streetaddress, string city, string zipcode, int customerId,
			string country, string countryCode, int telephoneCountryCode, string telephone, string emailAddress);

	}
}