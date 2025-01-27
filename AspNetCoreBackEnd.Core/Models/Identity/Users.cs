using Microsoft.AspNetCore.Identity;

namespace AspNetCoreBackEnd.Core.Models
{
    public class Users : IdentityUser<int>
    {
        public string realNameAndSurname { get; set; }
    }
}
