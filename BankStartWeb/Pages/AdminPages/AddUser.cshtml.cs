using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.AdminPages
{

    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
		private RoleManager<IdentityRole> _roleManager;
		private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddUserModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [DataType(DataType.Password, ErrorMessage= "Ett lösenord måste innehålla 7 tecken, stora och små bokstaver och 1 symbol")]
        [Required]
        public string Password { get; set; }
        [BindProperty]
        public string Role { get; set; }
        public List<SelectListItem> AllRoles { get; set; }
        public List<string> Roles { get; set; }	= new List<string>();
        public void OnGet()
        {
            SetAllRoles();
        }
        public void SetAllRoles()
            {
                AllRoles = _context.Roles.Select(role => new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name
                }).ToList();
            }
        public IActionResult OnPost()
        {
            var role = Role;
            Roles.Add(role);
            var email = Email.Trim();
            var password = Password.Trim();

            string[] roles = Roles.ToArray();
  ;

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

                var result = _userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {

                    //var result = _userManager.CreateAsync(user, password).Wait();
                    _userManager.AddToRoleAsync(user, role).Wait();
                    return RedirectToPage("/Index");
                }
				else
				{
                    ModelState.AddModelError(nameof(Password), "Ett lösenord måste innehålla 7 tecken, stora och små bokstaver och 1 symbol");
				}
				

            }
            SetAllRoles();
            return Page();

        }

    }
}
