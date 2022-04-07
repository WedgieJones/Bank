using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankStartWeb.Transactions;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Customerpages
{
    public class TransactiondetalsModel : PageModel
    {
		private readonly ApplicationDbContext _context;
		private readonly TransactionServices _transactions;

		public TransactiondetalsModel(ApplicationDbContext context, TransactionServices transactions)
		{
			_context = context;
			_transactions = transactions;
		}
        public int Id { get; set; }
		public int AccountId { get; set; }
		public string AccountType { get; set; }
		public int CustomerId { get; set; }
        [BindProperty]
        public decimal DepositAmount { get; set; }  
        public decimal WithdrawelAmount { get; set; }
        public decimal NewBalance { get; set; }
		public string FullName { get; set; }
		public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

	
		public void OnGet(int accountId)
        {

        var acc = _context.Customers
                .Include(a=>a.Accounts)
                .ThenInclude(t => t.Transactions.OrderByDescending(t=>t.Date))
                .First(c=>c.Accounts.Any( x => x.Id == accountId));

            var a = acc.Accounts.First(account => account.Id == accountId);

            CustomerId = acc.Id;
            FullName = acc.Givenname + " " + acc.Surname;
            AccountId = a.Id;
            AccountType = a.AccountType;
            Balance = a.Balance;
            Transactions = a.Transactions;
;           Created = a.Created;

            
        }

        public void OnPost(int accountId, decimal depositamount)
		{
            
                NewBalance = _transactions.Deposit(accountId, depositamount);
			//NewBalance = _transactions.Withdrawel(accountId, depositamount);

			var acc = _context.Customers
               .Include(a => a.Accounts)
               .ThenInclude(t => t.Transactions.OrderByDescending(t => t.Date))
               .First(c => c.Accounts.Any(x => x.Id == accountId));

            var a = acc.Accounts.First(account => account.Id == accountId);

            CustomerId = acc.Id;
            FullName = acc.Givenname + " " + acc.Surname;
            AccountId = a.Id;
            AccountType = a.AccountType;
            Balance = a.Balance;
            Transactions = a.Transactions;
            ; Created = a.Created;

        }
    }
}
