using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customerpages
{
    public class CustomersModel : PageModel
    {
		private readonly ApplicationDbContext _context;
		[BindProperty (SupportsGet = true)]
		public string SearchWord { get; set; }
		public int PageNo { get; set; }
		public int TotalPageCount { get; private set; }
		public string SortCol { get; set; }
		public string SortOrder { get; set; }
		public List<Customer> Customers { get; set; }

		public CustomersModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public class Customer
		{
			public string Givenname { get; set; }
			public string Surname { get; set; }
			public string NationalId { get; set; }
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
							||  c.Surname.Contains(SearchWord));


			cust = cust.OrderBy(col, order == "asc" ? ExtensionMethods.QuerySortOrder.Asc :
				ExtensionMethods.QuerySortOrder.Desc);


			//if(col == "firstName") 
			//{
			//	if (order == "asc")
			//		cust = cust.OrderBy(cu => cu.Givenname);
			//	else
			//		cust = cust.OrderByDescending(cu => cu.Givenname);
			//}
			//else if(col== "surname")
			//{
			//	if(order =="asc")
			//		cust = cust.OrderBy(cu => cu.Surname);
			//	else
			//		cust = cust.OrderByDescending(cu => cu.Surname);
			//}
			//else if (col == "nationalid")
			//{
			//	if (order == "asc")
			//		cust = cust.OrderBy(cu => cu.NationalId);
			//	else
			//		cust = cust.OrderByDescending(cu => cu.NationalId);
			//}

			var pageResult = cust.GetPaged(pageno, 20);
			TotalPageCount = pageResult.PageCount;

			Customers = pageResult.Results.Select(s =>
			new Customer
			{
				Givenname = s.Givenname,
				Surname = s.Surname,
				NationalId = s.NationalId,
				Id = s.Id
			}).ToList();
		}
	}
}
