using BEExam3.Areas.Manage.ViewModels.Account;
using BEExam3.Models;
using BEExam3.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BEExam3.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _useman;
        private readonly SignInManager<AppUser> _signman;
        private readonly RoleManager<IdentityRole> _roleman;

        public AccountController(UserManager<AppUser> useman,SignInManager<AppUser> signman,RoleManager<IdentityRole> roleman)
        {
            _useman = useman;
            _signman = signman;
            _roleman = roleman;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            AppUser user= new AppUser
            {
                Name= vm.Name,
                Email= vm.Email,
                Surname= vm.Surname,
                UserName=vm.Username
            };
            var result=await _useman.CreateAsync(user,vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(vm);
            }
            await _useman.AddToRoleAsync(user,Roles.Admin.ToString());
            await _signman.SignInAsync(user, false);

            return RedirectToAction("Index", "DashBoard");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            AppUser user = await _useman.FindByNameAsync(vm.EmailOrUsername);
            if (user == null)
            {
                user=await _useman.FindByEmailAsync(vm.EmailOrUsername);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "bele istifadechi tapilmadi");
                    return View(vm);
                }
            }
            var result=await _signman.PasswordSignInAsync(user, vm.Password,vm.IsRemembered,true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "sehv");
                return View(vm);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "blok olundunuz");
                return View(vm);
            }

            return RedirectToAction("Index", "DashBoard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signman.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleman.RoleExistsAsync(item.ToString()))
                {
                    await _roleman.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
            return RedirectToAction("Index", "DashBoard");
           
        }
    }
}
