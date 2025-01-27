

using AspNetCoreBackEnd.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndAspNetCore.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {

            var Admin = new Users
            {
                realNameAndSurname = "Admin User",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "ADMINKULLANICISI",
                UserName = "adminkullanicisi",
                PasswordHash = "AQAAAAIAAYagAAAAEASD4p0UosUMA9Pf2eKFxw7eYhCY7/dd9se4Ua4HgDrHSVVGbRkdPnH/Pb4AN7bvfw==",
                SecurityStamp = "62YQVR7YOBQ46KRZD6PAWEEZJ6EUJLFY",
                Id = 1
            };
            //h&$8kZo



            //seed user
            builder.HasData(Admin);




        }
    }
}
