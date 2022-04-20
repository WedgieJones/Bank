using BankStartWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Transactions
{
	public class TransactionServices
	{
		private readonly ApplicationDbContext _context;

		public TransactionServices(ApplicationDbContext context)
		{
			_context = context;
		}

		public decimal Deposit(int accountId, string type, string operation, decimal depositAmount)
		{
			//AcountId
			//Type of transaction
			//Amount
			//Operation
			//Date (DateTime ) Datetime.now?

			var enterDeposit = _context.Accounts.First(a => a.Id == accountId);
			if (depositAmount > 0)
			{
				var trans = new Transaction();

				trans.Amount = depositAmount;
				trans.Type = type;
				trans.Operation = operation;
				trans.Date = DateTime.Now;
				//trans. 
				//_context.Transactions.Add(trans);
				//_context.SaveChanges();
				return enterDeposit.Balance;
			}
			else
			{
				//Returnera en false bool?
			}
			return enterDeposit.Balance;
		}

		public decimal Withdrawal(int accountId, decimal withdrawalAmount)
		{
			var enterWithdrawal = _context.Accounts.First(a=>a.Id == accountId);
			enterWithdrawal.Balance = enterWithdrawal.Balance - withdrawalAmount;
			_context.SaveChanges();
			return enterWithdrawal.Balance;
		}

	}
}
