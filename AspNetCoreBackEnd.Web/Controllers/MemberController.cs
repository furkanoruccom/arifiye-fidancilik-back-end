using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.viewModels;
using AspNetCoreBackEnd.Web.Extenisons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCoreBackEnd.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<Users> _signInManager;
        private UserManager<Users> _userManager;
        private RoleManager<UserRole> _roleManager;
        private IResponseService _responseService;
        private IMemberService _memberService;

        public MemberController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<UserRole> roleManager, IResponseService responseService, IMemberService memberService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _responseService = responseService;
            _memberService = memberService;
        }

        private string userName => User.Identity!.Name!;
        private int userId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);







        //Çıkış Yap | Method
        public async Task<IActionResult> Logout()
        {
            await _memberService.LogoutAsync();
            return RedirectToAction("Login", "Member");
        }


        public IActionResult Login()
        {
            return View();
        }





        //Sisteme Giriş | Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SignIn(SignInViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { eventAjax = "Errors", errors = ModelState });
            }
            returnUrl ??= "/Panel/";


            var hasUser = await _memberService.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                return Json(_responseService.HandleError("Email or password is incorrect."));

            }





            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);


            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "You cannot log in for 3 minutes." });
                return Json(new { eventAjax = "TimeErrors", errors = ModelState });

            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Email or password is incorrect", $"Number of failed logins = {await _memberService.GetAccessFailedCountAsync(hasUser)}" });
                return Json(_responseService.HandleError("Email or password is incorrect."));

            }

            return Json(_responseService.HandleSuccessData("Login successful", returnUrl));
        }




        [ValidateAntiForgeryToken]
        public JsonResult GetUserRole()
        {
            var role = _memberService.GetRoleIdUser(userId);
            return Json(role);
        }

    }


}

