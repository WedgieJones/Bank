using BankStartWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Transactions
{
	public class TransactionServices : ITransactionServices
	{
		private readonly ApplicationDbContext _context;

		public TransactionServices(ApplicationDbContext context)
		{
			_context = context;
		}

		public ITransactionServices.ErrorCode Deposit(int accountId, string operation, decimal amount)
		{
			if (amount < 0)
			{
				return ITransactionServices.ErrorCode.AmountIsNegativ;
			}

			var account = _context.Accounts.First(a => a.Id == accountId);
			account.Balance += amount;

			var trans = new Transaction();
			{
				trans.Amount = amount;
				trans.Type = "Debit";
				trans.Operation = operation;
				trans.Date = DateTime.Now;
				trans.NewBalance = account.Balance;
			}
			account.Transactions.Add(trans);
			_context.SaveChanges();
			return ITransactionServices.ErrorCode.Ok;
		}

		public ITransactionServices.ErrorCode Withdraw(int accountId, string operation, decimal amount)
		{

			if (amount < 0)
			{
				return ITransactionServices.ErrorCode.AmountIsNegativ;
			}
			var account = _context.Accounts.First(a => a.Id == accountId);
			if(account.Balance < amount)
			{
				return ITransactionServices.ErrorCode.BalanceTooLow;
			}

			account.Balance -= amount;
			var transaction = new Transaction();
			{
				transaction.Amount = amount;
				transaction.Type = "Credit";
				transaction.Operation = operation;
				transaction.Date = DateTime.Now;
				transaction.NewBalance = account.Balance;
			}
			account.Transactions.Add(transaction);
			_context.SaveChanges();
			return ITransactionServices.ErrorCode.Ok;
		}


	}
}
