namespace BankStartWeb.Transactions
{
	public interface ITransactionServices
	{
		public enum ErrorCode
		{
			Ok,
			BalanceTooLow,
			AmountIsNegative,
			
		}
		ErrorCode Deposit(int accountId, string operation, decimal amount);
		ErrorCode Withdraw(int accountId, string operation, decimal amount);
		ErrorCode Transfer(int fromAccountId, int toAccount, decimal amount);
	}

}