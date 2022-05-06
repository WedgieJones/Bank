using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customerpages
{
	[Authorize]
	public class CustomersModel : PageModel
    {
		private readonly ApplicationDbContext _context;
		[BindProperty (SupportsGet = true)]
		public string SearchWord { get; set; }
		public int PageNo { get; set; }
		public int TotalPageCount { get; private set; }
		public string SortCol { get; set; }
		public string SortOrder { get; set; }
		public List<CustomerViewModel> Customers { get; set; }

		public CustomersModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public class CustomerViewModel
		{
			public string Givenname { get; set; }
			public string Surname { get; set; }
			public string NationalId { get; set; }
			public string CountryCode { get; set; }
			public string Address { get; set; }
			public string City { get; set; }
			public int Id { get; set; } 

		}
		public void OnGet(string searchWord, string col = "Givenname", string order = "asc", int pageno = 1 )
		{
			PageNo = pageno;
			SortCol = col;
			SortOrder = order;
			SearchWord = searchWord;

			var cust = _context.Customers.Take(600);

			if(!string.IsNullOrEmpty(SearchWord))
			cust = cust.Where(c => c.Givenname.Contains(SearchWord)
							||  c.Surname.Contains(SearchWord)
							|| c.City.Contains(SearchWord));


			cust = cust.OrderBy(col, order == "asc" ? ExtensionMethods.QuerySortOrder.Asc :
				ExtensionMethods.QuerySortOrder.Desc);


			var pageResult = cust.GetPaged(pageno, 600);
			TotalPageCount = pageResult.PageCount;

			Customers = pageResult.Results.Select(s =>
			new CustomerViewModel
			{
				Givenname = s.Givenname,
				Surname = s.Surname,
				NationalId = s.NationalId,
				CountryCode = s.CountryCode,
				Id = s.Id,
				Address = s.Streetaddress + ", " + s.City,
			}).ToList();
		}
	}
}
