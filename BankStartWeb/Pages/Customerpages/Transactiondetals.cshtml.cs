using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankStartWeb.Transactions;
using BankStartWeb.Infrastructure.Paging;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Microsoft.AspNetCore.Authorization;

namespace BankStartWeb.Pages.Customerpages
{
    [Authorize]
    public class TransactiondetalsModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public TransactiondetalsModel(ApplicationDbContext context)
		{
			_context = context;
		}
        public int Id { get; set; }
        public string AccountType { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
        [BindProperty]
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

        var cust = _context.Customers
                .Include(c=>c.Accounts)
                .ThenInclude(a => a.Transactions.OrderByDescending(t=>t.Date))
                .First(c=>c.Accounts.Any( x => x.Id == accountId));

            var a = cust.Accounts.First(account => account.Id == accountId);
            var t = a.Transactions.OrderByDescending(d=>d.Date);

            CustomerId = cust.Id;
            FullName = cust.Givenname + " " + cust.Surname;
            Balance = a.Balance;
            AccountType = a.AccountType;
            Id = accountId;

        }

        public IActionResult OnGetFetchMore(int accountId, int pageNo)
        {
	        var cust = _context.Customers
		        .Include(c => c.Accounts)
		        .ThenInclude(a => a.Transactions.OrderByDescending(t => t.Date))
		        .First(c => c.Accounts.Any(x => x.Id == accountId));

	        var account = cust.Accounts.First(account => account.Id == accountId);
	        var transactions = account.Transactions.OrderByDescending(t=> t.Date).AsQueryable();

            var r = transactions.GetPaged(pageNo, 20);


			var list = r.Results.Select(t => new TransactionViewModel
			{
				Id = t.Id,
				Date = t.Date,
				Operation = t.Operation,
				TransactionType = t.Type,
				Amount = t.Amount,
				NewBalance = t.NewBalance
			}).ToList();

			bool lastPage = pageNo == r.PageCount;

			return new JsonResult(new { items = list, lastPage = lastPage });
        }

    }
}
