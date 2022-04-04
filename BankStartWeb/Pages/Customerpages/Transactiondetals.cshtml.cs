using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customerpages
{
    public class TransactiondetalsModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public TransactiondetalsModel(ApplicationDbContext context)
		{
			_context = context;
		}
        public int Id { get; set; }
		public int AccountId { get; set; }
		public string AccountType { get; set; }
		public int CustomerId { get; set; }
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
    }
}
