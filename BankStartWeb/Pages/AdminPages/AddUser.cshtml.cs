using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankStartWeb.Pages.AdminPages
{

    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddUserModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
       
        public List<SelectListItem> AllRoles { get; set; }
        public List<string> Roles { get; set; }	
        public void OnGet()
        {
            SetAllRoles();
        }
        public void SetAllRoles()
            {
                AllRoles = _context.Roles.Select(role => new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString()
                }).ToList();
            }
        public IActionResult OnPost(string email, string password, string role)
        {
            Roles.Add(role);
            email = Email.Trim();
            password = Password.Trim();

            string[] roles = Roles.ToArray();

            if (_userManager.FindByEmailAsync(email).Result != null)
                return StatusCode(400, "Email already exists");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, password).Wait();
                _userManager.AddToRolesAsync(user, roles).Wait();
            }
            SetAllRoles();
            return Page();

        }

    }
}
