using BankStartWeb.Data;

namespace BankStartWeb.Transactions
{
	public class TransactionServices
	{
		private readonly ApplicationDbContext _context;

		public TransactionServices(ApplicationDbContext context)
		{
			_context = context;
		}

		public decimal Deposit(int accountId, decimal depositAmount)
		{
			var enterDeposit = _context.Accounts.First(a=>a.Id == accountId);
				enterDeposit.Balance = enterDeposit.Balance + depositAmount;
			_context.SaveChanges();
			return enterDeposit.Balance;
		}

		public decimal Withdrawel(int accountId, decimal withdrawelAmount)
		{
			var enterWithdrawel = _context.Accounts.First(a=>a.Id == accountId);
			enterWithdrawel.Balance = enterWithdrawel.Balance - withdrawelAmount;
			_context.SaveChanges();
			return enterWithdrawel.Balance;
		}

	}
}
