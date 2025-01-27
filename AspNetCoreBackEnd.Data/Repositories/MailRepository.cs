using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class MailRepository : GenericRepository<Mailings>, IMailRepository
    {
        public MailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
