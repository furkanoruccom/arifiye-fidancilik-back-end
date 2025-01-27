using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.viewModels;
using AspNetCoreBackEnd.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;

namespace AspNetCoreBackEnd.Service.Services
{
    public class MemberService : IMemberService
    {

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly AppDbContext _context;



        public MemberService(UserManager<Users> userManager, SignInManager<Users> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<Users> FindByEmailAsync(string mail)
        {
            var user = await _userManager.FindByEmailAsync(mail);


            return user!;
        }
        public async Task<Users> FindByUserIdAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == userId);
            return user!;
        }

        public Users FindByUserId(int userId)
        {
            var user = _context.Users.SingleOrDefault(i => i.Id == userId);
            return user!;
        }

        public async Task<string> GetAccessFailedCountAsync(Users user)
        {
            var count = await _userManager.GetAccessFailedCountAsync(user);
            return count.ToString();

        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var currentUser = (await _userManager.FindByNameAsync(userName))!;

            return await _userManager.CheckPasswordAsync(currentUser, password);
        }

        public async Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {
            var currentUser = (await _userManager.FindByNameAsync(userName))!;

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, oldPassword, newPassword);

            if (!resultChangePassword.Succeeded)
            {
                return (false, resultChangePassword.Errors);
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, newPassword, true, false);

            return (true, null);

        }




        public List<ClaimViewModel> GetClaims(ClaimsPrincipal principal)
        {
            return principal.Claims.Select(x => new ClaimViewModel()
            {
                Issuer = x.Issuer,
                Type = x.Type,
                Value = x.Value
            }).ToList();

        }


        public int GetRoleIdUser(int userId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(i => i.UserId == userId)!;
            return userRole.RoleId;
        }






    }
}
