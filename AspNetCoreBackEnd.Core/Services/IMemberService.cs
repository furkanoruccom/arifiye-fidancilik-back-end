using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.viewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IMemberService
    {
        Task LogoutAsync();
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
        List<ClaimViewModel> GetClaims(ClaimsPrincipal principal);
        Task<Users> FindByEmailAsync(string mail);
        Task<string> GetAccessFailedCountAsync(Users user);
        int GetRoleIdUser(int userId);
        Task<Users> FindByUserIdAsync(int userId);
        Users FindByUserId(int userId);
    }
}
