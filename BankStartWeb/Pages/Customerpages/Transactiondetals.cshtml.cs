using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankStartWeb.Transactions;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace BankStartWeb.Pages.Customerpages
{
    public class TransactiondetalsModel : PageModel
    {
		private readonly ApplicationDbContext _context;
		private readonly TransactionServices _transactionservices;

		public TransactiondetalsModel(ApplicationDbContext context, TransactionServices transactions)
		{
			_context = context;
			_transactionservices = transactions;
		}
        public int Id { get; set; }
        public string AccountType { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
        [BindProperty]
        //[Range(0,Max , ErrorMessage="Belopp måste vara högre än 1" )]
        public decimal DepositAmount { get; set; }  
        public decimal WithdrawalAmount { get; set; }
		public string FullName { get; set; }
		public string Type { get; set; }
		public string Operation { get; set; }
		public List<TransactionViewModel> Transactions { get; set; }

        public class TransactionViewModel
		{
            public int Id { get; set; }
			public string TransactionType { get; set; }
            public string Operation { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
		}

        public void OnGet(int accountId)
        {

        var acc = _context.Customers
                .Include(c=>c.Accounts)
                .ThenInclude(a => a.Transactions.OrderByDescending(t=>t.Date))
                .First(c=>c.Accounts.Any( x => x.Id == accountId));

            var a = acc.Accounts.First(account => account.Id == accountId);
            var t = a.Transactions.OrderByDescending(d=>d.Date);

            CustomerId = acc.Id;
            FullName = acc.Givenname + " " + acc.Surname;
            Balance = a.Balance;
            AccountType = a.AccountType;
            accountId = a.Id;

            Transactions = a.Transactions.Select(t => new TransactionViewModel
            {
                Id = t.Id,
                NewBalance = t.NewBalance,
                Date = t.Date,
                TransactionType = t.Type,
                Operation = t.Operation,
                Amount = t.Amount
            }).ToList();
        }

        public void OnPost(int accountId, string type, string operation, decimal depositamount)
		{

            var deposit = _transactionservices.Deposit(accountId, operation, depositamount);
            //var withDrawal = _transactions.Withdrawal(accountId, depositamount);

            

            var cust = _context.Customers
                    .Include(c => c.Accounts)
                    .ThenInclude(a => a.Transactions.OrderByDescending(t => t.Date))
                    .First(c => c.Accounts.Any(x => x.Id == accountId));

            var a = cust.Accounts.First(account => account.Id == accountId);
            var t = a.Transactions.OrderByDescending(d => d.Date);

            CustomerId = cust.Id;
            FullName = cust.Givenname + " " + cust.Surname;
            Balance = a.Balance;
            AccountType = a.AccountType;
            accountId = a.Id;


            Transactions = t.Select(t => new TransactionViewModel
            {
                Id = t.Id,
                NewBalance = t.NewBalance,
                Date = t.Date,
                TransactionType = t.Type,
                Operation = t.Operation,
                Amount = t.Amount
            }).ToList();

        }
    }
}
