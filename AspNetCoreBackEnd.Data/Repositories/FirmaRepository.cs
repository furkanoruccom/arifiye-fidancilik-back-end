using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class FirmaRepository : GenericRepository<Firmalar>, IFirmaRepository
    {
        public FirmaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
