namespace BankStartWeb.Transactions
{
	public interface ICustomerServices
	{
		public enum ErrorCode
		{
			Ok,
			FaultyInformation,
			
		}
		ErrorCode AddAccount(int customerId, string accountType);
		ErrorCode AddCustomer(string givenname, string surname,
            string streetaddress, string city, string zipcode, 
            string country, string countryCode, string nationalId, 
            int telephoneCountryCode, string telephone, string emailAddress, DateTime birthday);
     
    }
}