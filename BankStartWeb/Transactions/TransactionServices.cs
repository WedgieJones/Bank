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
				return ITransactionServices.ErrorCode.AmountIsNegative;
			}

			var account = _context.Accounts.First(a => a.Id == accountId);
			account.Balance += amount;

			var trans = new Transaction()
			{
				Amount = amount,
				Type = "Debit",
				Operation = operation,
				Date = DateTime.Now,
				NewBalance = account.Balance,
			};

			account.Transactions.Add(trans);
			_context.SaveChanges();
			return ITransactionServices.ErrorCode.Ok;
		}

		public ITransactionServices.ErrorCode Withdraw(int accountId, string operation, decimal amount)
		{

			if (amount < 0)
			{
				return ITransactionServices.ErrorCode.AmountIsNegative;
			}
			var account = _context.Accounts.First(a => a.Id == accountId);
			if(account.Balance < amount)
			{
				return ITransactionServices.ErrorCode.BalanceTooLow;
			}

			account.Balance -= amount;
			var transaction = new Transaction()
			{
				Amount = amount,
				Type = "Credit",
				Operation = operation,
				Date = DateTime.Now,
				NewBalance = account.Balance,
			};
			account.Transactions.Add(transaction);
			_context.SaveChanges();
			return ITransactionServices.ErrorCode.Ok;
		}

		public ITransactionServices.ErrorCode Transfer(int fromAccountId, int toAccount, decimal amount)
		{
			if (amount < 0)
			{
				return ITransactionServices.ErrorCode.AmountIsNegative;
			}
			var account = _context.Accounts.First(a => a.Id == fromAccountId);
			if (account.Balance < amount)
			{
				return ITransactionServices.ErrorCode.BalanceTooLow;
			}

			account.Balance -= amount;
			var transaction = new Transaction()
			{
				Amount = amount,
				Type = "Credit",
				Operation = "Overförning till kto:" + toAccount,
				Date = DateTime.Now,
				NewBalance = account.Balance,
			};
			account.Transactions.Add(transaction);

			var debitAccount = _context.Accounts.First(acc => acc.Id == toAccount);
			debitAccount.Balance += amount;
			var debitTransaction = new Transaction()
			{
				Amount = amount,
				Type = "Debit",
				Operation = "Overförning från kto: "+ fromAccountId,
				Date = DateTime.Now,
				NewBalance = debitAccount.Balance,
			};
			debitAccount.Transactions.Add(debitTransaction);
			_context.SaveChanges();

			return ITransactionServices.ErrorCode.Ok;
		}

		
	}
}
